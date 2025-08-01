﻿using Microsoft.EntityFrameworkCore;
using Terminal.Database.Entities;

namespace Terminal.Database
{
    public class TerminalDbContext : DbContext
    {
        public TerminalDbContext(DbContextOptions<TerminalDbContext> options)
            : base(options)
        {
        }
        public DbSet<CompanyEntity> Empresas { get; set; }
        public DbSet<BusEntity> Buses { get; set; }
        public DbSet<HorarioEntity> Horarios { get; set; }

    }
}
