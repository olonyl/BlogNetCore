using BlogNetCore.DataAccess.Data.Repository;
using BlogNetCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogNetCore.DataAccess.Data
{
    public class ArticleRepository : Repository<Article>, IArticleRepository
    {
        private readonly ApplicationDbContext db;
        public ArticleRepository(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }
    }
}
