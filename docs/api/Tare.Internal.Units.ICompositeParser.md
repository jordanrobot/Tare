#### [Tare](index.md 'index')
### [Tare.Internal.Units](Tare.Internal.Units.md 'Tare.Internal.Units')

## ICompositeParser Interface

Service interface for parsing composite unit strings into dimension signatures and factors.

```csharp
internal interface ICompositeParser
```

Derived  
&#8627; [CompositeParser](Tare.Internal.Units.CompositeParser.md 'Tare.Internal.Units.CompositeParser')

| Methods | |
| :--- | :--- |
| [IsValidComposite(string)](Tare.Internal.Units.ICompositeParser.IsValidComposite(string).md 'Tare.Internal.Units.ICompositeParser.IsValidComposite(string)') | Validates if a string can be parsed as a composite unit. |
| [TryParse(string, DimensionSignature, decimal)](Tare.Internal.Units.ICompositeParser.TryParse(string,Tare.Internal.Units.DimensionSignature,decimal).md 'Tare.Internal.Units.ICompositeParser.TryParse(string, Tare.Internal.Units.DimensionSignature, decimal)') | Parses a composite unit string into its dimension signature and conversion factor. |
