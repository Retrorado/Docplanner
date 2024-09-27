using Docplanner.Application.Doctors.Availability.Queries;
using Docplanner.Contracts.Queries.Doctors.Availability;
using Docplanner.Infrastructure;
using DocplannerApi.ErrorHandling;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddRouting();

builder.Services.AddValidatorsFromAssemblyContaining<GetDoctorWeeklyAvailability>();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblyContaining<GetDoctorWeeklyAvailabilityHandler>();
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services
    .AddExceptionHandler<GlobalExceptionHandler>()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseExceptionHandler(_ => { });
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();