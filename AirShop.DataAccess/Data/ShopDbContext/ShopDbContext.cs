﻿using AirShop.DataAccess.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AirShop.DataAccess.Data.ShopDbContext
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {
        }

        // Dodaj DbSet dla każdej tabeli w bazie danych
        public DbSet<Product> Products { get; set; }
        public DbSet<Code> Codes { get; set; }
        //public DbSet<AccessRight> AccessRights { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentPosition> DocumentPositions { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<ReceiptPosition> ReceiptPositions { get; set; }
        public DbSet<Contractor> Contractor { get; set; }
    }
}