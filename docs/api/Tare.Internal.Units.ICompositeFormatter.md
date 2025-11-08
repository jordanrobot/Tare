#### [Tare](index.md 'index')
### [Tare.Internal.Units](Tare.Internal.Units.md 'Tare.Internal.Units')

## ICompositeFormatter Interface

Service interface for formatting dimension signatures as composite unit strings.

```csharp
internal interface ICompositeFormatter
```

Derived  
&#8627; [CompositeFormatter](Tare.Internal.Units.CompositeFormatter.md 'Tare.Internal.Units.CompositeFormatter')

| Methods | |
| :--- | :--- |
| [Format(DimensionSignature, string[])](Tare.Internal.Units.ICompositeFormatter.Format(Tare.Internal.Units.DimensionSignature,string[]).md 'Tare.Internal.Units.ICompositeFormatter.Format(Tare.Internal.Units.DimensionSignature, string[])') | Formats a dimension signature with custom base unit tokens (for future use with non-SI bases). |
| [Format(DimensionSignature)](Tare.Internal.Units.ICompositeFormatter.Format(Tare.Internal.Units.DimensionSignature).md 'Tare.Internal.Units.ICompositeFormatter.Format(Tare.Internal.Units.DimensionSignature)') | Formats a dimension signature as a composite unit string using canonical base units. |
