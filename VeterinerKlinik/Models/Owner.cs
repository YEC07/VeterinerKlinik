using System;
using System.Collections.Generic;

namespace VeterinerKlinik.Models;

public partial class Owner
{
    public int OwnerId { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string? ContactNumber { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string IdentityNumber { get; set; } = null!;

    public virtual ICollection<Pet> Pets { get; set; } = new List<Pet>();

    public virtual ICollection<Receipt> Receipts { get; set; } = new List<Receipt>();
}
