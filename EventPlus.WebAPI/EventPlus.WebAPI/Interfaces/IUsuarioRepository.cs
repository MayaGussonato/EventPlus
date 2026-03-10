using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Interfaces;

public interface IUsuarioRepository
{
    List<TipoEvento> Listar();
    void Cadastrar(Usuario usuario);
    Usuario BuscarPorId(Guid id);
    Usuario BuscarPorEmailESenha (string email, string senha);
     void Atualizar(Guid id, Usuario usuario);
}
