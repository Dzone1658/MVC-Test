using Microsoft.EntityFrameworkCore;
using Employee_CRUD.Data.Entities;

namespace Employee_CRUD.Data.Context
{
    public class DataContext : DbContext
    {
        public DbSet<TBL_Tags> TBL_Tags { get; set; }
        public DbSet<TBL_Category> TBL_Category { get; set; }
        public DbSet<TBL_PostTags> TBL_PostTags { get; set; }
        public DbSet<TBL_PublicPost> TBL_PublicPost { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }
    }
}
