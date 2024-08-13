using BlazorCrudApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrudApp.Data;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
	{

	}

	public DbSet<CustomerModel> Customers_By_Sarwar { get; set; }
}
