using sharpcoder2_TechLife_Coinnecta_Backend.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace sharpcoder2_TechLife_Coinnecta_Backend.Domain
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
    : base(options){
    }

    
    }
}