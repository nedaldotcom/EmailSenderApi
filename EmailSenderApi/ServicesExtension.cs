using EmailSenderApi.Interface;
using EmailSenderApi.Models;
using EmailSenderApi.Services;

namespace EmailSenderApi;

public static class ServicesExtension
{
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        using var serviceProvider = builder.Services.BuildServiceProvider();
        var configuration = serviceProvider.GetService<IConfiguration>();
        if (configuration is null) return builder;
        
        var configOptions = configuration.GetSection("EmailSenderOptions");
        builder.Services.Configure<EmailSenderOptions>(configOptions);
        
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        
        builder.Services.AddTransient<IEmailSender, EmailSender>();
        
        builder.Services.AddSwaggerGen();

        return builder;
    }

    public static WebApplication Use(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}