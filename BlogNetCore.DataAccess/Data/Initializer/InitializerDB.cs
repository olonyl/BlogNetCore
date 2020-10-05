using BlogNetCore.Models;
using BlogNetCore.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.Xml;
using System.Text;

namespace BlogNetCore.DataAccess.Data.Initializer
{
    public class InitializerDB : IInitalizerDB
    {
        private readonly ApplicationDbContext db;
        private UserManager<ApplicationUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        public InitializerDB(ApplicationDbContext db, 
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public void Initialize()
        {
            try
            {
                if (db.Database.GetPendingMigrations().Count() > 0)
                {
                    db.Database.Migrate();
                }
            }
            catch(Exception ex){}

            if (!db.Roles.Any(a => a.Name == CNT.Admin)) roleManager.CreateAsync(new IdentityRole(CNT.Admin)).GetAwaiter().GetResult();
            if (!db.Roles.Any(a => a.Name == CNT.User)) roleManager.CreateAsync(new IdentityRole(CNT.User)).GetAwaiter().GetResult();

            const string email = "kakachi.landeros@gmail.com";
            const string password = "Temp123*";

            if (userManager.FindByEmailAsync(email).GetAwaiter().GetResult() == null)
            {   
             var restult = userManager.CreateAsync(new ApplicationUser()
                {
                    UserName = email,
                    Email =email,
                    EmailConfirmed = true,
                    Name ="Jose Landeros"
             }, password).GetAwaiter().GetResult();
                
            }
            ApplicationUser user = db.User.Where(w => w.Email == email).FirstOrDefault() ;
            if (!userManager.IsInRoleAsync(user, CNT.Admin).GetAwaiter().GetResult() )
                userManager.AddToRoleAsync(user, CNT.Admin);
       
        }
    }
}
