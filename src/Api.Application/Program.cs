using Api.CrossCutting.DependencyInjection;
using Api.Domain.Security;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    {
        Version = "v1",
        Title = "Curso de Api C#",
        Description= "Arquitetura DDD",
        TermsOfService = new Uri("https://github.com/fernando2dias"),
        Contact = new OpenApiContact 
        {
            Name = "Fernando Dias Motta",
            Email = "fernando3dias@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/fernando2dias/")
        },
        License = new OpenApiLicense
        {
            Name = "Termo de Licença de Uso",
            Url = new Uri("https://www.linkedin.com/in/fernando2dias/")
        }
    });
});




// TODO futuramente usar a injeção utilizando o crosscutting
ConfigureService.ConfigureDependenciesService(builder.Services);
ConfigureRepository.ConfigureDependenciesRepository(builder.Services);

var signingConfigurations = new SigningConfigurations();
builder.Services.AddSingleton(signingConfigurations);

var tokenConfigurations = new TokenConfigurations();
new ConfigureFromConfigurationOptions<TokenConfigurations>(
    builder.Configuration.GetSection("TokenConfigurations"))
    .Configure(tokenConfigurations);
builder.Services.AddSingleton(tokenConfigurations);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Curso de API .net DDD");
    c.RoutePrefix = string.Empty;
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
