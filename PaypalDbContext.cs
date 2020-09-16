using Microsoft.EntityFrameworkCore;

public class PaypalDbContext:DbContext
{
    public PaypalDbContext(DbContextOptions<PaypalDbContext> optionsBuilder)
        :base(optionsBuilder)
    {
        
    }
    

    public DbSet<PaypalToken> PaypalToken{get;set;}
}