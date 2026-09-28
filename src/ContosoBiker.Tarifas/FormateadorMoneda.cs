using System.Globalization;

namespace ContosoBiker.Tarifas;

/// <summary>
/// Da formato legible a los montos que muestra Contoso Biker.
/// </summary>
public static class FormateadorMoneda
{
    private static readonly CultureInfo CulturaMexico = new("es-MX");

    /// <summary>
    /// Formatea un monto como moneda mexicana, por ejemplo <c>$1,234.50</c>.
    /// </summary>
    public static string ATextoMxn(decimal monto) =>
        monto.ToString("C2", CulturaMexico);

    /// <summary>
    /// Formatea un porcentaje entre 0 y 1 como texto, por ejemplo <c>15 %</c>.
    /// </summary>
    public static string APorcentaje(decimal proporcion)
    {
        if (proporcion is < 0m or > 1m)
        {
            throw new ArgumentOutOfRangeException(nameof(proporcion), "La proporción debe estar entre 0 y 1.");
        }

        return proporcion.ToString("P0", CulturaMexico);
    }

    /// <summary>
    /// Describe una renta en una línea lista para el ticket del cliente.
    /// </summary>
    public static string DescribirRenta(string codigoBicicleta, int dias, decimal total)
    {
        if (string.IsNullOrWhiteSpace(codigoBicicleta))
        {
            throw new ArgumentException("El código de la bicicleta es obligatorio.", nameof(codigoBicicleta));
        }

        var plural = dias == 1 ? "día" : "días";
        return $"{codigoBicicleta.ToUpperInvariant()} · {dias} {plural} · {ATextoMxn(total)}";
    }
}