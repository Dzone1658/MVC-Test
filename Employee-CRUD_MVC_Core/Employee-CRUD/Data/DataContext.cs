using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Employee_CRUD.Models;

using Microsoft.EntityFrameworkCore;

namespace Employee_CRUD.Data
{
    public class DataContext : DbContext
    {
        public DataContext() : base()
        { }
    }
}
