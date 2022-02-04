using Microsoft.EntityFrameworkCore;

namespace DraplusApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}