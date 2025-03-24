using System;
using System.Collections.Generic;

namespace VeterinerKlinik.Models;

public partial class ReceiptDetail
{
    public int DetailId { get; set; }

    public int ReceiptId { get; set; }

    public int InspectionId { get; set; }

    public decimal Amount { get; set; }

    public virtual Inspection Inspection { get; set; } = null!;

    public virtual Receipt Receipt { get; set; } = null!;
}
