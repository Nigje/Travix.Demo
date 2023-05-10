using Microsoft.EntityFrameworkCore;
using System;
using Travix.Common.Middlewares;
using Travix.Common.Models;
using Travix.Common.ORM.EntityFramework;
using Travix.Common.ORM.Models;
using Travix.Common.ORM.Models.Impl;
using Travix.DB;
using Travix.Demo.Services;
using Travix.Demo.Services.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddTransient<RequestContext, RequestContext>();
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<DemoDBContext, DemoDBContext>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TravixDBContext, DemoDBContext>(opt => opt.UseInMemoryDatabase("Travix.Demo"), ServiceLifetime.Transient);

var app = builder.Build();

//Generate database.
using (var scope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    var _unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();
    _unitOfWork.GenericRepository<User>().Add(new User { Email = "a@b.com", Name = "John" });
    _unitOfWork.GenericRepository<User>().Add(new User { Email = "c@d.com", Name = "Sara" });
    _unitOfWork.SaveAsync();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseMiddleware<HttpStatusCodeMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();
app.Run();
