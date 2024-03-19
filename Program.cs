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

// Vérification de l'environnement pour configurer les services spécifiques au test
if (builder.Environment.IsEnvironment("Test"))
{
    // Configuration de la connexion à la base de données pour les tests
    builder.Services.ConfigureDBContextTest();

    // Configuration de l'injection de dépendances pour les tests
    builder.Services.ConfigureInjectionDependencyRepositoryTest();
    builder.Services.ConfigureInjectionDependencyServiceTest();
}
else
{
    // Configuration de la connexion à la base de données pour l'environnement de production
    builder.Services.ConfigureDBContext(configuration);

    // Configuration de l'injection de dépendances pour l'environnement de production
    builder.Services.ConfigureInjectionDependencyRepository();
    builder.Services.ConfigureInjectionDependencyService();
}

// Configuration d'AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Configuration de l'identité utilisateur
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<FoodiesDBContext>()
    .AddDefaultTokenProviders();

// Configuration des options d'identité
builder.Services.Configure<IdentityOptions>(
    options => options.SignIn.RequireConfirmedEmail = true
);

// Configuration de l'authentification JWT
builder.Services.AddAuthentication(options =>
{
    // Spécification des schémas d'authentification par défaut
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Configuration du JWT Bearer
.AddJwtBearer(options =>
{
    // Indique que le jeton JWT reçu dans la requête doit être sauvegardé
    options.SaveToken = true;
    // Autorise l'utilisation de JWT sans HTTPS
    options.RequireHttpsMetadata = false;
    // Paramètres de validation du jeton JWT
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        // Indique si l'émetteur et l'auditeur du jeton doivent être validés
        ValidateIssuer = true,
        ValidateAudience = true,
        // Valeurs attendues pour l'auditeur et l'émetteur du jeton
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        // Clé de signature utilisée pour vérifier l'authenticité du jeton
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});


// Configuration du service de fichiers
builder.Services.AddTransient<IFileService, FileService>();

// Configuration des paramètres de l'email
var emailConfig = configuration
    .GetSection("EmailConfiguration")
    .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<IEmailService, EmailService>();


// Configuration permettant à Serilog d'utiliser la configuration fournie par l'hôte.
builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
}
);

// Ajout des contrôleurs
builder.Services.AddControllers();

// Configuration de Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen
(
    option =>
    {
        option.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth API", Version = "v1" });

        // Ajoute une définition de sécurité pour les jetons JWT.
        option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });
        // Ajoute une exigence de sécurité pour les jetons JWT.
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

// Configuration de CORS pour autoriser les demandes depuis un domaine spécifique.
builder.Services.AddCors(options =>
{
    // Ajout d'une politique nommée "_myAllowSpecificOrigins".
    options.AddPolicy("_myAllowSpecificOrigins",
        // Configuration des autorisations pour cette politique.
        builder => builder
            // Autorise les requêtes depuis le domaine "http://localhost:3000".
            .WithOrigins("http://localhost:3000")
            // Autorise toutes les en-têtes dans les requêtes.
            .AllowAnyHeader()
            // Autorise toutes les méthodes HTTP (GET, POST, etc.).
            .AllowAnyMethod()
            // Indique que les cookies peuvent être inclus dans les requêtes CORS.
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

// Activation du logging des requêtes avec Serilog
app.UseSerilogRequestLogging();

// Redirection HTTPS pour assurer la sécurité des communications
app.UseHttpsRedirection();

// Activation de la gestion de l'authentification des requêtes
app.UseAuthentication();

// Activation de la gestion des autorisations pour contrôler l'accès aux ressources
app.UseAuthorization();

// Activation de la gestion des CORS pour permettre les requêtes cross-origin spécifiques
app.UseCors("_myAllowSpecificOrigins");

// Mappage des contrôleurs MVC pour le traitement des requêtes HTTP
app.MapControllers();

// Exécution de l'application
app.Run();
