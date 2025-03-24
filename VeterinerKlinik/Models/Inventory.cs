using System;
using System.Collections.Generic;

namespace VeterinerKlinik.Models;

public partial class Inventory
{
    public int ItemId { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public int StockQuantity { get; set; }

    public decimal? UnitPrice { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();

    public virtual ICollection<Vaccination> Vaccinations { get; set; } = new List<Vaccination>();
}
