using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace OtoServisSatis.Entities;

public class Marka : IEntity
{
    public int Id { get; set; }
    [Display(Name = "Adı")]
    public string Adi { get; set; }
}
