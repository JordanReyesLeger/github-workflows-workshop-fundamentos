namespace ContosoBiker.Tarifas;

/// <summary>
/// Calcula el costo de una renta de bicicleta en Contoso Biker.
/// </summary>
public static class CalculadoraTarifas
{
    /// <summary>Días a partir de los cuales aplica el descuento semanal.</summary>
    public const int DiasParaDescuentoSemanal = 7;

    /// <summary>Porcentaje de descuento cuando la renta dura una semana o más.</summary>
    public const decimal PorcentajeDescuentoSemanal = 0.15m;

    /// <summary>
    /// Calcula el subtotal de una renta sin descuentos ni recargos.
    /// </summary>
    /// <param name="tarifaDiaria">Tarifa por día de la bicicleta.</param>
    /// <param name="dias">Días completos de la renta.</param>
    public static decimal CalcularSubtotal(decimal tarifaDiaria, int dias)
    {
        if (tarifaDiaria < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(tarifaDiaria), "La tarifa diaria no puede ser negativa.");
        }

        if (dias <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(dias), "La renta debe durar al menos un día.");
        }

        return Redondear(tarifaDiaria * dias);
    }

    /// <summary>
    /// Aplica el descuento semanal cuando la renta dura 7 días o más.
    /// </summary>
    public static decimal AplicarDescuentoSemanal(decimal subtotal, int dias)
    {
        if (subtotal < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(subtotal), "El subtotal no puede ser negativo.");
        }

        if (dias < DiasParaDescuentoSemanal)
        {
            return Redondear(subtotal);
        }

        return Redondear(subtotal * (1 - PorcentajeDescuentoSemanal));
    }

    /// <summary>
    /// Calcula el recargo por devolver la bicicleta tarde.
    /// Cada hora iniciada de retraso cuesta el 10% de la tarifa diaria.
    /// </summary>
    public static decimal CalcularRecargoPorRetraso(decimal tarifaDiaria, int horasDeRetraso)
    {
        if (tarifaDiaria < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(tarifaDiaria), "La tarifa diaria no puede ser negativa.");
        }

        if (horasDeRetraso <= 0)
        {
            return 0m;
        }

        return Redondear(tarifaDiaria * 0.10m * horasDeRetraso);
    }

    /// <summary>
    /// Calcula el total de una renta: subtotal, descuento semanal y recargo por retraso.
    /// </summary>
    public static decimal CalcularTotal(decimal tarifaDiaria, int dias, int horasDeRetraso = 0)
    {
        var subtotal = CalcularSubtotal(tarifaDiaria, dias);
        var conDescuento = AplicarDescuentoSemanal(subtotal, dias);
        var recargo = CalcularRecargoPorRetraso(tarifaDiaria, horasDeRetraso);

        return Redondear(conDescuento + recargo);
    }

    private static decimal Redondear(decimal valor) =>
        Math.Round(valor, 2, MidpointRounding.AwayFromZero);
}