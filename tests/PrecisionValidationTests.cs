namespace TareTests;

[TestFixture]
public class PrecisionValidationTests
{
    [Test]
    public void DimensionalOperation_60InLbfRoundTrip_ExactValue()
    {
        // This test validates the fix for the 0.015% precision error
        // Previously: 60 in·lbf → Nm → in·lbf returned 59.9999092...
        // Expected: Exact 60.0 with Rational arithmetic
        
        // Arrange
        var force = Quantity.Parse(10, "lbf");
        var distance = Quantity.Parse(6, "in");
        
        // Act - multiply to get torque
        var torque = force * distance;
        
        // Convert to Nm and back to in*lbf
        var torqueNm = Quantity.Parse(torque.Convert("Nm"), "Nm");
        var backToInLbf = torqueNm.Convert("in*lbf");
        
        // Assert - should be exactly 60.0 (within floating point precision)
        Assert.That(backToInLbf, Is.EqualTo(60.0m).Within(0.0000001m), 
            "Dimensional operation round-trip should maintain precision");
    }
    
    [Test]
    public void DimensionalOperation_48InLbfRoundTrip_ExactValue()
    {
        // This test validates the fix for another precision case
        // Previously: 48 in·lbf → Nm → in·lbf returned 47.9999273...
        // Expected: Exact 48.0 with Rational arithmetic
        
        // Arrange
        var a = Quantity.Parse(12, "in");
        var b = Quantity.Parse(8, "in");
        var c = Quantity.Parse(4, "in");
        var d = Quantity.Parse(2, "lbf");
        
        // Act - complex operation: ((12in × 8in) ÷ 4in) × 2lbf
        var result = ((a * b) / c) * d;
        
        // Convert to Nm and back
        var resultNm = Quantity.Parse(result.Convert("Nm"), "Nm");
        var backToInLbf = resultNm.Convert("in*lbf");
        
        // Assert - should be exactly 48.0
        Assert.That(backToInLbf, Is.EqualTo(48.0m).Within(0.0000001m),
            "Complex dimensional operation should maintain precision");
    }
    
    [Test]
    public void DimensionalOperation_ForceTimesDistance_PreciseConversion()
    {
        // Test general force × distance precision
        
        // Arrange
        var force = Quantity.Parse(100, "N");
        var distance = Quantity.Parse(2, "m");
        
        // Act
        var energy = force * distance;
        
        // Assert - should be exactly 200 J
        Assert.That(energy.Value, Is.EqualTo(200.0m));
        Assert.That(energy.Unit, Does.Contain("J").Or.Contain("Nm"));
    }
    
    [Test]
    public void DimensionalOperation_MixedUnits_MaintainsPrecision()
    {
        // Test with imperial and metric mix
        
        // Arrange
        var length1 = Quantity.Parse(1, "ft");
        var length2 = Quantity.Parse(1, "m");
        
        // Act - multiply and convert
        var area = length1 * length2;
        var areaM2 = area.Convert("m^2");
        var areaFt2 = area.Convert("ft^2");
        
        // Round-trip check
        var backArea = Quantity.Parse(areaM2, "m^2");
        var backToFt2 = backArea.Convert("ft^2");
        
        // Assert - round-trip should be very close
        Assert.That(backToFt2, Is.EqualTo(areaFt2).Within(0.0001m));
    }
    
    [Test]
    public void SimpleConversion_StillExact()
    {
        // Verify simple conversions (which were already exact) still work
        
        // Arrange
        var miles = Quantity.Parse(1, "mile");
        
        // Act
        var inches = miles.Convert("in");
        
        // Assert
        Assert.That(inches, Is.EqualTo(63360m));
    }
}
