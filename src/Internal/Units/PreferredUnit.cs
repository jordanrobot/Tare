using System;
using System.Collections.Generic;
using System.Linq;

namespace Tare.Internal.Units;

/// <summary>
/// Represents a preferred unit name for a known dimension signature.
/// Includes the canonical name and optional alternative names for different contexts.
/// </summary>
/// <remarks>
/// This immutable value type encapsulates unit naming information for dimensional signatures.
/// For example, the energy/torque signature (L²M¹T⁻²) has canonical name "J" (joule)
/// with alternative "Nm" (newton-meter) for torque contexts.
/// </remarks>
internal readonly struct PreferredUnit : IEquatable<PreferredUnit>
{
    /// <summary>
    /// Gets the canonical unit name (e.g., "N", "Nm", "Pa", "m²").
    /// </summary>
    public string CanonicalName { get; }
    
    /// <summary>
    /// Gets alternative names for the same signature (e.g., "J" as alternative to "Nm" for energy).
    /// </summary>
    public IReadOnlyList<string> AlternativeNames { get; }
    
    /// <summary>
    /// Gets a description of the physical quantity (e.g., "Force", "Torque", "Pressure").
    /// </summary>
    public string Description { get; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="PreferredUnit"/> struct.
    /// </summary>
    /// <param name="canonicalName">The canonical unit name.</param>
    /// <param name="description">Description of the physical quantity.</param>
    /// <param name="alternativeNames">Optional alternative names for the same signature.</param>
    /// <exception cref="ArgumentNullException">Thrown when canonicalName or description is null.</exception>
    public PreferredUnit(string canonicalName, string description, params string[] alternativeNames)
    {
        if (canonicalName == null)
            throw new ArgumentNullException(nameof(canonicalName));
        if (description == null)
            throw new ArgumentNullException(nameof(description));
        
        CanonicalName = canonicalName;
        Description = description;
        AlternativeNames = alternativeNames?.Length > 0 
            ? Array.AsReadOnly(alternativeNames) 
            : Array.Empty<string>();
    }
    
    /// <summary>
    /// Determines whether the specified <see cref="PreferredUnit"/> is equal to the current unit.
    /// </summary>
    /// <param name="other">The unit to compare with the current unit.</param>
    /// <returns>True if the specified unit is equal to the current unit; otherwise, false.</returns>
    public bool Equals(PreferredUnit other)
    {
        if (CanonicalName != other.CanonicalName || Description != other.Description)
            return false;

        // Handle null alternative names (default struct)
        if (AlternativeNames == null && other.AlternativeNames == null)
            return true;
        if (AlternativeNames == null || other.AlternativeNames == null)
            return false;

        return AlternativeNames.SequenceEqual(other.AlternativeNames);
    }
    
    /// <summary>
    /// Determines whether the specified object is equal to the current unit.
    /// </summary>
    /// <param name="obj">The object to compare with the current unit.</param>
    /// <returns>True if the specified object is equal to the current unit; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        return obj is PreferredUnit other && Equals(other);
    }
    
    /// <summary>
    /// Returns the hash code for this unit.
    /// </summary>
    /// <returns>A hash code for the current unit.</returns>
    public override int GetHashCode()
    {
#if NETSTANDARD2_0
        unchecked
        {
            int hash = 17;
            hash = hash * 31 + (CanonicalName?.GetHashCode() ?? 0);
            hash = hash * 31 + (Description?.GetHashCode() ?? 0);
            // Alternative names contribute to hash (handle null for default struct)
            if (AlternativeNames != null)
            {
                foreach (var alt in AlternativeNames)
                {
                    hash = hash * 31 + (alt?.GetHashCode() ?? 0);
                }
            }
            return hash;
        }
#else
        var hashCode = new HashCode();
        hashCode.Add(CanonicalName);
        hashCode.Add(Description);
        if (AlternativeNames != null)
        {
            foreach (var alt in AlternativeNames)
            {
                hashCode.Add(alt);
            }
        }
        return hashCode.ToHashCode();
#endif
    }
    
    /// <summary>
    /// Determines whether two <see cref="PreferredUnit"/> instances are equal.
    /// </summary>
    public static bool operator ==(PreferredUnit left, PreferredUnit right)
    {
        return left.Equals(right);
    }
    
    /// <summary>
    /// Determines whether two <see cref="PreferredUnit"/> instances are not equal.
    /// </summary>
    public static bool operator !=(PreferredUnit left, PreferredUnit right)
    {
        return !left.Equals(right);
    }
    
    /// <summary>
    /// Returns a string representation of this preferred unit.
    /// </summary>
    /// <returns>A string in the format "CanonicalName (Description)".</returns>
    public override string ToString()
    {
        return $"{CanonicalName} ({Description})";
    }
}
