
using Sarafi.Domain.Common;
using Sarafi.Domain.Enums;

namespace Sarafi.Domain.Entities; 
public class ExchangeRate : AggregateRoot, IMultiTenant
{
    public DateTime DateTime { get; private set; }
    public decimal Rate { get; private set; }
    public CurrencyType FromCurrencyType { private set; get; }
    public CurrencyType ToCurrencyType { private set; get; }
    public long CompanyId { get; set; }

    public void SetCompanyId(long companyId) => CompanyId = companyId;
}
