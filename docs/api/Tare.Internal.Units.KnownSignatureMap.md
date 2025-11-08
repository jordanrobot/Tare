#### [Tare](index.md 'index')
### [Tare.Internal.Units](Tare.Internal.Units.md 'Tare.Internal.Units')

## KnownSignatureMap Class

Provides a mapping from dimension signatures to preferred unit names.  
Implements the known-signature naming map for common physical quantities.

```csharp
internal sealed class KnownSignatureMap
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; KnownSignatureMap

### Remarks
This sealed class implements a singleton pattern for efficient reuse and provides  
O(1) lookup performance for signature resolution. The map is immutable and thread-safe.  
Initial implementation uses SI-first policy; US Customary preferences deferred to future iteration.

| Fields | |
| :--- | :--- |
| [Instance](Tare.Internal.Units.KnownSignatureMap.Instance.md 'Tare.Internal.Units.KnownSignatureMap.Instance') | Singleton instance for efficient reuse. |

| Methods | |
| :--- | :--- |
| [GetKnownSignatures()](Tare.Internal.Units.KnownSignatureMap.GetKnownSignatures().md 'Tare.Internal.Units.KnownSignatureMap.GetKnownSignatures()') | Gets all known signatures in the map. |
| [IsKnown(DimensionSignature)](Tare.Internal.Units.KnownSignatureMap.IsKnown(Tare.Internal.Units.DimensionSignature).md 'Tare.Internal.Units.KnownSignatureMap.IsKnown(Tare.Internal.Units.DimensionSignature)') | Checks if a signature is known in the map. |
| [TryGetPreferredUnit(DimensionSignature, PreferredUnit)](Tare.Internal.Units.KnownSignatureMap.TryGetPreferredUnit(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.PreferredUnit).md 'Tare.Internal.Units.KnownSignatureMap.TryGetPreferredUnit(Tare.Internal.Units.DimensionSignature, Tare.Internal.Units.PreferredUnit)') | Attempts to get the preferred unit for a given dimension signature. |
