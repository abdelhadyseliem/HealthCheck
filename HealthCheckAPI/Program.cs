global using HealthCheckAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks()
    .AddCheck("Facebook", new ICMPHealthCheck("www.facebook.com", 100))
    .AddCheck("Google", new ICMPHealthCheck("www.google.com", 100))
    .AddCheck("Does Not Exist", new ICMPHealthCheck($"www.does-not-exist.com", 100));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseHealthChecks(new PathString("/api/health"), new CustomHealthCheckOptions());

app.MapControllers();

app.Run();