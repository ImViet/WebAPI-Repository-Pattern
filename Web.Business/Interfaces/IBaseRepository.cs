﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Business.Interfaces
{
    public interface IBaseRepository<T>
    {
        IQueryable<T> Entities { get; }
        Task<IEnumerable<T>> GetAll();
    }
}
