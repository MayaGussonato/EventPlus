using EventPlus.WebAPI.BdContextEvet;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EventContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Registra os repositórios para injeção de dependência
builder.Services.AddScoped<ITipoEventoRepository, TipoEventoRepository>();

//Registra os repositórios para injeção de dependência
builder.Services.AddScoped<ITipoUsuarioRepository, TipoUsuarioRepository>();

//Registra os repositórios para injeção de dependência
builder.Services.AddScoped< IInstituicaoRepository, InstituicaoRepository>();

//Registra os repositórios para injeção de dependência
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

//Registra os repositórios para injeção de dependência
builder.Services.AddScoped<IEventoRepository, EventoRepository>();



builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
})
.AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        //  Validações
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        // MESMA CHAVE DO TOKEN
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes("event-chave-autenticacao-webapi-2026")
        ),

        //  PADRÃO IGUAL AO TOKEN
        ValidIssuer = "Event.WebAPI",
        ValidAudience = "Event.WebAPI",

        //  tolerância de tempo
        ClockSkew = TimeSpan.FromMinutes(5)
    };
});

//adiciona Swagger
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
options.SwaggerDoc("v1", new OpenApiInfo
{
    Version = "v1",
    Title = "API de Eventos",
    Description = "API para gerenciamento de eventos",
    TermsOfService = new Uri("https://example.com/terms"),
    Contact = new OpenApiContact
    {
        Name = "Mayara Gussonato",
        Url = new Uri("https://www.linkedin.com/in/mayara-gussonato-de-oliveira-silva-848899383/")
    },

    License = new OpenApiLicense
    {
        Name = "Exemplo de Licensa",
        Url = new Uri("https://exemple.com/license")
    }
});

// Usando a autenticacao no Swagger
options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
{
    Name = "Authorization",
    Type = SecuritySchemeType.Http,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "Insira o token JWT"
});

options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference ("Bearer", document)] = Array.Empty<string>().ToList()
                 
    });

});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger(options => { });

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty; 
    });

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


