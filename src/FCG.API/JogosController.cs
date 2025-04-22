using FCG.Domain.Entities;
using FCG.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using FCG.Application.Model;
using FCG.Domain.Services;
using GreenDonut;

namespace FCG.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class JogosController : ControllerBase
    {
        private readonly JogoService _jogoService;

        public JogosController(JogoService jogoService)
        {
            _jogoService = jogoService;
        }

        // GET /api/jogos
        [HttpGet]
        [Authorize] // qualquer usuário logado (Aluno ou Admin)
        public async Task<ActionResult<IEnumerable<Jogo>>> GetJogos()
        {
            try
            {
                List<Jogo> jogos = (List<Jogo>)await _jogoService.ListarAsync();
                return Ok(jogos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // GET /api/jogos/{id}
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Jogo>> GetJogo(Guid id)
        {
            try
            {
                Jogo jogo = await _jogoService.BuscarPorIdAsync(id);
                
                if(jogo != null)
                {
                    return Ok(jogo);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"Erro: Jogo não encontrado");
                }
               
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        // POST /api/jogos
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<Jogo>> PostJogo(Jogo jogo)
        {
            try
            {
                bool result = await _jogoService.SalvarAsync(jogo);

                if (result)
                {
                    return StatusCode(StatusCodes.Status201Created,"Jogo criado");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao criar jogo");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,"Esse erro aconteceu no metodo create - " + ex.Message);
            }            
        }

        // PUT /api/jogos/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> PutJogo(Guid id, Jogo jogo)
        {
            try
            {
                jogo.Id = id;
                bool result = await _jogoService.AlterarAsync(jogo);

                if (result)
                {
                    return StatusCode(StatusCodes.Status200OK, "Jogo atualizado");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar jogo");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Esse erro aconteceu no metodo update - " + ex.Message);
            }
        }

        // DELETE /api/jogos/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteJogo(Guid id)
        {
            try
            {
                bool result = await _jogoService.DeletarAsync(id);

                if (result)
                {
                    return StatusCode(StatusCodes.Status201Created, "Jogo deletado");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar jogo");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Esse erro aconteceu no metodo delete - " + ex.Message);
            }
        }
    }
}
