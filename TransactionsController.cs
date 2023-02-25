
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Banking;
using BankingApp;
using BankingApp.Entities;
using BankingApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Banking
{
    public class TransactionsController : Controller
    {
        public IActionResult TransactionsList()
        {
            var dbContext = new BankingDbContext();

            var transactions = dbContext.Transactions.Include(p=>p.Purpose).ToList();

            return View(transactions);
            
        }

        public IActionResult TransactionEditor()
        {
            var dbContext = new BankingDbContext();
            var purposes = dbContext.Purposes.ToList();

            var model = new TransactionModel();
            model.Purposes = new List<SelectListItem>();

            foreach(var purpose in purposes)
            {
                model.Purposes.Add(new SelectListItem
                {
                    Text = purpose.Title,
                    Value = purpose.PurposeId.ToString()
                });

            }
            return View(model);
        }

        [HttpPost]
        public IActionResult TransactionEditor(TransactionModel model)
        {
            ModelState.Remove("Purposes");

            if (ModelState.IsValid)
            {
                var dbContext = new BankingDbContext();

                var exchangeRateUSDtoINR = 80; //TODO: we can fetch this from database table

                var transactionObj = new Transaction
                {
                    AmountSendInUsd = model.AmountSendInUSD,
                    AmountReceivedInInr = model.AmountSendInUSD * exchangeRateUSDtoINR, // amount converted into INR
                    Sender = model.Sender,
                    Receiver = model.Receiver,
                    PurposeId = Convert.ToInt32(model.PurposeID)
                };

                dbContext.Transactions.Add(transactionObj);

                dbContext.SaveChanges();

                return RedirectToAction("TransactionDetail", "Transactions", new { transactionId = transactionObj.TransactionId});
            }
            else
            {
                ModelState.AddModelError("", "Transactioins not saved, please fix errors and save again!");

                return View(model);
            }
        }

        public IActionResult TransactionDetail(int transactionId)
        {

            var dbContext = new BankingDbContext();

            var transactionEntityObj = dbContext.Transactions.FirstOrDefault(p => p.TransactionId == transactionId);

            return View(transactionEntityObj);
        }
    }
}
