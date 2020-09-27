using System;
using System.Collections.Generic;
using System.Text;

namespace BlogNetCore.DataAccess.Data.Repository
{
    public interface IUnitOfWork: IDisposable
    {
        ICategorysRepository Category { get; }
        void Save();
    }
}
