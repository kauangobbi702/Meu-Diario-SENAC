using System.Text;
using System.Text.Json.Serialization;
using MeuDiarioSENAC.Classes;
using MeuDiarioSENAC.Data;
using MeuDiarioSENAC.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var jwtSettings = builder.Configuration.GetSection("JWTSettings");
        
var chaveSecretaBytes = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
}).AddJwtBearer("JwtBearer", options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(chaveSecretaBytes),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapPost("/login/auth", ([FromBody] Usuario usuario) =>
{
   TokenService authService = new TokenService(builder.Configuration);
   return authService.GerarToken(usuario);
});

var registrosGroup = app.MapGroup("/registros").RequireAuthorization();

app.MapGet("/", () => "EAEEEEEEEEE!");

app.MapGet("/motivacional", () => "Lembre-se: Nada é tão ruim que não possa piorar!");

registrosGroup.MapGet("/listar", () => {
    List<Registro> registros = new RegistroService().ListarRegistros(3);
    return registros;
});

registrosGroup.MapGet("/listar-data", (int idUsuario, DateOnly data) =>
{
    List<Registro> registrosPorData = new RegistroService().BuscarRegistroData(idUsuario, data);
    return registrosPorData;
});

registrosGroup.MapPost("/postar", ([FromBody] Registro registro) =>
{
    new RegistroService().AdicionarRegistro(registro);
    return "Registro inserido com sucesso!";
});

registrosGroup.MapPut("/editar", ([FromBody] Registro registro) =>
{
    new RegistroService().EditarRegistro(registro.Id, registro.Titulo, registro.Conteudo);
    return "Registro editado com sucesso!";
});

registrosGroup.MapDelete("/excluir", ([FromBody] Registro registro) =>{
    new RegistroService().RemoverRegistro(registro);
    return "Registro excluido com sucesso!";
});



app.Run();
