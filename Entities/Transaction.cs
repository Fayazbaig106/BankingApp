using System;
using System.Collections.Generic;

namespace BankingApp.Entities;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public decimal AmountSendInUsd { get; set; }

    public decimal AmountReceivedInInr { get; set; }

    public string Sender { get; set; } = null!;

    public string Receiver { get; set; } = null!;

    public int PurposeId { get; set; }

    public virtual Purpose Purpose { get; set; } = null!;
}
