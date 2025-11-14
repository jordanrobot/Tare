#### [Tare](index.md 'index')
### [Tare\.Internal\.Units](Tare.Internal.Units.md 'Tare\.Internal\.Units')

## BaseUnitMap Class

Static mapping of dimension types to their base units\.
Base units are the reference for each dimensional family, typically SI base or derived units\.

```csharp
internal static class BaseUnitMap
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') &#129106; BaseUnitMap
### Fields

<a name='Tare.Internal.Units.BaseUnitMap.BaseUnits'></a>

## BaseUnitMap\.BaseUnits Field

Mapping of UnitTypeEnum to canonical base unit identifiers\.

```csharp
public static readonly Dictionary<UnitTypeEnum,string> BaseUnits;
```

#### Field Value
[System\.Collections\.Generic\.Dictionary&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[UnitTypeEnum](Tare.UnitTypeEnum.md 'Tare\.UnitTypeEnum')[,](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')