#### [Tare](index.md 'index')
### [Tare\.Internal\.Units](Tare.Internal.Units.md 'Tare\.Internal\.Units')

## KnownSignatureMap Class

Provides a mapping from dimension signatures to preferred unit names\.
Implements the known\-signature naming map for common physical quantities\.

```csharp
internal sealed class KnownSignatureMap
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') &#129106; KnownSignatureMap

### Remarks
This sealed class implements a singleton pattern for efficient reuse and provides
O\(1\) lookup performance for signature resolution\. The map is immutable and thread\-safe\.
Initial implementation uses SI\-first policy; US Customary preferences deferred to future iteration\.
### Fields

<a name='Tare.Internal.Units.KnownSignatureMap.Instance'></a>

## KnownSignatureMap\.Instance Field

Singleton instance for efficient reuse\.

```csharp
public static readonly KnownSignatureMap Instance;
```

#### Field Value
[KnownSignatureMap](Tare.Internal.Units.KnownSignatureMap.md 'Tare\.Internal\.Units\.KnownSignatureMap')
### Methods

<a name='Tare.Internal.Units.KnownSignatureMap.GetKnownSignatures()'></a>

## KnownSignatureMap\.GetKnownSignatures\(\) Method

Gets all known signatures in the map\.

```csharp
public System.Collections.Generic.IEnumerable<Tare.Internal.Units.DimensionSignature> GetKnownSignatures();
```

#### Returns
[System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare\.Internal\.Units\.DimensionSignature')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')  
An enumerable collection of all known dimension signatures\.

<a name='Tare.Internal.Units.KnownSignatureMap.IsKnown(Tare.Internal.Units.DimensionSignature)'></a>

## KnownSignatureMap\.IsKnown\(DimensionSignature\) Method

Checks if a signature is known in the map\.

```csharp
public bool IsKnown(Tare.Internal.Units.DimensionSignature signature);
```
#### Parameters

<a name='Tare.Internal.Units.KnownSignatureMap.IsKnown(Tare.Internal.Units.DimensionSignature).signature'></a>

`signature` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare\.Internal\.Units\.DimensionSignature')

The dimension signature to check\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
True if the signature has a known preferred unit\.

<a name='Tare.Internal.Units.KnownSignatureMap.TryGetPreferredUnit(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.PreferredUnit)'></a>

## KnownSignatureMap\.TryGetPreferredUnit\(DimensionSignature, PreferredUnit\) Method

Attempts to get the preferred unit for a given dimension signature\.

```csharp
public bool TryGetPreferredUnit(Tare.Internal.Units.DimensionSignature signature, out Tare.Internal.Units.PreferredUnit preferredUnit);
```
#### Parameters

<a name='Tare.Internal.Units.KnownSignatureMap.TryGetPreferredUnit(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.PreferredUnit).signature'></a>

`signature` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare\.Internal\.Units\.DimensionSignature')

The dimension signature to resolve\.

<a name='Tare.Internal.Units.KnownSignatureMap.TryGetPreferredUnit(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.PreferredUnit).preferredUnit'></a>

`preferredUnit` [PreferredUnit](Tare.Internal.Units.PreferredUnit.md 'Tare\.Internal\.Units\.PreferredUnit')

The preferred unit if found; otherwise default\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
True if the signature is known; false otherwise\.

### Remarks
This method uses the TryGet pattern to avoid exceptions for unknown signatures\.
Callers can fallback to composite formatting when this returns false\.