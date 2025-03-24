using System;
using System.Collections.Generic;

namespace VeterinerKlinik.Models;

public partial class Prescription
{
    public int PrescriptionId { get; set; }

    public int InspectionId { get; set; }

    public int MedicineId { get; set; }

    public int Quantity { get; set; }

    public string? UsageInstructions { get; set; }

    public virtual Inspection Inspection { get; set; } = null!;

    public virtual Inventory Medicine { get; set; } = null!;
}
