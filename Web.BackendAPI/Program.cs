using Web.BackendAPI.Extensions;
using Web.BackendAPI.Middlewares;
using Web.Business;
using Web.DataAccessor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthenticationRegister();
//Add service register in another project
builder.Services.AddDataAccessorLayer(builder.Configuration);
builder.Services.AddBusinessLayer();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerRegister();
builder.Services.AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseCors(builder =>
   {
       builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
   });
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
