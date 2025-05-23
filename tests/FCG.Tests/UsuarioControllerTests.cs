using FCG.API;
using FCG.API.Controllers;
using FCG.Application.DTOs;
using FCG.Application.Model;
using FCG.Application.Services;
using FCG.Application.Model.Extensions;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Language.Flow;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FCG.Tests
{
    public partial class UsuarioControllerTests : IDisposable  // Implementando IDisposable corretamente
    {
        private readonly Mock<IUsuarioService> _mockUsuarioService;
        private readonly Mock<ILogger<UsuarioController>> _mockLogger;
        private readonly UsuarioController _controller;

        public UsuarioControllerTests()
        {
            _mockUsuarioService = new Mock<IUsuarioService>();
            _mockLogger = new Mock<ILogger<UsuarioController>>();
            _controller = new UsuarioController(_mockUsuarioService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Listar_ReturnsOkResult_WhenUsersExist()
        {
            // Arrange
            var usuarios = new List<UsuarioRegistroModel>
            {
                new UsuarioRegistroModel { Id = Guid.NewGuid(), Nome = "Rafael Nicoletti" }
            };

            _mockUsuarioService.Setup(service => service.ListaUsuariosAsync()).ReturnsAsync(usuarios.Convert());

            // Act
            var result = await _controller.Listar();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(usuarios);
        }

        [Fact]
        public async Task BuscarPorId_ReturnsOkResult_WhenUserFound()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var usuario = new UsuarioRegistroModel { Id = userId, Nome = "Pedro de Lara" };
            _mockUsuarioService.Setup(service => service.BuscarUsuarioIdAsync(userId)).ReturnsAsync(usuario.Convert());

            // Act
            var result = await _controller.BuscarPorId(userId);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().Be(usuario);
        }

        [Fact]
        public async Task Salvar_ReturnsOkResult_WhenUserSavedSuccessfully()
        {
            // Arrange
            var usuario = new UsuarioRegistroModel { Nome = "Rafael Nicoletti" };

            // Retornando Task<bool> correto
            _mockUsuarioService.Setup(service => service.SalvarUsuarioAsync(usuario)).ReturnsAsync(true);

            // Act
            var result = await _controller.Salvar(usuario);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task Deletar_ReturnsOkResult_WhenUserDeletedSuccessfully()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _mockUsuarioService.Setup(service => service.BuscarUsuarioIdAsync(userId)).ReturnsAsync(new UsuarioRegistroModel { Id = userId }.Convert());
            _mockUsuarioService.Setup(service => service.DeletarUsuarioAsync(userId)).ReturnsAsync(true);

            // Act
            var result = await _controller.Deletar(userId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        // Método Dispose da interface IDisposable
        public void Dispose()
        {
            // Aqui você pode liberar recursos, se necessário
            // No caso de testes unitários, geralmente não há necessidade de liberar recursos
        }
    }
}
