using System;
using System.Collections.Generic;

namespace VeterinerKlinik.Models;

public partial class Pet
{
    public int PetId { get; set; }

    public int OwnerId { get; set; }

    public int GroupId { get; set; }

    public string Name { get; set; } = null!;

    public string Species { get; set; } = null!;

    public string? Breed { get; set; }

    public DateOnly? BirthDate { get; set; }

    public string? Gender { get; set; }

    public decimal? Weight { get; set; }

    public string? MicrochipNumber { get; set; }

    public DateOnly? RegistrationDate { get; set; }

    public int? Age { get; set; }

    public virtual AnimalGroup Group { get; set; } = null!;

    public virtual ICollection<Inspection> Inspections { get; set; } = new List<Inspection>();

    public virtual Owner Owner { get; set; } = null!;

    public virtual ICollection<Vaccination> Vaccinations { get; set; } = new List<Vaccination>();
}
