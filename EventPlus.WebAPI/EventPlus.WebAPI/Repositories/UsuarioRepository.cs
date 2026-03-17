using EventPlus.WebAPI.BdContextEvet;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Utils;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly EventContext _context;

    //Metodo construtor que aplica a injecao de dependencia 
    public UsuarioRepository(EventContext context)
    {
        _context = context;
    }

    public void Atualizar(Guid id, Usuario usuario)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Busca o usuario pelo email e valida o hash da senha
    /// </summary>
    /// <param name="email">Email do usuario a ser buscado</param>
    /// <param name="senha">Senha para validar o usuario</param>
    /// <returns>Usuario buscado</returns>
    public Usuario BuscarPorEmailESenha(string email, string senha)
    {
        //Primeiro, buscamos o usuario pelo e-mail
        var usuarioBuscado = _context.Usuarios.Include(usuario => usuario.IdTipoUsuarioNavigation).
            FirstOrDefault(usuario => usuario.Email == email);

        //Verificamos se o usuario foi encontrado
        if (usuarioBuscado != null)
        {
            //compramos o hash da senha digitada com oque esta no banco
            bool confere = Criptografia.CompararHash(senha, usuarioBuscado.Senha);

            if (confere)
            {
                return usuarioBuscado;
            }
             
        }

        return null!;
    }

    /// <summary>
    /// Busca um usuário por ID, incluindo os dados do seu  tipo de usuário 
    /// </summary>
    /// <param name="id">id do usuario a ser buscado</param>
    /// <returns>Usuario buscado e seu tipo de usuario</returns>
    public Usuario BuscarPorId(Guid id)
    {
        return _context.Usuarios.Include(usuario => usuario.IdTipoUsuarioNavigation).
            FirstOrDefault(usuario => usuario.IdUsuario == id)!;
    }

    /// <summary>
    /// Cadastra um novo usuário, aplicando a criptografia e o ID Gerado Pelo Banco de Dados
    /// </summary>
    /// <param name="usuario">Usuario a ser cadastrado</param>
    public void Cadastrar(Usuario usuario)
    {
       usuario.Senha = Criptografia.GerarHash(usuario.Senha);

         _context.Usuarios.Add(usuario);
        _context.SaveChanges();
    }

    public List<TipoEvento> Listar()
    {
        throw new NotImplementedException();
    }
}
