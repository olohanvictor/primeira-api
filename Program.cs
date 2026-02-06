var builder = WebApplication.CreateBuilder(args);

// 🔹 Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 🔹 Swagger UI
app.UseSwagger();
app.UseSwaggerUI();

// "Banco de dados" em memória
var usuarios = new List<Usuario>();
var posts = new List<Post>();

// Rotas
//GET
app.MapGet("/", () =>
{
    return "API rodando";
});

app.MapGet("/hello", () =>
{
    return "Tu és digno, Senhor e Deus nosso, de receber a glória, a honra e o poder, porque criaste todas as coisas e por tua vontade elas vieram a existir e foram criadas."+
"\nApocalipse 4:11";
});

app.MapGet("/usuarios", () =>
{
    return usuarios;
});

app.MapGet("/usuarios/{id}", (int id) =>
{
    var usuario = usuarios.FirstOrDefault(u => u.Id == id);

    if (usuario == null)
        return Results.NotFound();

    return Results.Ok(usuario);
});

app.MapGet("/posts/{id}", (int id) =>
{
    var post = posts.FirstOrDefault(u => u.Id == id);

    if (post == null)
        return Results.NotFound();

    return Results.Ok(post);
});

//POST
app.MapPost("/usuarios", (Usuario usuario) =>
{
    usuarios.Add(usuario);
    return Results.Created($"/usuarios/{usuario.Id}", usuario);
});

app.MapPost("/posts", (Post post) => 
{
    posts.Add(post);
    return Results.Created($"/posts/{post.Id}", post);

});

//PUT
app.MapPut("/usuarios/{id}", (int id, Usuario usuarioAtualizado) =>
{
    var usuario = usuarios.FirstOrDefault(u => u.Id == id);
    usuario.Nome = usuarioAtualizado.Nome;

    return Results.Ok(usuario);
});


//DELETE
app.MapDelete("/usuarios/{id}", (int id) =>
{
    var usuario = usuarios.FirstOrDefault(u => u.Id == id);
    usuarios.Remove(usuario);

    return Results.NoContent();
});

app.Run();

// Modelo
public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; }
}

public class Post
{
    public int Id { get; set; }
    public int IdAutor { get; set; }
    public string Conteudo { get; set; }
}