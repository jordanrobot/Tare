using System;
using System.Text;

namespace Tare.Internal.Units;

/// <summary>
/// Formats dimension signatures as composite unit strings using canonical ordering and notation.
/// </summary>
/// <remarks>
/// This sealed class implements deterministic, idempotent formatting for composite units.
/// Base dimensions are ordered canonically (L, M, T, I, Θ, N, J), with positive exponents
/// in the numerator and negative exponents in the denominator.
/// </remarks>
internal sealed class CompositeFormatter : ICompositeFormatter
{
    /// <summary>
    /// Singleton instance for efficient reuse.
    /// </summary>
    public static readonly CompositeFormatter Instance = new CompositeFormatter();

    /// <summary>
    /// Default SI base unit tokens in canonical order: L, M, T, I, Θ, N, J.
    /// </summary>
    private static readonly string[] DefaultBaseUnits = { "m", "kg", "s", "A", "K", "mol", "cd" };

    private CompositeFormatter()
    {
    }

    /// <inheritdoc/>
    public string Format(DimensionSignature signature)
    {
        return Format(signature, DefaultBaseUnits);
    }

    /// <inheritdoc/>
    public string Format(DimensionSignature signature, string[] baseUnitTokens)
    {
        if (baseUnitTokens == null || baseUnitTokens.Length != 7)
        {
            throw new ArgumentException("Base unit tokens must be an array of exactly 7 elements (L, M, T, I, Θ, N, J).", nameof(baseUnitTokens));
        }

        // Get exponents in canonical order
        sbyte[] exponents = {
            signature.Length,
            signature.Mass,
            signature.Time,
            signature.ElectricCurrent,
            signature.Temperature,
            signature.AmountOfSubstance,
            signature.LuminousIntensity
        };

        // Check for dimensionless
        if (IsDimensionless(exponents))
        {
            return string.Empty;
        }

        var numerator = new StringBuilder();
        var denominator = new StringBuilder();

        // Build numerator and denominator
        for (int i = 0; i < 7; i++)
        {
            sbyte exp = exponents[i];

            if (exp > 0)
            {
                // Positive exponent: add to numerator
                if (numerator.Length > 0)
                {
                    numerator.Append('·');
                }
                numerator.Append(baseUnitTokens[i]);
                if (exp > 1)
                {
                    numerator.Append('^').Append(exp);
                }
            }
            else if (exp < 0)
            {
                // Negative exponent: add to denominator
                if (denominator.Length > 0)
                {
                    denominator.Append('·');
                }
                denominator.Append(baseUnitTokens[i]);
                if (exp < -1)
                {
                    denominator.Append('^').Append(-exp);
                }
            }
        }

        // Combine numerator and denominator
        if (numerator.Length == 0)
        {
            // Only denominator (e.g., 1/s²)
            return "1/" + denominator.ToString();
        }
        else if (denominator.Length == 0)
        {
            // Only numerator
            return numerator.ToString();
        }
        else
        {
            // Both numerator and denominator
            return numerator.ToString() + "/" + denominator.ToString();
        }
    }

    private static bool IsDimensionless(sbyte[] exponents)
    {
        foreach (var exp in exponents)
        {
            if (exp != 0)
            {
                return false;
            }
        }
        return true;
    }
}
