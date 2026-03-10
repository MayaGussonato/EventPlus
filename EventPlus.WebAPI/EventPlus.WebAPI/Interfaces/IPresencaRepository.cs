using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Interfaces;

public interface IPresencaRepository
{
    void Increver(Presenca presenca);
    void Deletar(Guid id);
    List<Presenca> Listar();
    Presenca BuscarPorId(Guid IdUsuario, Guid IdEvento);
    void Atualizar(Guid id);
    List<Presenca>ListarMinhas(Guid IdUsuario);

}
