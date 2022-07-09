using Microsoft.EntityFrameworkCore;

namespace ApiXMen.Models
{
    public class XMenContext : DbContext
    {
        public XMenContext(DbContextOptions<XMenContext> options) : base(options)
        {
        }
   
        public DbSet<DnaResult> DnaResults { get; set; }
    }
}
