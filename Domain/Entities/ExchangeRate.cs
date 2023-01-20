
using Sarafi.Domain.Common;
using Sarafi.Domain.Enums;

namespace Sarafi.Domain.Entities; 
public class ExchangeRate : AuditableEntity
{
    public DateTime DateTime { get; set; }
    public decimal Rate { get; set; }
    public CurrencyType FromCurrencyType { set; get; }
    public CurrencyType ToCurrencyType { set; get; }
}
