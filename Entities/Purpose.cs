using System;
using System.Collections.Generic;

namespace BankingApp.Entities;

public partial class Purpose
{
    public int PurposeId { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; } = new List<Transaction>();
}
