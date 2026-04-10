using DotNetEnv;
using Gestao_Patrimonios.Applications.Autenticacao;
using Gestao_Patrimonios.Applications.Services;
using Gestao_Patrimonios.Contexts;
using Gestao_Patrimonios.Interfaces;
using Gestao_Patrimonios.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// CARREGA O .env
Env.Load();

// PEGA A CONNECTION STRING DO .env
string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

// CONECTA AO BANCO COM A CONNECTION STRING DO .env
builder.Services.AddDbContext<Gestao_PatrimoniosContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// JWT NO SWAGGER
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Value: Bearer TokenJWT"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// USUÁRIO
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<UsuarioService>();

// CARGO
builder.Services.AddScoped<ICargoRepository, CargoRepository>();
builder.Services.AddScoped<CargoService>();

// PATRIMÔNIO
builder.Services.AddScoped<IPatrimonioRepository, PatrimonioRepository>();
builder.Services.AddScoped<PatrimonioService>();

// SOLICITAÇÃO TRANSFERÊNCIA
builder.Services.AddScoped<ISolicitacaoTransferenciaRepository, SolicitacaoTransferenciaRepository>();
builder.Services.AddScoped<SolicitacaoTransferenciaService>();

// STATUS PATRIMÔNIO
builder.Services.AddScoped<IStatusPatrimonioRepository, StatusPatrimonioRepository>();
builder.Services.AddScoped<StatusPatrimonioService>();

// LOG PATRIMÔNIO
builder.Services.AddScoped<ILogPatrimonioRepository, LogPatrimonioRepository>();
builder.Services.AddScoped<LogPatrimonioService>();

// ÁREA
builder.Services.AddScoped<IAreaRepository, AreaRepository>();
builder.Services.AddScoped<AreaService>();

// LOCALIZAÇÃO
builder.Services.AddScoped<ILocalizacaoRepository, LocalizacaoRepository>();
builder.Services.AddScoped<LocalizacaoService>();

// CIDADE
builder.Services.AddScoped<ICidadeRepository, CidadeRepository>();
builder.Services.AddScoped<CidadeService>();

// BAIRRO
builder.Services.AddScoped<IBairroRepository, BairroRepository>();
builder.Services.AddScoped<BairroService>();

// ENDEREÇO
builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();
builder.Services.AddScoped<EnderecoService>();

// JWT
builder.Services.AddScoped<GeradorTokenJwt>();
builder.Services.AddScoped<AutenticacaoService>();

// Configura o sistema de autenticação da aplicação.
// Habilita o JWT como autenticação padrão.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)

    // Adiciona o suporte para autenticação usando JWT.
    .AddJwtBearer(options =>
    {
        // Lê a chave secreta definida no appsettings.json.
        var chave = Environment.GetEnvironmentVariable("JWT_KEY");
        //var chave = builder.Configuration["Jwt:Key"]!;

        // Quem emitiu o token.
        var issuer = builder.Configuration["Jwt:Issuer"]!;

        // Para quem o token foi criado.
        var audience = builder.Configuration["Jwt:Audience"]!;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Verifica se o emissor do token é válido.
            ValidateIssuer = true,

            // Verifica se o destinatário do token é válido.
            ValidateAudience = true,

            // Verifica se o token ainda está válido.
            ValidateLifetime = true,

            // Verifica se a assinatura do token é válida.
            ValidateIssuerSigningKey = true,

            // Define qual emissor é considerado válido.
            ValidIssuer = issuer,

            // Define qual audience é considerada válida.
            ValidAudience = audience,

            // Define qual chave será usada para validar a assinatura do token.
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(chave)
            ),

            // O token geralmente tem 5 minutos de tolerância
            // Remove tolerância extra no vencimento do token
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// AUTHENTICATION: HABILITA AUTENTICAÇÃO DO SWAGGER (CONFIGURADA NA LINHA 27)
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();