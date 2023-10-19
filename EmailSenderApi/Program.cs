using EmailSenderApi;

var builder = WebApplication.CreateBuilder(args);

var app = builder.AddServices().Build();
app.Use().Run();