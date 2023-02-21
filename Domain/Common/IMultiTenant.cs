
namespace Sarafi.Domain.Common
{
    public interface IMultiTenant
    {
        public long CompanyId { set; get; }
        public void SetCompanyId(long companyId);
    }
}
