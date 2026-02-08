using primeira_api.Models;

namespace primeira_api.Endpoints;

public static class UsuariosEndpoints
{
    public static void MapUsuariosEndpoints(this WebApplication app, List<Usuario> usuarios)
    {
        //POST: CREATE
        app.MapPost("/usuarios", (Usuario usuario) =>
        {
            if (string.IsNullOrWhiteSpace(usuario.Nome))
                return Results.BadRequest("Nome é obrigatório");

            if (usuarios.Any(u => u.Id == usuario.Id))
                return Results.Conflict("Id de usuário já está sendo utilizado");

            usuarios.Add(usuario);
            return Results.Created($"/usuarios/{usuario.Id}", usuario);
        });

        //GET: READ
        app.MapGet("/usuarios", () => usuarios);

        app.MapGet("/usuarios/{id}", (int id) =>
        {
            var usuario = usuarios.FirstOrDefault(u => u.Id == id);

            if (usuario == null)
                return Results.NotFound(); 

            return Results.Ok(usuario);
        });

        //PUT: UPDATE
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

        //DELETE
        app.MapDelete("/usuarios/{id}", (int id) =>
        {
            var usuario = usuarios.FirstOrDefault(u => u.Id == id);

            if (usuario == null)
                return Results.NotFound("Usuário não encontrado");

            usuarios.Remove(usuario);
            return Results.NoContent();
        });
    }
}
