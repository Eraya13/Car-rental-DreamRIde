﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Autopujcovna_DreamRide.Models; // maybe Rename

namespace Autopujcovna_DreamRide.Data
{
    public class AppDbContext : IdentityDbContext
    {
        // konstruktor databaze + options - rules nastavení - dodají se v Program.cs
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }



    }
}
