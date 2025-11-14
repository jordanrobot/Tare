#### [Tare](index.md 'index')
### [Tare\.Internal\.Units](Tare.Internal.Units.md 'Tare\.Internal\.Units')

## IKnownSignatureMap Interface

Service interface for resolving dimension signatures to preferred unit names\.

```csharp
internal interface IKnownSignatureMap
```

Derived  
&#8627; [KnownSignatureMap](Tare.Internal.Units.KnownSignatureMap.md 'Tare\.Internal\.Units\.KnownSignatureMap')

### Remarks
This interface provides signature\-to\-name resolution for known dimensional compositions,
enabling display of recognized unit names \(e\.g\., "N" for force\) instead of generic
composite strings \(e\.g\., "kg·m/s²"\)\.
### Methods

<a name='Tare.Internal.Units.IKnownSignatureMap.GetKnownSignatures()'></a>

## IKnownSignatureMap\.GetKnownSignatures\(\) Method

Gets all known signatures in the map\.

```csharp
System.Collections.Generic.IEnumerable<Tare.Internal.Units.DimensionSignature> GetKnownSignatures();
```

#### Returns
[System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare\.Internal\.Units\.DimensionSignature')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')  
An enumerable collection of all known dimension signatures\.

<a name='Tare.Internal.Units.IKnownSignatureMap.IsKnown(Tare.Internal.Units.DimensionSignature)'></a>

## IKnownSignatureMap\.IsKnown\(DimensionSignature\) Method

Checks if a signature is known in the map\.

```csharp
bool IsKnown(Tare.Internal.Units.DimensionSignature signature);
```
#### Parameters

<a name='Tare.Internal.Units.IKnownSignatureMap.IsKnown(Tare.Internal.Units.DimensionSignature).signature'></a>

`signature` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare\.Internal\.Units\.DimensionSignature')

The dimension signature to check\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
True if the signature has a known preferred unit\.

<a name='Tare.Internal.Units.IKnownSignatureMap.TryGetPreferredUnit(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.PreferredUnit)'></a>

## IKnownSignatureMap\.TryGetPreferredUnit\(DimensionSignature, PreferredUnit\) Method

Attempts to get the preferred unit for a given dimension signature\.

```csharp
bool TryGetPreferredUnit(Tare.Internal.Units.DimensionSignature signature, out Tare.Internal.Units.PreferredUnit preferredUnit);
```
#### Parameters

<a name='Tare.Internal.Units.IKnownSignatureMap.TryGetPreferredUnit(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.PreferredUnit).signature'></a>

`signature` [DimensionSignature](Tare.Internal.Units.DimensionSignature.md 'Tare\.Internal\.Units\.DimensionSignature')

The dimension signature to resolve\.

<a name='Tare.Internal.Units.IKnownSignatureMap.TryGetPreferredUnit(Tare.Internal.Units.DimensionSignature,Tare.Internal.Units.PreferredUnit).preferredUnit'></a>

`preferredUnit` [PreferredUnit](Tare.Internal.Units.PreferredUnit.md 'Tare\.Internal\.Units\.PreferredUnit')

The preferred unit if found; otherwise default\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
True if the signature is known; false otherwise\.

### Remarks
This method uses the TryGet pattern to avoid exceptions for unknown signatures\.
Callers can fallback to composite formatting when this returns false\.