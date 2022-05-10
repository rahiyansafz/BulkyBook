﻿using BulkyBook.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.Contracts
{
    public interface ICoverTypeRepository : IRepository<CoverType>
    {
        void Update(CoverType category);
    }
}