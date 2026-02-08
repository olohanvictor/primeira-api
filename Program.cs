using primeira_api.Endpoints;
using primeira_api.Models;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// "Banco de dados" em memória
var usuarios = new List<Usuario>();
var posts = new List<Post>();

// Mapear endpoints
app.MapUsuariosEndpoints(usuarios);
app.MapPostsEndpoints(usuarios, posts);

app.MapGet("/", () => "API rodando 🚀");

app.Run();
