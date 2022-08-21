internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            c.IgnoreObsoleteActions();
            c.IgnoreObsoleteProperties();
            c.CustomSchemaIds(type => type.FullName);
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseDeveloperExceptionPage();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseRouting();

        app.UseStaticFiles();

        app.MapControllers();

        app.Run();
    }
}