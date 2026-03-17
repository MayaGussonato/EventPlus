using System.ComponentModel.DataAnnotations;
namespace EventPlus.WebAPI.DTO;

    public class LoginDTO
    {
        [Required(ErrorMessage = "O EMAIL DO USUARIO E OBRIGATORIO!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A SENHA DO USUARIO E OBRIGATORIO!")]
        public string Senha { get; set; }
    }

