using Taxually.TechnicalTest.Services;
using Taxually.TechnicalTest.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// NOTE: Usually scoped is enough, without further domain knowledge
// transient mimics the behavior of the original code
builder.Services.AddTransient<IDomainHttpClient, TaxuallyHttpClient>();
builder.Services.AddTransient<IDomainQueueClient, TaxuallyQueueClient>();
builder.Services.AddTransient<ITaxuallyService, TaxuallyService>();
builder.Services.AddSingleton<IConfigurationService, ConstantConfigurationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
