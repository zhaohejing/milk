using EntityFramework.DynamicFilters;
using YT.EntityFramework;

namespace YT.Tests.TestDatas
{
    public class TestDataBuilder
    {
        private readonly YtDbContext _context;
        private readonly int _tenantId;

        public TestDataBuilder(YtDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new TestOrganizationUnitsBuilder(_context, _tenantId).Create();

            _context.SaveChanges();
        }
    }
}
