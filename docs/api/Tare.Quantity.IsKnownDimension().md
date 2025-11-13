#### [Tare](index.md 'index')
### [Tare](Tare.md 'Tare').[Quantity](Tare.Quantity.md 'Tare.Quantity')

## Quantity.IsKnownDimension() Method

Determines whether this quantity's dimension is recognized in the known signature map.  
Known dimensions include standard physical quantities like Force, Energy, Pressure, etc.

```csharp
public bool IsKnownDimension();
```

#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')  
True if the dimension is known and has a preferred canonical unit; otherwise, false.

### Remarks
Known dimensions include:  
- Base: Length, Mass, Time, Electric Current, Temperature, Amount of Substance, Luminous Intensity  
- Geometric: Area, Volume  
- Kinematic: Velocity, Acceleration, Jerk  
- Dynamic: Force, Momentum, Energy, Power, Pressure, Torque