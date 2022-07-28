using System;
using CardsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CardsAPI.Data
{
    public class CardsDbContext: DbContext
    {
        public CardsDbContext(DbContextOptions options) : base(options)
        {
        }
        //Dbset
        public DbSet<Card> Cards { get; set; }
    }
}