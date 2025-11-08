using System;

namespace Tare.Internal.Units;

/// <summary>
/// Represents a normalized, canonical identifier for a unit.
/// Immutable value object ensuring unique representation across aliases.
/// </summary>
internal readonly struct UnitToken : IEquatable<UnitToken>
{
    /// <summary>
    /// Gets the canonical string representation of the unit.
    /// </summary>
    public string Canonical { get; }

    /// <summary>
    /// Constructs a unit token from a canonical string.
    /// </summary>
    /// <param name="canonical">The canonical unit identifier (e.g., "in", "lbf", "m").</param>
    /// <exception cref="ArgumentException">Thrown when canonical is null, empty, or whitespace.</exception>
    public UnitToken(string canonical)
    {
        if (canonical == null)
            throw new ArgumentNullException(nameof(canonical));
        
        if (string.IsNullOrWhiteSpace(canonical))
            throw new ArgumentException("Canonical unit identifier cannot be empty or whitespace.", nameof(canonical));
        
        Canonical = canonical;
    }

    /// <summary>
    /// Determines whether the specified <see cref="UnitToken"/> is equal to the current <see cref="UnitToken"/>.
    /// </summary>
    /// <param name="other">The <see cref="UnitToken"/> to compare with the current <see cref="UnitToken"/>.</param>
    /// <returns>true if the specified <see cref="UnitToken"/> is equal to the current <see cref="UnitToken"/>; otherwise, false.</returns>
    public bool Equals(UnitToken other)
    {
        return Canonical == other.Canonical;
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current <see cref="UnitToken"/>.
    /// </summary>
    /// <param name="obj">The object to compare with the current <see cref="UnitToken"/>.</param>
    /// <returns>true if the specified object is equal to the current <see cref="UnitToken"/>; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        return obj is UnitToken other && Equals(other);
    }

    /// <summary>
    /// Returns the hash code for this <see cref="UnitToken"/>.
    /// </summary>
    /// <returns>A hash code for the current <see cref="UnitToken"/>.</returns>
    public override int GetHashCode()
    {
        return Canonical?.GetHashCode() ?? 0;
    }

    /// <summary>
    /// Determines whether two <see cref="UnitToken"/> instances are equal.
    /// </summary>
    public static bool operator ==(UnitToken left, UnitToken right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="UnitToken"/> instances are not equal.
    /// </summary>
    public static bool operator !=(UnitToken left, UnitToken right)
    {
        return !left.Equals(right);
    }

    /// <summary>
    /// Returns the canonical string representation of this unit token.
    /// </summary>
    /// <returns>The canonical unit identifier string.</returns>
    public override string ToString()
    {
        return Canonical ?? string.Empty;
    }
}
