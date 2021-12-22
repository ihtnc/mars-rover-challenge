using MarsRoverChallenge.Apps.Web;
using MarsRoverChallenge.Apps.Web.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("LocalDevPolicy", policy =>
    {
        policy.AllowAnyHeader()
            .AllowAnyMethod()
            .WithOrigins("http://localhost:6001")
            .AllowCredentials();
    });
});

builder.Services.AddSignalR();
builder.Services.AddDependencies();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("LocalDevPolicy");
}

app.UseAuthorization();

app.MapHub<RoverHub>("/hub");
app.MapControllers();

app.Run();
