using System.ComponentModel.DataAnnotations;

namespace CBR.Storage.Models;

public class Currency
{
    [Key]
    public string CurrencyId { get; set; }

    public string Name { get; set; }
    public string EngName { get; set; }
    public string ParentCode { get; set; }
    public int Nominal { get; set; }

    public ICollection<CurrencyCourse> Courses { get; set; } = new List<CurrencyCourse>();
}
