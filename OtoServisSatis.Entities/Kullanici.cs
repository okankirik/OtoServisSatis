﻿using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace OtoServisSatis.Entities;

public class Kullanici : IEntity
{
    public int Id { get; set; }
    [StringLength(50)]
    [Display(Name = "Adı")]
    public string Adi { get; set; }
    [StringLength(50)]
    [Display(Name = "Soyadı")]
    public string Soyadi { get; set; }
    [StringLength(50)]
    public string Email { get; set; }
    [StringLength(20)]
    public string Telefon { get; set; }
    [StringLength(50)]
    [Display(Name = "Kullanıcı Adı")]
    public string KullaniciAdi { get; set; }
    [StringLength(50)]
    public string Sifre { get; set; }
    public bool AktifMi { get; set; }
    [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]
    //ScaffoldColumn(false) = Ekranda bu kolon için gerekli alanları oluşturmaz.
    public DateTime? EklenmeTarihi { get; set; } = DateTime.Now;
    public int RolId { get; set; }
    public virtual Rol? Rol { get; set; }
}
