using Microsoft.EntityFrameworkCore;

namespace app_da_foto.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        string MyAllowSpecificOrigin = "_myAllowSpecificOrigins";

        public IConfiguration Configuration { get; }

        // Add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddCors(options =>
            {
                options.AddPolicy("_myAllowSpecificOrigins",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    );
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(MyAllowSpecificOrigin);

            app.UseHttpsRedirection();

            app.UseAuthorization();

        }
    }
}

