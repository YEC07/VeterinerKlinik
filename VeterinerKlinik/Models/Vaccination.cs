using System;
using System.Collections.Generic;

namespace VeterinerKlinik.Models;

public partial class Vaccination
{
    public int VaccinationId { get; set; }

    public int PetId { get; set; }

    public int VaccineId { get; set; }

    public int StaffId { get; set; }

    public DateOnly? VaccinationDate { get; set; }

    public DateOnly? NextVaccinationDate { get; set; }

    public string? Notes { get; set; }

    public virtual Pet Pet { get; set; } = null!;

    public virtual Staff Staff { get; set; } = null!;

    public virtual Inventory Vaccine { get; set; } = null!;
}
