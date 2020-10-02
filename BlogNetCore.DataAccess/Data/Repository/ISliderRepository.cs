using BlogNetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogNetCore.DataAccess.Data.Repository
{
    public interface ISliderRepository: IRepository<Slider>
    {
        void Update(Slider slider);
    }
}
