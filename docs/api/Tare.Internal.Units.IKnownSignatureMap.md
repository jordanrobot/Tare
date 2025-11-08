#### [Tare](index.md 'index')
### [Tare.Internal.Units](Tare.Internal.Units.md 'Tare.Internal.Units')

## IKnownSignatureMap Interface

Service interface for resolving dimension signatures to preferred unit names.

```csharp
internal interface IKnownSignatureMap
```

Derived  
&#8627; [KnownSignatureMap](Tare.Internal.Units.KnownSignatureMap.md 'Tare.Internal.Units.KnownSignatureMap')

### Remarks
This interface provides signature-to-name resolution for known dimensional compositions,  
enabling display of recognized unit names (e.g., "N" for force) instead of generic  
composite strings (e.g., "kg·m/s²").

| Methods | |
| :--- | :--- |
| [GetKnownSignatures()](Tare.Internal.Units.IKnownSignatureMap.GetKnownSignatures().md 'Tare.Internal.Units.IKnownSignatureMap.GetKnownSignatures()') | Gets all known signatures in the map. |
| [IsKnown(DimensionSignature)](Tare.Internal.Units.IKnownSignatureMap.IsKnown(Tare.Internal.Units.DimensionSignature).md 'Tare.Internal.Units.IKnownSignatureMap.IsKnown(Tare.Internal.Units.DimensionSignature)') | Checks if a signature is known in the map. |
| [TryGetPreferredUnit(DimensionSignature, PreferredUnit)](Tare.Internal.Units.IKnownSignatureMap.TryGetPreferredUnit(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.PreferredUnit).md 'Tare.Internal.Units.IKnownSignatureMap.TryGetPreferredUnit(Tare.Internal.Units.DimensionSignature, Tare.Internal.Units.PreferredUnit)') | Attempts to get the preferred unit for a given dimension signature. |
