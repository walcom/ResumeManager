using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResumeManager.Data;
using ResumeManager.Models;
using ResumeManager.ViewModel;

namespace ResumeManager.Controllers
{
    public class SaleInvoicesController : Controller
    {
        private readonly ResumeDbContext _context;

        public SaleInvoicesController(ResumeDbContext context)
        {
            _context = context;
        }

        // GET: SaleInvoices
        public async Task<IActionResult> Index()
        {
            return View(await _context.SaleInvoices.Include(c => c.Customer).ToListAsync());
        }



        private List<OCustomer> FillCustomerssList()
        {
            var customers = _context.OCustomers.ToList();
            customers.Insert(0, new OCustomer {  CustomerID = -1,  Name= "--Please select--" });
            return customers;
        }



        // GET: SaleInvoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleInvoiceVM = await _context.SaleInvoices
                .FirstOrDefaultAsync(m => m.SalesInvoiceID == id);
            if (saleInvoiceVM == null)
            {
                return NotFound();
            }

            return View(saleInvoiceVM);
        }

        // GET: SaleInvoices/Create
        public IActionResult Create()
        {
            SaleInvoiceVM model = new SaleInvoiceVM
            {
                SalesInvoiceDateString = DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                Customers = FillCustomerssList()
            };

            return View(model);
        }

        // POST: SaleInvoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalesInvoiceID,SalesInvoiceCode,SalesInvoiceDate,CustomerID,Remarks,VatAmount")] SaleInvoiceVM saleInvoiceVM)
        {
            if (ModelState.IsValid)
            {
                _context.Add(saleInvoiceVM);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(saleInvoiceVM);
        }

        // GET: SaleInvoices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleInvoiceVM = await _context.SaleInvoices.FindAsync(id);
            if (saleInvoiceVM == null)
            {
                return NotFound();
            }
            return View(saleInvoiceVM);
        }

        // POST: SaleInvoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SalesInvoiceID,SalesInvoiceCode,SalesInvoiceDate,CustomerID,Remarks,VatAmount")] SaleInvoiceVM saleInvoiceVM)
        {
            if (id != saleInvoiceVM.SalesInvoiceID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(saleInvoiceVM);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleInvoiceVMExists(saleInvoiceVM.SalesInvoiceID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(saleInvoiceVM);
        }

        // GET: SaleInvoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleInvoiceVM = await _context.SaleInvoices
                .FirstOrDefaultAsync(m => m.SalesInvoiceID == id);
            if (saleInvoiceVM == null)
            {
                return NotFound();
            }

            return View(saleInvoiceVM);
        }

        // POST: SaleInvoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var saleInvoiceVM = await _context.SaleInvoices.FindAsync(id);
            _context.SaleInvoices.Remove(saleInvoiceVM);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaleInvoiceVMExists(int id)
        {
            return _context.SaleInvoices.Any(e => e.SalesInvoiceID == id);
        }
    }
}
