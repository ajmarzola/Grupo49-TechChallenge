using System;
using System.ComponentModel.DataAnnotations;

namespace FCG.Application.DTOs
{
    public class UsuarioRegistroModel
    {
        public Guid Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        public string Email { get; set; } = string.Empty;
      
        [Required(ErrorMessage = "A senha é obrigatória.")]
        [RegularExpression(@"^(?=.{8,}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W).*$", ErrorMessage = "Senha fraca. Use no mínimo 8 caracteres, incluindo maiúscula, minúscula, número e símbolo.")]
        public string Senha { get; set; } = string.Empty;

        public string Role { get; set; } = "Aluno"; // ou "Administrador"
    }
}