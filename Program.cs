using Foodies.Api.Business.Services;
using Foodies.Api.Business.Services.Interfaces;
using Foodies.Api.Common.Services;
using Foodies.Api.Data;
using Foodies.Api.Data.Models;
using Foodies.Api.IoC.IoCApplication;
using Foodies.Api.IoC.IoCTest;
using Foodies.Common.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

// V�rification de l'environnement pour configurer les services sp�cifiques au test
if (builder.Environment.IsEnvironment("Test"))
{
    // Configuration de la connexion � la base de donn�es pour les tests
    builder.Services.ConfigureDBContextTest();

    // Configuration de l'injection de d�pendances pour les tests
    builder.Services.ConfigureInjectionDependencyRepositoryTest();
    builder.Services.ConfigureInjectionDependencyServiceTest();
}
else
{
    // Configuration de la connexion � la base de donn�es pour l'environnement de production
    builder.Services.ConfigureDBContext(configuration);

    // Configuration de l'injection de d�pendances pour l'environnement de production
    builder.Services.ConfigureInjectionDependencyRepository();
    builder.Services.ConfigureInjectionDependencyService();
}

// Configuration d'AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Configuration de l'identit� utilisateur
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<FoodiesDBContext>()
    .AddDefaultTokenProviders();

// Configuration des options d'identit�
builder.Services.Configure<IdentityOptions>(
    options => options.SignIn.RequireConfirmedEmail = true
);

// Configuration de l'authentification JWT
builder.Services.AddAuthentication(options =>
{
    // Sp�cification des sch�mas d'authentification par d�faut
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Configuration du JWT Bearer
.AddJwtBearer(options =>
{
    // Indique que le jeton JWT re�u dans la requ�te doit �tre sauvegard�
    options.SaveToken = true;
    // Autorise l'utilisation de JWT sans HTTPS
    options.RequireHttpsMetadata = false;
    // Param�tres de validation du jeton JWT
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        // Indique si l'�metteur et l'auditeur du jeton doivent �tre valid�s
        ValidateIssuer = true,
        ValidateAudience = true,
        // Valeurs attendues pour l'auditeur et l'�metteur du jeton
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        // Cl� de signature utilis�e pour v�rifier l'authenticit� du jeton
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});


// Configuration du service de fichiers
builder.Services.AddTransient<IFileService, FileService>();

// Configuration des param�tres de l'email
var emailConfig = configuration
    .GetSection("EmailConfiguration")
    .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<IEmailService, EmailService>();


// Configuration permettant � Serilog d'utiliser la configuration fournie par l'h�te.
builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
}
);

// Ajout des contr�leurs
builder.Services.AddControllers();

// Configuration de Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen
(
    option =>
    {
        option.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth API", Version = "v1" });

        // Ajoute une d�finition de s�curit� pour les jetons JWT.
        option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });
        // Ajoute une exigence de s�curit� pour les jetons JWT.
        option.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id="Bearer"
                    }
                },
                new string[] {}
            }
        });
    }
);

// Configuration de CORS pour autoriser les demandes depuis un domaine sp�cifique.
builder.Services.AddCors(options =>
{
    // Ajout d'une politique nomm�e "_myAllowSpecificOrigins".
    options.AddPolicy("_myAllowSpecificOrigins",
        // Configuration des autorisations pour cette politique.
        builder => builder
            // Autorise les requ�tes depuis le domaine "http://localhost:3000".
            .WithOrigins("http://localhost:3000")
            // Autorise toutes les en-t�tes dans les requ�tes.
            .AllowAnyHeader()
            // Autorise toutes les m�thodes HTTP (GET, POST, etc.).
            .AllowAnyMethod()
            // Indique que les cookies peuvent �tre inclus dans les requ�tes CORS.
            .AllowCredentials());
});


// Construction de l'application
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
        c =>
    {
        c.ConfigObject.AdditionalItems.Add("persistAuthorization", "true");
    }
    );
}

// Activation du logging des requ�tes avec Serilog
app.UseSerilogRequestLogging();

// Redirection HTTPS pour assurer la s�curit� des communications
app.UseHttpsRedirection();

// Activation de la gestion de l'authentification des requ�tes
app.UseAuthentication();

// Activation de la gestion des autorisations pour contr�ler l'acc�s aux ressources
app.UseAuthorization();

// Activation de la gestion des CORS pour permettre les requ�tes cross-origin sp�cifiques
app.UseCors("_myAllowSpecificOrigins");

// Mappage des contr�leurs MVC pour le traitement des requ�tes HTTP
app.MapControllers();

// Ex�cution de l'application
app.Run();
