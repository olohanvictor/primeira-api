var builder = WebApplication.CreateBuilder(args);

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Swagger UI
app.UseSwagger();
app.UseSwaggerUI();

// "Banco de dados" em memória
var usuarios = new List<Usuario>();
var posts = new List<Post>();


//CRUD:
//CREATE 
//READ
//UPDATE
//DELETE

//Rotas
//POST: o CREATE
app.MapPost("/usuarios", (Usuario usuario) =>
{
    if (string.IsNullOrWhiteSpace(usuario.Nome))
        return Results.BadRequest("Nome é obrigatório");

    if (usuarios.Any(u => u.Id == usuario.Id))
        return Results.Conflict("Id de usuário já está sendo utilizado");

    usuarios.Add(usuario);
    return Results.Created($"/usuarios/{usuario.Id}", usuario);
});


app.MapPost("/usuarios/{idAutor}/posts", (int idAutor, Post post) =>
{
    var usuarioExiste = usuarios.Any(u => u.Id == idAutor);

    if (!usuarioExiste)
        return Results.NotFound("Usuário não existe");

    post.IdAutor = idAutor;
    post.Id = posts.Count + 1;

    posts.Add(post);

    return Results.Created($"/posts/{post.Id}", post);
});

//GET: o READ
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

app.MapGet("/posts", () =>
{
    return posts;
});

//PUT: O UPDATE
app.MapPut("/usuarios/{id}", (int id, Usuario usuarioAtualizado) =>
{
    var usuario = usuarios.FirstOrDefault(u => u.Id == id);

    if (usuario == null)
        return Results.NotFound("Usuário não encontrado");

    if (string.IsNullOrWhiteSpace(usuarioAtualizado.Nome))
        return Results.BadRequest("Nome é obrigatório");

    usuario.Nome = usuarioAtualizado.Nome;
    return Results.Ok(usuario);
});

app.MapDelete("/usuarios/{id}", (int id) =>
{
    var usuario = usuarios.FirstOrDefault(u => u.Id == id);

    if (usuario == null)
        return Results.NotFound("Usuário não encontrado");

    usuarios.Remove(usuario);
    return Results.NoContent();
});


app.Run();

// Modelo
public class Usuario
{
    public int Id { get; set; }
    public required string Nome { get; set; }
}

public class Post
{
    public int Id { get; set; }
    public int IdAutor { get; set; }
    public required string Conteudo { get; set; }
}