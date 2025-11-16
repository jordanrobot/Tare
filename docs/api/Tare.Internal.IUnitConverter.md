#### [Tare](index.md 'index')
### [Tare\.Internal](Tare.Internal.md 'Tare\.Internal')

## IUnitConverter Interface

Unified interface for unit conversions\.
Implementations handle conversion to/from base unit values\.

```csharp
internal interface IUnitConverter
```

Derived  
&#8627; [DelegateConverter](Tare.Internal.DelegateConverter.md 'Tare\.Internal\.DelegateConverter')  
&#8627; [LinearConverter](Tare.Internal.LinearConverter.md 'Tare\.Internal\.LinearConverter')
### Properties

<a name='Tare.Internal.IUnitConverter.IsExact'></a>

## IUnitConverter\.IsExact Property

Gets whether this converter uses exact rational arithmetic\.

```csharp
bool IsExact { get; }
```

#### Property Value
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')
### Methods

<a name='Tare.Internal.IUnitConverter.FromBase(decimal)'></a>

## IUnitConverter\.FromBase\(decimal\) Method

Converts a value from the base unit to this unit\.

```csharp
decimal FromBase(decimal baseValue);
```
#### Parameters

<a name='Tare.Internal.IUnitConverter.FromBase(decimal).baseValue'></a>

`baseValue` [System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')

Value in base unit\.

#### Returns
[System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')  
Value in this unit\.

<a name='Tare.Internal.IUnitConverter.ToBase(decimal)'></a>

## IUnitConverter\.ToBase\(decimal\) Method

Converts a value from this unit to the base unit\.

```csharp
decimal ToBase(decimal value);
```
#### Parameters

<a name='Tare.Internal.IUnitConverter.ToBase(decimal).value'></a>

`value` [System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')

Value in this unit\.

#### Returns
[System\.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal 'System\.Decimal')  
Value in base unit\.