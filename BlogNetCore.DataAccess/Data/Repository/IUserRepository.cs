using BlogNetCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogNetCore.DataAccess.Data.Repository
{
    public interface IUserRepository: IRepository<ApplicationUser>
    {
        void LockUser(string userId);
        void UnlockUser(string userId);
    }
}
