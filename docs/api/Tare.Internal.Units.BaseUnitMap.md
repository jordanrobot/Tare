#### [Tare](index.md 'index')
### [Tare.Internal.Units](Tare.Internal.Units.md 'Tare.Internal.Units')

## BaseUnitMap Class

Static mapping of dimension types to their base units.  
Base units are the reference for each dimensional family, typically SI base or derived units.

```csharp
internal static class BaseUnitMap
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; BaseUnitMap
### Fields

<a name='Tare.Internal.Units.BaseUnitMap.BaseUnits'></a>

## BaseUnitMap.BaseUnits Field

Mapping of UnitTypeEnum to canonical base unit identifiers.

```csharp
public static readonly Dictionary<UnitTypeEnum,string> BaseUnits;
```

#### Field Value
[System.Collections.Generic.Dictionary&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.Dictionary-2 'System.Collections.Generic.Dictionary`2')[UnitTypeEnum](Tare.UnitTypeEnum.md 'Tare.UnitTypeEnum')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.Dictionary-2 'System.Collections.Generic.Dictionary`2')[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.Dictionary-2 'System.Collections.Generic.Dictionary`2')