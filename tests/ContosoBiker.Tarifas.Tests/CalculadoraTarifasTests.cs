using ContosoBiker.Tarifas;

namespace ContosoBiker.Tarifas.Tests;

public class CalculadoraTarifasTests
{
    [Theory]
    [InlineData(120, 1, 120)]
    [InlineData(120, 3, 360)]
    [InlineData(99.90, 2, 199.80)]
    public void CalcularSubtotal_MultiplicaTarifaPorDias(decimal tarifa, int dias, decimal esperado)
    {
        Assert.Equal(esperado, CalculadoraTarifas.CalcularSubtotal(tarifa, dias));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void CalcularSubtotal_RechazaDiasNoPositivos(int dias)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => CalculadoraTarifas.CalcularSubtotal(100m, dias));
    }

    [Fact]
    public void CalcularSubtotal_RechazaTarifaNegativa()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => CalculadoraTarifas.CalcularSubtotal(-1m, 3));
    }

    [Fact]
    public void AplicarDescuentoSemanal_NoDescuentaAntesDeSieteDias()
    {
        Assert.Equal(720m, CalculadoraTarifas.AplicarDescuentoSemanal(720m, 6));
    }

    [Fact]
    public void AplicarDescuentoSemanal_DescuentaQuincePorCientoDesdeSieteDias()
    {
        Assert.Equal(714m, CalculadoraTarifas.AplicarDescuentoSemanal(840m, 7));
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(-3, 0)]
    [InlineData(2, 24)]
    public void CalcularRecargoPorRetraso_CobraDiezPorCientoPorHora(int horas, decimal esperado)
    {
        Assert.Equal(esperado, CalculadoraTarifas.CalcularRecargoPorRetraso(120m, horas));
    }

    [Fact]
    public void CalcularTotal_SumaDescuentoYRecargo()
    {
        // 120 x 7 = 840, con 15 % de descuento = 714, mas 2 horas de retraso = 24
        Assert.Equal(738m, CalculadoraTarifas.CalcularTotal(120m, 7, 2));
    }

    [Fact]
    public void CalcularTotal_SinRetrasoNiDescuento()
    {
        Assert.Equal(360m, CalculadoraTarifas.CalcularTotal(120m, 3));
    }
}