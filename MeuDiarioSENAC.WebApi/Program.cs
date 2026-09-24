using System.Text.Json.Serialization;
using MeuDiarioSENAC.Classes;
using MeuDiarioSENAC.Data;
using MeuDiarioSENAC.Service;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

var registrosGroup = app.MapGroup("/registros");

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
