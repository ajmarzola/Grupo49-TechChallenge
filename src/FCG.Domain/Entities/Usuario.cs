using System;
using System.Collections.Generic;

namespace FCG.Domain.Entities;

public class Usuario
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public string Role { get; set; } = "Aluno";
    public ICollection<Compra> Compras { get; set; } = new List<Compra>();
}