using TechTalk.SpecFlow;
using FCG.Domain.Entities;
using FluentAssertions;

namespace FCG.BDD.Tests;

[Binding]
public class CompraSteps
{
    private Jogo _jogo;
    private Promocao _promocao;
    private decimal _valorFinal;

    [Given(@"existe um jogo com promoção de (.*) por cento")]
    public void DadoExisteUmJogoComPromocaoDePorCento(decimal desconto)
    {
        _jogo = new Jogo { Preco = 100 };
        _promocao = new Promocao
        {
            DescontoPercentual = desconto,
            Jogo = _jogo
        };
    }

    [When(@"ele realiza a compra")]
    public void QuandoEleRealizaACompra()
    {
        _valorFinal = _jogo.Preco - (_jogo.Preco * _promocao.DescontoPercentual / 100);
    }

    [Then(@"o valor final deve ser (.*) reais")]
    public void EntaoOValorFinalDeveSerReais(decimal esperado)
    {
        _valorFinal.Should().Be(esperado);
    }
}
