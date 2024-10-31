var builder = WebApplication.CreateBuilder(args);

//Add services to container.
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});


var app = builder.Build();

//Configura the HTTP request pipeline
app.MapCarter();

app.Run();

