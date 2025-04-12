using Xunit;
using FCG.Domain.Entities;
using FluentAssertions;

namespace FCG.Tests
{
    public class CompraServiceTests
    {
        [Fact]
        public void Deve_calcular_valor_total_com_promocao()
        {
            // Arrange
            var jogo = new Jogo { Preco = 100 };
            var promocao = new Promocao
            {
                DescontoPercentual = 10,
                DataInicio = DateTime.UtcNow.AddDays(-1),
                DataFim = DateTime.UtcNow.AddDays(1),
                Jogo = jogo
            };

            var valorEsperado = 90;

            // Act
            var valorComDesconto = jogo.Preco - (jogo.Preco * promocao.DescontoPercentual / 100);

            // Assert
            valorComDesconto.Should().Be(valorEsperado);
        }
    }
}
