using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BankingApp.Models;

public class TransactionModel
{
    [Display(Name ="Amount In USD")]

    [Required(ErrorMessage = "AmountSendInUSD is Mandatory.")]
    public decimal AmountSendInUSD { get; set; }

    //[Display(Name ="Amount In INR")]
    //[Required(ErrorMessage = "AmountRecieved is Mandatory.")]
    //public decimal? AmountReceivedInINR { get; set; }


    [StringLength(200)]
    [Required(ErrorMessage = "Sender Name is Mandatory.")]
    public string Sender { get; set; }

    [StringLength(200)]
    [Required(ErrorMessage = "Receiver is Mandatory.")]
    public string Receiver { get; set; }

    [StringLength(500)]

    [Display(Name ="Purpose")]
    [Required(ErrorMessage = "Purpose is Mandatory.")]
    public string PurposeID { get; set; }

    public IList<SelectListItem> Purposes { get; set; }
}

