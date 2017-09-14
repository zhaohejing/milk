using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Microsoft.AspNet.Identity;
using YT.Authorization.Users.Dto;
using YT.Authorization.Users.Exporting;
using YT.Dto;
using YT.Managers.Roles;
using YT.Managers.Users;
using YT.Notifications;

namespace YT.Authorization.Users
{
    /// <summary>
    /// 用户service
    /// </summary>
 //   [AbpAuthorize(StaticPermissionsName.Page_User)]
    [AbpAuthorize]
    public class UserAppService : YtAppServiceBase, IUserAppService
    {
        private readonly RoleManager _roleManager;
        private readonly IUserListExcelExporter _userListExcelExporter;
        private readonly IAppNotifier _appNotifier;
        private readonly IRepository<RolePermissionSetting, long> _rolePermissionRepository;
        private readonly IRepository<UserPermissionSetting, long> _userPermissionRepository;
        private readonly IRepository<UserRole, long> _userRoleRepository;
        private readonly IRepository<User, long> _userRepository;
      /// <summary>
      /// ctor
      /// </summary>
      /// <param name="roleManager"></param>
      /// <param name="userListExcelExporter"></param>
      /// <param name="appNotifier"></param>
      /// <param name="rolePermissionRepository"></param>
      /// <param name="userPermissionRepository"></param>
      /// <param name="userRoleRepository"></param>
      /// <param name="userRepository"></param>
        public UserAppService(
            RoleManager roleManager,
            IUserListExcelExporter userListExcelExporter,
            IAppNotifier appNotifier,
            IRepository<RolePermissionSetting, long> rolePermissionRepository,
            IRepository<UserPermissionSetting, long> userPermissionRepository,
            IRepository<UserRole, long> userRoleRepository, IRepository<User, long> userRepository)
        {
            _roleManager = roleManager;
            _userListExcelExporter = userListExcelExporter;
            _appNotifier = appNotifier;
            _rolePermissionRepository = rolePermissionRepository;
            _userPermissionRepository = userPermissionRepository;
            _userRoleRepository = userRoleRepository;
            _userRepository = userRepository;
        }
        /// <summary>
        /// 获取用户 分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<UserListDto>> GetUsers(GetUsersInput input)
        {
            var query = UserManager.Users
                .Include(u => u.Roles)
                .WhereIf(input.Role.HasValue, u => u.Roles.Any(r => r.RoleId == input.Role.Value))
                .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.Name.Contains(input.Filter) ||
                        u.Surname.Contains(input.Filter) ||
                        u.UserName.Contains(input.Filter)
                // ||  u.EmailAddress.Contains(input.Filter)
                );

            if (!input.Permission.IsNullOrWhiteSpace())
            {
                query = (from user in query
                         join ur in _userRoleRepository.GetAll() on user.Id equals ur.UserId into urJoined
                         from ur in urJoined.DefaultIfEmpty()
                         join up in _userPermissionRepository.GetAll() on new { UserId = user.Id, Name = input.Permission } equals new { up.UserId, up.Name } into upJoined
                         from up in upJoined.DefaultIfEmpty()
                         join rp in _rolePermissionRepository.GetAll() on new { RoleId = ur.RoleId, Name = input.Permission } equals new { rp.RoleId, rp.Name } into rpJoined
                         from rp in rpJoined.DefaultIfEmpty()
                         where (up != null && up.IsGranted) || (up == null && rp != null)
                         group user by user into userGrouped
                         select userGrouped.Key);
            }

            var userCount = await query.CountAsync();
            var users = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();

            var userListDtos = users.MapTo<List<UserListDto>>();
            await FillRoleNames(userListDtos);

            return new PagedResultDto<UserListDto>(
                userCount,
                userListDtos
                );
        }
        /// <summary>
        /// 导出用户
        /// </summary>
        /// <returns></returns>
        public async Task<FileDto> GetUsersToExcel()
        {
            var users = await UserManager.Users.Include(u => u.Roles).ToListAsync();
            var userListDtos = users.MapTo<List<UserListDto>>();
            await FillRoleNames(userListDtos);

            return _userListExcelExporter.ExportToFile(userListDtos);
        }
        /// <summary>
        /// 获取用户用于编辑
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetUserForEditOutput> GetUserForEdit(NullableIdDto<long> input)
        {
            //Getting all available roles
            var userRoleDtos = (await _roleManager.Roles
                .OrderBy(r => r.DisplayName)
                .Select(r => new UserRoleDto
                {
                    RoleId = r.Id,
                    RoleName = r.Name,
                    RoleDisplayName = r.DisplayName
                })
                .ToArrayAsync());

            var output = new GetUserForEditOutput
            {
                Roles = userRoleDtos
            };

            if (!input.Id.HasValue)
            {
                //Creating a new user
                output.User = new UserEditDto
                {
                    IsActive = true,
                };

                foreach (var defaultRole in await _roleManager.Roles.Where(r => r.IsDefault).ToListAsync())
                {
                    var defaultUserRole = userRoleDtos.FirstOrDefault(ur => ur.RoleName == defaultRole.Name);
                    if (defaultUserRole != null)
                    {
                        defaultUserRole.IsAssigned = true;
                    }
                }
            }
            else
            {
                //Editing an existing user
                var user = await UserManager.GetUserByIdAsync(input.Id.Value);

                output.User = user.MapTo<UserEditDto>();
                output.ProfilePictureId = user.ProfilePictureId;

                foreach (var userRoleDto in userRoleDtos)
                {
                    userRoleDto.IsAssigned = await UserManager.IsInRoleAsync(input.Id.Value, userRoleDto.RoleName);
                }
            }

            return output;
        }
        /// <summary>
        /// 获取用户的权限 和信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetUserPermissionsForEditOutput> GetUserPermissionsForEdit(EntityDto<long> input)
        {
            var user = await UserManager.GetUserByIdAsync(input.Id);
            var grantedPermissions = await _userPermissionRepository.GetAllListAsync(c => c.UserId == input.Id);
            return new GetUserPermissionsForEditOutput
            {
                UserDto = user.MapTo<UserEditDto>(),
                GrantedPermissionNames = grantedPermissions.Select(p => p.Name).ToList()
            };
        }
        /// <summary>
        /// 重置用户的权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task ResetUserSpecificPermissions(EntityDto<long> input)
        {
            var user = await UserManager.GetUserByIdAsync(input.Id);
            await _userPermissionRepository.DeleteAsync(c => c.UserId == user.Id);
        }
        /// <summary>
        /// 更新用户权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateUserPermissions(UpdateUserPermissionsInput input)
        {
            var user = await UserManager.GetUserByIdAsync(input.Id);
             await _userPermissionRepository.DeleteAsync(c => c.UserId == user.Id);

            foreach (var t in input.GrantedPermissionNames)
            {
                await _userPermissionRepository.InsertAsync(new UserPermissionSetting()
                {
                    IsGranted = true,
                    Name = t,
                    TenantId = user.TenantId,
                    UserId = user.Id
                });
            }

          
        }
        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task CreateOrUpdateUser(CreateOrUpdateUserInput input)
        {
            if (input.User.Id.HasValue)
            {
                await UpdateUserAsync(input);
            }
            else
            {
                await CreateUserAsync(input);
            }
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task DeleteUser(EntityDto<long> input)
        {
            if (input.Id == AbpSession.GetUserId())
            {
                throw new AbpException("不可删除自身");
            }
            var user = await UserManager.GetUserByIdAsync(input.Id);
            CheckErrors(await UserManager.DeleteAsync(user));
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected virtual async Task UpdateUserAsync(CreateOrUpdateUserInput input)
        {
            Debug.Assert(input.User.Id != null, "用户id不可谓null");
            var user =await _userRepository.GetAllIncluding(c=>c.Roles).FirstOrDefaultAsync(c=>c.Id== input.User.Id.Value);
            //Update user properties
            input.User.MapTo(user); //Passwords is not mapped (see mapping configuration)

            if (input.SetDefaultPassword)
            {
                input.User.Password = User.DefaultPassword;
            }

            if (!input.User.Password.IsNullOrEmpty())
            {
                CheckErrors(await UserManager.ChangePasswordAsync(user, input.User.Password));
            }

            CheckErrors(await UserManager.UpdateAsync(user));

            //Update roles
            CheckErrors(await UserManager.SetRoles(user, input.AssignedRoleNames));
        }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected virtual async Task CreateUserAsync(CreateOrUpdateUserInput input)
        {
            //最大人数
            //if (AbpSession.TenantId.HasValue)
            //{
            //    await _userPolicy.CheckMaxUserCountAsync(AbpSession.GetTenantId());
            //}

            var user = input.User.MapTo<User>(); //Passwords is not mapped (see mapping configuration)
            user.TenantId = AbpSession.TenantId;

            //Set password
            if (!input.User.Password.IsNullOrEmpty())
            {
                CheckErrors(await UserManager.PasswordValidator.ValidateAsync(input.User.Password));
            }
            else
            {
                input.User.Password = User.DefaultPassword;
            }

            user.Password = new PasswordHasher().HashPassword(input.User.Password);
            //   user.ShouldChangePasswordOnNextLogin = input.User.ShouldChangePasswordOnNextLogin;

            //Assign roles
            user.Roles = new Collection<UserRole>();
            foreach (var roleName in input.AssignedRoleNames)
            {
                var role = await _roleManager.GetRoleByNameAsync(roleName);
                user.Roles.Add(new UserRole(AbpSession.TenantId, user.Id, role.Id));
            }

            CheckErrors(await UserManager.CreateAsync(user));
            await CurrentUnitOfWork.SaveChangesAsync(); //To get new user's Id.

            //Notifications
            //  await _notificationSubscriptionManager.SubscribeToAllAvailableNotificationsAsync(user.ToUserIdentifier());
            await _appNotifier.WelcomeToTheApplicationAsync(user);


        }
        /// <summary>
        /// 获取角色名
        /// </summary>
        /// <param name="userListDtos"></param>
        /// <returns></returns>
        private async Task FillRoleNames(List<UserListDto> userListDtos)
        {
            /* This method is optimized to fill role names to given list. */

            var distinctRoleIds = (
                from userListDto in userListDtos
                from userListRoleDto in userListDto.Roles
                select userListRoleDto.RoleId
                ).Distinct();

            var roleNames = new Dictionary<int, string>();
            foreach (var roleId in distinctRoleIds)
            {
                roleNames[roleId] = (await _roleManager.GetRoleByIdAsync(roleId)).DisplayName;
            }

            foreach (var userListDto in userListDtos)
            {
                foreach (var userListRoleDto in userListDto.Roles)
                {
                    userListRoleDto.RoleName = roleNames[userListRoleDto.RoleId];
                }

                userListDto.Roles = userListDto.Roles.OrderBy(r => r.RoleName).ToList();
            }
        }
    }

}
