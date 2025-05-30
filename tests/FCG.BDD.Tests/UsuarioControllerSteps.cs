using TechTalk.SpecFlow;
using FCG.API.Controllers;
using FCG.Application.Model.Extensions;
using FCG.Application.Services;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using FCG.Application.DTOs;
using FCG.API;

namespace FCG.BDD.Tests
{
    [Binding]
    public partial class UsuarioControllerSteps
    {
        private readonly UsuarioController _controller;
        private readonly Mock<IUsuarioService> _mockUsuarioService;
        private readonly Mock<ILogger<UsuarioController>> _mockLogger;

        // Altere o tipo de _result conforme o tipo de resposta
        private ActionResult<IEnumerable<UsuarioRegistroModel>> _resultList;  // Para o método Listar
        private ActionResult<UsuarioRegistroModel> _result;  // Para o método BuscarPorId
        private UsuarioRegistroModel _usuario;

        public UsuarioControllerSteps()
        {
            _mockUsuarioService = new Mock<IUsuarioService>();
            _mockLogger = new Mock<ILogger<UsuarioController>>();
            _controller = new UsuarioController(_mockUsuarioService.Object, _mockLogger.Object);
        }

        [Given(@"que existem usuários cadastrados no sistema")]
        public void DadoQueExistemUsuariosCadastradosNoSistema()
        {
            var usuarios = new List<UsuarioRegistroModel>
            {
                new UsuarioRegistroModel { Id = Guid.NewGuid(), Nome = "Rafael Nicoletti" }
            };
            _mockUsuarioService.Setup(service => service.ListaUsuariosAsync()).ReturnsAsync(usuarios.Convert());
        }

        [When(@"eu faço uma requisição para listar os usuários")]
        public async Task QuandoEuFaçoUmaRequisicaoParaListarOsUsuarios()
        {
            _resultList = await _controller.Listar();
        }

        [Then(@"a resposta deve ser 200 OK e conter a lista de usuários")]
        public void EntaoARespostaDeveSer200OKEConterAListaDeUsuarios()
        {
            _resultList.Result.Should().BeOfType<OkObjectResult>();
            var okResult = _resultList.Result as OkObjectResult;
            okResult.Value.Should().BeOfType<List<UsuarioRegistroModel>>();  // Verifica se é uma lista de usuários
        }

        [Given(@"que existe um usuário com o ID ""(.*)""")]
        public void DadoQueExisteUmUsuarioComOID(Guid userId)
        {
            _usuario = new UsuarioRegistroModel { Id = userId, Nome = "Pedro de Lara" };
            _mockUsuarioService.Setup(service => service.BuscarUsuarioIdAsync(userId)).ReturnsAsync(_usuario.Convert());
        }

        [When(@"eu faço uma requisição para buscar o usuário com o ID ""(.*)""")]
        public async Task QuandoEuFaçoUmaRequisicaoParaBuscarOUsuarioComOID(Guid userId)
        {
            _result = await _controller.BuscarPorId(userId);
        }

        [Then(@"a resposta deve ser 200 OK e conter o usuário com o ID ""(.*)""")]
        public void EntaoARespostaDeveSer200OKEConterOUsuarioComOID(Guid userId)
        {
            _result.Result.Should().BeOfType<OkObjectResult>();  // Verifica se é um OkObjectResult
            var okResult = _result.Result as OkObjectResult;
            var usuario = okResult.Value as UsuarioRegistroModel;
            usuario.Id.Should().Be(userId);  // Verifica se o ID do usuário corresponde
        }

    }
}
