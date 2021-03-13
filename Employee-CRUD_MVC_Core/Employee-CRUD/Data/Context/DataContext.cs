using Microsoft.EntityFrameworkCore;

namespace Employee_CRUD.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

       
    }
}
