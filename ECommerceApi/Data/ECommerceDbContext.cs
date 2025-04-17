using Microsoft.EntityFrameworkCore;

namespace ECommerceApi.Data;

public class ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : DbContext(options)
{
    
}