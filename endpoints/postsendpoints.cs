using primeira_api.Models;

namespace primeira_api.Endpoints;

public static class PostsEndpoints
{
    public static void MapPostsEndpoints(
        this WebApplication app,
        List<Usuario> usuarios,
        List<Post> posts)
    {
        //POST: CREATE
        app.MapPost("/usuarios/{idAutor}/posts", (int idAutor, Post post) =>
        {
            if (!usuarios.Any(u => u.Id == idAutor))
                return Results.NotFound("Usuário não existe");

            post.IdAutor = idAutor;
            post.Id = posts.Count + 1;

            posts.Add(post);
            return Results.Created($"/posts/{post.Id}", post);
        });

        //GET: READ
        app.MapGet("/posts", () => posts);

        app.MapGet("/posts/{id}", (int id) =>
        {
            var post = posts.FirstOrDefault(u => u.Id == id);

            if (post == null)
                return Results.NotFound();

            return Results.Ok(post);
        });
    }
}
