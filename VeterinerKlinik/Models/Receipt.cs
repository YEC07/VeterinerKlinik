using System;
using System.Collections.Generic;

namespace VeterinerKlinik.Models;

public partial class Receipt
{
    public int ReceiptId { get; set; }

    public int OwnerId { get; set; }

    public int StaffId { get; set; }

    public DateTime ReceiptDate { get; set; }

    public decimal TotalAmount { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public string PaymentStatus { get; set; } = null!;

    public string? Notes { get; set; }

    public virtual Owner Owner { get; set; } = null!;

    public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; } = new List<ReceiptDetail>();

    public virtual Staff Staff { get; set; } = null!;
}
