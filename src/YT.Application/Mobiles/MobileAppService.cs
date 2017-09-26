using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using YT.Customers.Dtos;
using YT.Mobiles.Dtos;
using YT.Models;

namespace YT.Mobiles
{
    /// <summary>
    /// 手机端服务
    /// </summary>
    public class MobileAppService : YtAppServiceBase, IMobileAppService
    {
        private readonly IRepository<Customer, int> _customerRepository;
        private readonly IRepository<SpecialCard> _specialRepository;
        private readonly IRepository<VilidateCode> _codeRepository;
        private readonly IRepository<Straw> _strawRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderItem> _orderItemRepository;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="customerRepository"></param>
        /// <param name="specialRepository"></param>
        /// <param name="codeRepository"></param>
        /// <param name="strawRepository"></param>
        /// <param name="orderRepository"></param>
        /// <param name="orderItemRepository"></param>
        public MobileAppService(IRepository<Customer, int> customerRepository,
            IRepository<SpecialCard> specialRepository,
            IRepository<VilidateCode> codeRepository,
            IRepository<Straw> strawRepository, 
            IRepository<Order> orderRepository,
            IRepository<OrderItem> orderItemRepository)
        {
            _customerRepository = customerRepository;
            _specialRepository = specialRepository;
            _codeRepository = codeRepository;
            _strawRepository = strawRepository;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
        }

        #region 登陆注册相关
        /// <summary>
        /// 登陆接口 byopenId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<CustomerListDto> LoginByOpenId(LoginOpenIdModel input)
        {
            var customer =
              await _customerRepository.FirstOrDefaultAsync(
                    c => c.Account.Equals(input.Name) && c.Password.Equals(input.Password));
            if (customer == null) throw new AbpException("当前用户不存在");
            if (customer.UserKey.IsNullOrWhiteSpace()) customer.UserKey = input.OpenId;
            return customer.MapTo<CustomerListDto>();
        }
        /// <summary>
        /// 登陆接口 byspecial
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<CustomerListDto> LoginBySpecial(LoginSpecialModel input)
        {
            var card =
               await _specialRepository.FirstOrDefaultAsync(
                    c => c.CardCode.Equals(input.CardCode) && c.Password.Equals(input.Password) && c.IsUsed);
            if (card == null)
            {
                throw new AbpException("该唯鲜卡不存在");
            }

            var customer =
              await _customerRepository.FirstOrDefaultAsync(c => c.SpecialId.HasValue && c.SpecialId == card.Id);
            if (customer == null) throw new AbpException("当前用户不存在");
            return customer.MapTo<CustomerListDto>();
        }
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <returns></returns>
        public async Task<CustomerListDto> Register(RegisterModel input)
        {
            var code = _codeRepository.GetAllList(c => c.Mobile.Equals(input.Mobile))
                .Where(c => c.CreationTime >= DateTime.Now.AddMinutes(-30)).OrderByDescending(c => c.CreationTime).FirstOrDefault();
            if (code == null)
            {
                throw new AbpException("请先获取验证码");
            }
            if (!code.Code.Equals(input.Code))
            {
                throw new AbpException("验证码错误");
            }
            var count = await _customerRepository.CountAsync(c => c.Account.Equals(input.Mobile));
            if (count > 0)
            {
                throw new AbpException("当前手机号已被注册,请登录");
            }
            var model = new Customer()
            {
                Account = input.Mobile,
                BirthDay = input.Birthday,
                CustomerName = input.CustomerName,
                Gender = input.Gender,
                IsActive = true,
                Mobile = input.Mobile,
                Password = input.Password,
                UserKey = input.OpenId,
                Avatar = input.Avatar
            };
            await _customerRepository.InsertAsync(model);
            await CurrentUnitOfWork.SaveChangesAsync();
            return model.MapTo<CustomerListDto>();
        }
        /// <summary>
        /// 绑定唯鲜卡
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task BindSpecialCard(BindSpecialCardModel input)
        {
            var card =
              await _specialRepository.FirstOrDefaultAsync(
                   c => c.CardCode.Equals(input.CardCode) && c.Password.Equals(input.Password));
            if (card == null)
            {
                throw new AbpException("该唯鲜卡不存在");
            }
            if (card.IsUsed)
            {
                throw new AbpException("该唯鲜卡已被使用");
            }
            var customer = await _customerRepository.FirstOrDefaultAsync(c => c.UserKey.Equals(input.OpenId));
            if (customer == null)
            {
                throw new AbpException("当前用户不存在");
            }
            customer.SpecialId = card.Id;
            card.IsUsed = true;
        }
        #endregion
        #region 充值相关
        /// <summary>
        /// 获取充值记录
        /// </summary>
        /// <returns></returns>
        public List<ChargeType> GetChargeTypeList()
        {
            return MilkConsts.ChargeTypes;
        }
        #endregion
        #region 吸管相关
        /// <summary>
        /// 管理员设置用户的吸管状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateCustomerStrawState(Entity<int> input)
        {
            var customer = await _customerRepository.FirstOrDefaultAsync(input.Id);
            if (customer == null)
            {
                throw new AbpException("当前用户不存在");
            }
            customer.CanPickUpStraw = true;
        }
        /// <summary>
        /// 获取用户是否可以取得吸管
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> CanPickUpStraw(UserKeyModel input)
        {
            var customer = await _customerRepository.FirstOrDefaultAsync(c => c.UserKey.Equals(input.OpenId));
            if (customer == null)
            {
                throw new AbpException("当前用户不存在");
            }
            var now = DateTime.Now;
            var left = now.AddDays(-(int)now.DayOfWeek + 1);
            var right = now.AddDays(7 - (int)now.DayOfWeek);
            var count =
              await _strawRepository.CountAsync(
                    c => c.CreationTime > left && c.CreationTime <= right && c.UserKey.Equals(input.OpenId));
            customer.CanPickUpStraw = count > 0;
            return customer.CanPickUpStraw;
        }
        /// <summary>
        /// 用户获取吸管
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task PickUpStraw(UserKeyModel input)
        {
            var customer = await _customerRepository.FirstOrDefaultAsync(c => c.UserKey.Equals(input.OpenId));
            if (customer == null)
            {
                throw new AbpException("当前用户不存在");
            }
            customer.CanPickUpStraw = false;
            await _strawRepository.InsertAsync(new Straw() { UserKey = input.OpenId });
        }
        #endregion
        #region 奶瓶相关
        /// <summary>
        /// 获取用户的奶瓶数量
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> GetCustomerBottleCount(UserKeyModel input)
        {
            var customer = await _customerRepository.FirstOrDefaultAsync(c=>c.UserKey.Equals(input.OpenId));
            if (customer == null)
            {
                throw new AbpException("当前用户不存在");
            }
            return customer.BottleCount;
        }
        /// <summary>
        /// 核销奶瓶
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task GetCustomerBottleCount(DealBottleModel input)
        {
            var customer = await _customerRepository.FirstOrDefaultAsync(c => c.UserKey.Equals(input.OpenId));
            if (customer == null)
            {
                throw new AbpException("当前用户不存在");
            }
            if (input.DealCount>customer.BottleCount)
            {
                throw new AbpException("核销奶瓶数不可大于持有数");
            }
            customer.BottleCount -= input.DealCount;
        }
        #endregion
        #region 订单相关

        #endregion
    }
    /// <summary>
    /// 手机端接口
    /// </summary>
    public interface IMobileAppService : IApplicationService
    {
        /// <summary>
        /// 登陆接口 byopenId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CustomerListDto> LoginByOpenId(LoginOpenIdModel input);
        /// <summary>
        ///  登陆接口 byspecial
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CustomerListDto> LoginBySpecial(LoginSpecialModel input);

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <returns></returns>
        Task<CustomerListDto> Register(RegisterModel input);
        /// <summary>
        /// 绑定唯鲜卡
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task BindSpecialCard(BindSpecialCardModel input);
        #region 吸管相关

        /// <summary>
        /// 获取用户是否可以取得吸管
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> CanPickUpStraw(UserKeyModel input);

        /// <summary>
        /// 用户获取吸管
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task PickUpStraw(UserKeyModel input);
        /// <summary>
        /// 管理员设置用户的吸管状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateCustomerStrawState(Entity<int> input);

        #endregion
        #region 奶瓶相关

        /// <summary>
        /// 获取用户的奶瓶数量
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
          Task<int> GetCustomerBottleCount(UserKeyModel input);

        /// <summary>
        /// 核销奶瓶
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
          Task GetCustomerBottleCount(DealBottleModel input);

        #endregion
    }
}
