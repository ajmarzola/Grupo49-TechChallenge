using FCG.Application.DTOs;
using FCG.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Application.Model.Extensions
{
    public static class UsuarioRegistroModelExtensions
    {
        /// <summary>
        /// Converte um domínio Usuario para o DTO UsuarioRegistroModel
        /// </summary>
        public static UsuarioRegistroModel Convert(this Usuario src) => src == null
                ? null
                : new UsuarioRegistroModel
                {
                    Id = src.Id,
                    Nome = src.Nome,
                    Email = src.Email,
                    Senha = src.Senha,
                    Role = src.Role
                };

        /// <summary>
        /// Converte um DTO UsuarioRegistroModel para o domínio Usuario
        /// </summary>
        public static Usuario Convert(this UsuarioRegistroModel src) => src == null
                ? null
                : new Usuario
                {
                    Id = src.Id,
                    Nome = src.Nome,
                    Email = src.Email,
                    Senha = src.Senha,
                    Role = src.Role
                };

        /// <summary>
        /// Converte um domínio Usuario para o DTO UsuarioRegistroModel
        /// </summary>
        public static IEnumerable<UsuarioRegistroModel> Convert(this IEnumerable<Usuario> src)
        {
            if (src == null)
                return null;

            var retorno = new List<UsuarioRegistroModel>();

            foreach (Usuario registro in src)
            {
                retorno.Add(registro.Convert());
            }

            return retorno;
        }

        /// <summary>
        /// Converte um DTO UsuarioRegistroModel para o domínio Usuario
        /// </summary>
        public static IEnumerable<Usuario> Convert(this IEnumerable<UsuarioRegistroModel> src)
        {
            if (src == null)
                return null;

            var retorno = new List<Usuario>();

            foreach (UsuarioRegistroModel registro in src)
            {
                retorno.Add(registro.Convert());
            }

            return retorno;
        }
    }
}