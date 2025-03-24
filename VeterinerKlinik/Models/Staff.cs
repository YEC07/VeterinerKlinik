using System;
using System.Collections.Generic;

namespace VeterinerKlinik.Models;

public partial class Staff
{
    public int StaffId { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public DateOnly? WorkStartDate { get; set; }

    public virtual ICollection<Inspection> Inspections { get; set; } = new List<Inspection>();

    public virtual ICollection<Receipt> Receipts { get; set; } = new List<Receipt>();

    public virtual ICollection<Vaccination> Vaccinations { get; set; } = new List<Vaccination>();
}
