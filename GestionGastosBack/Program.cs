using GestionGastosBack;
using GestionGastosBD;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GestionGastosBDContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("GestionGastosConnection"))
);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("*")
                .AllowAnyHeader()
                .AllowAnyMethod();
            // .WithMethods("GET", "PUT", "POST", "OPTIONS", "HEAD", "DELETE", "PATCH");
            //  .WithHeaders("Content-Type", "Authorization");
        });
});

builder.Services.AddAuthorization();

var app = builder.Build();
GlobalFunctions globalFunctions;

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<GestionGastosBDContext>();

    context.Database.Migrate();

    globalFunctions = new GlobalFunctions(context);

    globalFunctions.InsertItems(pm => pm.name, Constants.paymentMethods.ToList());
    
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
