using ContosoBiker.Tarifas;

namespace ContosoBiker.Tarifas.Tests;

public class FormateadorMonedaTests
{
    [Fact]
    public void ATextoMxn_IncluyeSimboloYDosDecimales()
    {
        var texto = FormateadorMoneda.ATextoMxn(1234.5m);

        Assert.Contains("1,234.50", texto);
        Assert.StartsWith("$", texto);
    }

    [Fact]
    public void APorcentaje_FormateaLaProporcion()
    {
        Assert.Contains("15", FormateadorMoneda.APorcentaje(0.15m));
    }

    [Theory]
    [InlineData(-0.1)]
    [InlineData(1.5)]
    public void APorcentaje_RechazaValoresFueraDeRango(decimal proporcion)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => FormateadorMoneda.APorcentaje(proporcion));
    }

    [Fact]
    public void DescribirRenta_UsaSingularParaUnDia()
    {
        var texto = FormateadorMoneda.DescribirRenta("bk-001", 1, 120m);

        Assert.Contains("BK-001", texto);
        Assert.Contains("1 día", texto);
    }

    [Fact]
    public void DescribirRenta_UsaPluralParaVariosDias()
    {
        Assert.Contains("3 días", FormateadorMoneda.DescribirRenta("bk-002", 3, 360m));
    }

    [Fact]
    public void DescribirRenta_RechazaCodigoVacio()
    {
        Assert.Throws<ArgumentException>(() => FormateadorMoneda.DescribirRenta("  ", 1, 120m));
    }
}