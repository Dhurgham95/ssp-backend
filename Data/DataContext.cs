using Backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Data
{
    public class DataContext : IdentityDbContext<User>
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        } 
        public DbSet<AskBank> AskBanks {get;set;}

        public DbSet<PersonalAccount> PersonalAccounts { get; set; }    
        public DbSet<Bill> Bills { get; set; } 
        public DbSet<Good> Goods { get; set; } 
        public DbSet<InsuranceDoc> InsuranceDocs { get; set; } 
        public DbSet<NotPayAlarm> NotPayAlarms { get; set; } 
        public DbSet<NotPayMonth> NotPayMonths { get; set; } 
        public DbSet<InsuranceTimeToPay> InsuranceTimeToPays { get; set; } 

        public DbSet<BillT>  BillTs {get;set;} 

        public DbSet<SmsVerificationCode> SmsVerificationCodes {get;set;} 

        public DbSet<PartialPay> PartialPays {get;set;}

     //    public DbSet<Good> Goods {get;set;}
     //    public DbSet<Bill> Bills {get;set;}  

       //  public DbSet<Pledge> Pledges {get;set;}
         

       
    }
}
