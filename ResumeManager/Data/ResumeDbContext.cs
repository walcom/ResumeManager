using Microsoft.EntityFrameworkCore;
using ResumeManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResumeManager.ViewModel;

namespace ResumeManager.Data
{
    public class ResumeDbContext : DbContext
    {
        public ResumeDbContext(DbContextOptions<ResumeDbContext> options) : base(options)
        {
        }


        public virtual DbSet<Applicant> Applicants { get; set; }
        public virtual DbSet<Experience> Experiences { get; set; }

        public virtual DbSet<TransactionModel> Transactions { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }


        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<OCustomer> OCustomers { get; set; }
        public virtual DbSet<SaleInvoice> SaleInvoices { get; set; }
        public virtual DbSet<SalesInvoiceDetails> SaleInvoiceDetails { get; set; }
        //public DbSet<ResumeManager.ViewModel.SaleInvoiceVM> SaleInvoiceVM { get; set; }


    }
}
