using CustomerAPI.Data;
using CustomerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // In-memory database:
            services.AddDbContext<CustomerApiContext>(opt => opt.UseInMemoryDatabase("CustomersDb"));

            // Register repositories for dependency injection
            services.AddScoped<IRepository<CustomerModel>, CustomerRepository>();

            // Register database initializer for dependency injection
            services.AddTransient<IDbInitializer, DbInitializer>();

            services.AddSwaggerGen();

            services.AddControllers();
        }
    }
}
