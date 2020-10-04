using BlogNetCore.DataAccess.Data.Repository;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogNetCore.DataAccess.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext db;
        public ICategorysRepository Category { get; private set; }
        public IArticleRepository Article { get; private set; }
        public ISliderRepository Slider {get; private set;}
        public IUserRepository User {get; private set;}
        public UnitOfWork(ApplicationDbContext db)
        {
            this.db = db;
            Category = new CategoryRepository(db);
            Article = new ArticleRepository(db);
            Slider = new SliderRepository(db);
            User = new UserRepository(db);
        }
        public void Dispose()
        {
            db.Dispose();
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
