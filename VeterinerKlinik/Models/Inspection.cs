using System;
using System.Collections.Generic;

namespace VeterinerKlinik.Models;

public partial class Inspection
{
    public int InspectionId { get; set; }

    public int PetId { get; set; }

    public int StaffId { get; set; }

    public DateTime? ExamDate { get; set; }

    public string? Symptoms { get; set; }

    public string? Diagnosis { get; set; }

    public string? Treatment { get; set; }

    public string? Notes { get; set; }

    public decimal? Cost { get; set; }

    public virtual Pet Pet { get; set; } = null!;

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();

    public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; } = new List<ReceiptDetail>();

    public virtual Staff Staff { get; set; } = null!;
}
