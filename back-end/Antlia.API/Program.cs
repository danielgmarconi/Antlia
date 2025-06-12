using Antlia.Infra.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(c => {  c.AllowAnyHeader(); 
                    c.AllowAnyMethod();
                    c.AllowAnyOrigin(); 
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
