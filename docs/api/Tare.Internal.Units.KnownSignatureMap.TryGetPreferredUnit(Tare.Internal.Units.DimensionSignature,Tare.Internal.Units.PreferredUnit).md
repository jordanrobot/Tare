#### [Tare](index.md 'index')
### [Tare.Internal.Units](Tare.Internal.Units.md 'Tare.Internal.Units').[KnownSignatureMap](Tare.Internal.Units.KnownSignatureMap.md 'Tare.Internal.Units.KnownSignatureMap')

## KnownSignatureMap.TryGetPreferredUnit(DimensionSignature, PreferredUnit) Method

Attempts to get the preferred unit for a given dimension signature.

```csharp
public bool TryGetPreferredUnit(Tare.Internal.Units.DimensionSignature signature, out Tare.Internal.Units.PreferredUnit preferredUnit);
```
#### Parameters

<a name='Tare.Internal.Units.KnownSignatureMap.TryGetPreferredUnit(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.PreferredUnit).signature'></a>

`signature` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare.Internal.Units.DimensionSignature')

The dimension signature to resolve.

<a name='Tare.Internal.Units.KnownSignatureMap.TryGetPreferredUnit(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.PreferredUnit).preferredUnit'></a>

`preferredUnit` [PreferredUnit](Tare.Internal.Units.PreferredUnit.md 'Tare.Internal.Units.PreferredUnit')

The preferred unit if found; otherwise default.

#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')  
True if the signature is known; false otherwise.

### Remarks
This method uses the TryGet pattern to avoid exceptions for unknown signatures.  
Callers can fallback to composite formatting when this returns false.