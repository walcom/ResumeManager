using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResumeManager.Data;
using ResumeManager.Models;
using ResumeManager.ViewModel;

namespace ResumeManager.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ResumeDbContext _context;

        public TransactionController(ResumeDbContext context)
        {
            _context = context;
        }

        // GET: Transaction
        public async Task<IActionResult> Index()
        {
            return View(await _context.Transactions.Include(b => b.Bank).ToListAsync());
        }

        // GET: Transaction/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionModel = await _context.Transactions
                .FirstOrDefaultAsync(m => m.TransactionID == id);
            if (transactionModel == null)
            {
                return NotFound();
            }

            return View(transactionModel);
        }

        // GET: Transaction/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Transaction/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("TransactionID,AccountNumber,BeneficiaryName,BankName,SWIFTCode,Amount")] TransactionModel transactionModel)
        //{
        //    //if (ModelState.IsValid)
        //    //{
        //    //    _context.Add(transactionModel);
        //    //    await _context.SaveChangesAsync();
        //    //    return RedirectToAction(nameof(Index));
        //    //}
        //    return View(transactionModel);
        //}

        // GET: Transaction/AddOrEdit/5
        //[NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                var model = new TransactionVM
                {
                    Banks = FillBanksList()
                };

                return View(model);
                //return View(new TransactionModel());
            }
            else
            {
                var transactionModel = await _context.Transactions.Include(b => b.Bank).SingleOrDefaultAsync(t => t.TransactionID == id);
                if (transactionModel == null)
                {
                    return NotFound();
                }

                var vmodel = new TransactionVM
                {
                    TransactionID = transactionModel.TransactionID,
                    AccountNumber = transactionModel.AccountNumber,
                    BeneficiaryName = transactionModel.BeneficiaryName,
                    SWIFTCode = transactionModel.SWIFTCode,
                    Amount = transactionModel.Amount,
                    BankID = transactionModel.Bank.ID,
                    Banks = FillBanksList()
                };

                return View(vmodel);
            }
        }

        private List<Bank> FillBanksList()
        {
            var banks = _context.Banks.ToList();
            banks.Insert(0, new Bank { ID = -1, BankName = "--Please select--" });
            return banks;
        }

        // POST: Transaction/AddOrEdit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, TransactionVM model)
        //, [Bind("TransactionID,AccountNumber,BeneficiaryName,BankName,SWIFTCode,Amount,Date")] TransactionModel transactionModel)
        {
            //if (id != transactionModel.TransactionID)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    var transactionModel = new TransactionModel
                    {
                        AccountNumber = model.AccountNumber,
                        BeneficiaryName = model.BeneficiaryName,
                        SWIFTCode = model.SWIFTCode,
                        Amount = model.Amount,
                        Date = DateTime.Now,
                        Bank = _context.Banks.Find(model.BankID)
                    };

                    _context.Add(transactionModel);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    try
                    {
                        var transactionModel = new TransactionModel
                        {
                            TransactionID = model.TransactionID,
                            AccountNumber = model.AccountNumber,
                            BeneficiaryName = model.BeneficiaryName,
                            SWIFTCode = model.SWIFTCode,
                            Amount = model.Amount,
                            Date = DateTime.Now,
                            Bank = _context.Banks.Find(model.BankID)
                        };

                        _context.Update(transactionModel);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TransactionModelExists(model.TransactionID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }


                return Json(
                    new
                    {
                        isValid = true,
                        html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Transactions.Include(b => b.Bank).ToList())
                    }
                );
                //return RedirectToAction(nameof(Index));
            }

            return Json(
                new
                {
                    isValid = false,
                    html = Helper.RenderRazorViewToString(this, "AddOrEdit", model)
                }
             );
            //return View(transactionModel);
        }

        // GET: Transaction/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionModel = await _context.Transactions.FirstOrDefaultAsync(m => m.TransactionID == id);
            if (transactionModel == null)
            {
                return NotFound();
            }

            return View(transactionModel);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transactionModel = await _context.Transactions.FindAsync(id);
            _context.Transactions.Remove(transactionModel);
            await _context.SaveChangesAsync();

            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Transactions.ToList()) });
        }

        private bool TransactionModelExists(int id)
        {
            return _context.Transactions.Any(e => e.TransactionID == id);
        }


    }
}
