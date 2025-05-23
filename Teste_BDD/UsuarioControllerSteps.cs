using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using FCG.API.Controllers;
using FCG.Application.DTOs;
using FCG.Application.Model;
using FCG.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TechTalk.SpecFlow;
using FCG.API;

namespace Teste_BDD
{
    [Binding]
    public partial class UsuarioControllerSteps
    {
        private readonly UsuarioController _controller;
        private readonly Mock<IUsuarioService> _mockUsuarioService;
        private readonly Mock<ILogger<UsuarioController>> _mockLogger;

        private ActionResult<IEnumerable<UsuarioRegistroModel>> _resultList;
        private ActionResult<UsuarioRegistroModel> _result;
        private UsuarioRegistroModel _usuarioEsperado;

        public UsuarioControllerSteps()
        {
            _mockUsuarioService = new Mock<IUsuarioService>();
            _mockLogger = new Mock<ILogger<UsuarioController>>();
            _controller = new UsuarioController(_mockUsuarioService.Object, _mockLogger.Object);
        }

        [Given("que existem usuários cadastrados no sistema")]
        public void DadoQueExistemUsuariosCadastrados()
        {
            var usuarios = new List<UsuarioRegistroModel>
        {
            new UsuarioRegistroModel { Id = Guid.NewGuid(), Nome = "Rafael Nicoletti" }
        };

            _mockUsuarioService.Setup(s => s.ListaUsuariosAsync()).ReturnsAsync(usuarios);
        }

        [When("eu faço uma requisição para listar os usuários")]
        public async Task QuandoEuFacoUmaRequisicaoParaListarOsUsuarios()
        {
            _resultList = await _controller.Listar();
        }

        [Then("a resposta deve ser 200 OK e conter a lista de usuários")]
        public void EntaoARespostaDeveSer200OK()
        {
            _resultList.Result.Should().BeOfType<OkObjectResult>();
            var okResult = _resultList.Result as OkObjectResult;
            okResult.Value.Should().BeAssignableTo<IEnumerable<UsuarioRegistroModel>>();
        }

        [Given(@"que existe um usuário com o ID ""(.*)""")]
        public void DadoQueExisteUmUsuarioComOID(Guid id)
        {
            _usuarioEsperado = new UsuarioRegistroModel { Id = id, Nome = "Pedro de Lara" };
            _mockUsuarioService.Setup(s => s.BuscarUsuarioIdAsync(id)).ReturnsAsync(_usuarioEsperado);
        }

        [When(@"eu faço uma requisição para buscar o usuário com o ID ""(.*)""")]
        public async Task QuandoEuFaçoUmaRequisicaoParaBuscarOUsuarioComOID(Guid id)
        {
            _result = await _controller.BuscarPorId(id);
        }

        [Then(@"a resposta deve ser 200 OK e conter o usuário com o ID ""(.*)""")]
        public void EntaoARespostaDeveSer200OKEConterOUsuarioComOID(Guid id)
        {
            _result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = _result.Result as OkObjectResult;
            var usuario = okResult?.Value as UsuarioRegistroModel;
            usuario.Should().NotBeNull();
            usuario!.Id.Should().Be(id);
        }
    }
}
