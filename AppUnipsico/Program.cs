using AppUnipsico.Data.Context;
using AppUnipsico.Models;
using AppUnipsico.Repositories;
using AppUnipsico.Services.Impl;
using AppUnipsico.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("App");

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<DataDisponivelService>();
builder.Services.AddScoped<ConsultaRepository>();
builder.Services.AddScoped<DataDisponivelRepository>();
builder.Services.AddScoped<ICriaUsuarioRoleInicialService, CriaUsuarioRoleInicialService>();
builder.Services.AddScoped<IDataDisponivelService, DataDisponivelService>();

builder.Services.AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<AppUnipsicoDb>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAlunoRole", policy => policy.RequireRole("Aluno"));
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
    options.AddPolicy("RequireProfessorAlunoRole", policy => policy.RequireRole("Professor", "Aluno"));
    options.AddPolicy("RequireAdminProfessorRole", policy => policy.RequireRole("Admin", "Professor"));
});

builder.Services.AddDbContext<AppUnipsicoDb>(opt =>
{
    opt.UseSqlServer(connectionString);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

CriarPerfisUsuarios(app);
void CriarPerfisUsuarios(WebApplication app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<ICriaUsuarioRoleInicialService>();
        service.CriaRoles();
        service.CriaUsuarios();
    }
}

app.MapControllerRoute(
  name: "areas",
  pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Aluno}/{action=Index}/{id?}"
    );

app.Run();
