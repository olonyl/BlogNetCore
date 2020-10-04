using BlogNetCore.DataAccess.Data.Repository;
using BlogNetCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogNetCore.DataAccess.Data
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private readonly ApplicationDbContext db;
        
        public UserRepository(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }

        public void LockUser(string userId)
        {
            var user  = db.User.FirstOrDefault(u=> u.Id == userId);
            user.LockoutEnd = DateTime.Now.AddYears(100);
            db.SaveChanges();
        }

        public void UnlockUser(string userId)
        {
            var user = db.User.FirstOrDefault(u => u.Id == userId);
            user.LockoutEnd = DateTime.Now;
            db.SaveChanges();
        }
    }
}
