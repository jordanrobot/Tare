using System.Collections.Generic;

namespace Tare.Internal.Units;

/// <summary>
/// Service interface for resolving dimension signatures to preferred unit names.
/// </summary>
/// <remarks>
/// This interface provides signature-to-name resolution for known dimensional compositions,
/// enabling display of recognized unit names (e.g., "N" for force) instead of generic
/// composite strings (e.g., "kg·m/s²").
/// </remarks>
internal interface IKnownSignatureMap
{
    /// <summary>
    /// Attempts to get the preferred unit for a given dimension signature.
    /// </summary>
    /// <param name="signature">The dimension signature to resolve.</param>
    /// <param name="preferredUnit">The preferred unit if found; otherwise default.</param>
    /// <returns>True if the signature is known; false otherwise.</returns>
    /// <remarks>
    /// This method uses the TryGet pattern to avoid exceptions for unknown signatures.
    /// Callers can fallback to composite formatting when this returns false.
    /// </remarks>
    bool TryGetPreferredUnit(DimensionSignature signature, out PreferredUnit preferredUnit);
    
    /// <summary>
    /// Checks if a signature is known in the map.
    /// </summary>
    /// <param name="signature">The dimension signature to check.</param>
    /// <returns>True if the signature has a known preferred unit.</returns>
    bool IsKnown(DimensionSignature signature);
    
    /// <summary>
    /// Gets all known signatures in the map.
    /// </summary>
    /// <returns>An enumerable collection of all known dimension signatures.</returns>
    IEnumerable<DimensionSignature> GetKnownSignatures();
}
