namespace Tare.Internal;

/// <summary>
/// Represents an exact rational number as a normalized fraction.
/// Used for precise unit conversion factor calculations.
/// </summary>
public readonly struct Rational : IEquatable<Rational>, IComparable<Rational>
{
    /// <summary>
    /// Gets the numerator of the normalized fraction.
    /// </summary>
    public long Numerator { get; }
    
    /// <summary>
    /// Gets the denominator of the normalized fraction (always positive).
    /// </summary>
    public long Denominator { get; }
    
    /// <summary>
    /// Constructs a normalized rational number.
    /// </summary>
    /// <param name="numerator">The numerator.</param>
    /// <param name="denominator">The denominator (must not be zero).</param>
    /// <exception cref="ArgumentException">Thrown when denominator is zero.</exception>
    public Rational(long numerator, long denominator)
    {
        if (denominator == 0)
        {
            throw new ArgumentException("Denominator cannot be zero.", nameof(denominator));
        }
        
        // Handle zero numerator special case
        if (numerator == 0)
        {
            Numerator = 0;
            Denominator = 1;
            return;
        }
        
        // Move sign to numerator (make denominator positive)
        if (denominator < 0)
        {
            numerator = -numerator;
            denominator = -denominator;
        }
        
        // Normalize by dividing by GCD
        var gcd = GreatestCommonDivisor(Math.Abs(numerator), denominator);
        Numerator = numerator / gcd;
        Denominator = denominator / gcd;
    }
    
    /// <summary>
    /// Returns the rational number representing zero (0/1).
    /// </summary>
    public static Rational Zero => new(0, 1);
    
    /// <summary>
    /// Returns the rational number representing one (1/1).
    /// </summary>
    public static Rational One => new(1, 1);
    
    /// <summary>
    /// Computes the greatest common divisor using the Euclidean algorithm.
    /// </summary>
    private static long GreatestCommonDivisor(long a, long b)
    {
        // Handle edge case: GCD(0, n) = n
        if (a == 0) return b;
        if (b == 0) return a;
        
        // Euclidean algorithm
        while (b != 0)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }
    
    #region Arithmetic Operators
    
    /// <summary>
    /// Adds two rational numbers.
    /// </summary>
    public static Rational operator +(Rational a, Rational b)
    {
        checked
        {
            // (a/b) + (c/d) = (a*d + b*c) / (b*d)
            var numerator = a.Numerator * b.Denominator + b.Numerator * a.Denominator;
            var denominator = a.Denominator * b.Denominator;
            return new Rational(numerator, denominator);
        }
    }
    
    /// <summary>
    /// Subtracts two rational numbers.
    /// </summary>
    public static Rational operator -(Rational a, Rational b)
    {
        checked
        {
            // (a/b) - (c/d) = (a*d - b*c) / (b*d)
            var numerator = a.Numerator * b.Denominator - b.Numerator * a.Denominator;
            var denominator = a.Denominator * b.Denominator;
            return new Rational(numerator, denominator);
        }
    }
    
    /// <summary>
    /// Multiplies two rational numbers.
    /// </summary>
    public static Rational operator *(Rational a, Rational b)
    {
        checked
        {
            // (a/b) * (c/d) = (a*c)/(b*d)
            var numerator = a.Numerator * b.Numerator;
            var denominator = a.Denominator * b.Denominator;
            return new Rational(numerator, denominator);
        }
    }
    
    /// <summary>
    /// Divides two rational numbers.
    /// </summary>
    /// <exception cref="DivideByZeroException">Thrown when divisor is zero.</exception>
    public static Rational operator /(Rational a, Rational b)
    {
        if (b.Numerator == 0)
        {
            throw new DivideByZeroException("Cannot divide by zero rational.");
        }
        
        checked
        {
            // (a/b) / (c/d) = (a/b) * (d/c) = (a*d)/(b*c)
            var numerator = a.Numerator * b.Denominator;
            var denominator = a.Denominator * b.Numerator;
            return new Rational(numerator, denominator);
        }
    }
    
    /// <summary>
    /// Negates a rational number.
    /// </summary>
    public static Rational operator -(Rational a)
    {
        return new Rational(-a.Numerator, a.Denominator);
    }
    
    #endregion
    
    #region Conversion Methods
    
    /// <summary>
    /// Converts the rational number to a decimal value.
    /// </summary>
    public decimal ToDecimal()
    {
        return (decimal)Numerator / (decimal)Denominator;
    }
    
    /// <summary>
    /// Creates a rational number from a decimal value.
    /// </summary>
    public static Rational FromDecimal(decimal value)
    {
        // Extract the internal representation of decimal
        var bits = decimal.GetBits(value);
        var lo = (uint)bits[0];
        var mid = (uint)bits[1];
        var hi = (uint)bits[2];
        var flags = bits[3];
        
        // Extract sign and scale
        var isNegative = (flags & 0x80000000) != 0;
        var scale = (flags >> 16) & 0xFF;
        
        // Build the mantissa as a 96-bit value
        // For most practical unit conversions, we can work with this directly
        // We need to be careful about overflow
        
        // Try to construct from the bits
        decimal absValue = Math.Abs(value);
        
        // Multiply by 10^scale to get integer mantissa
        long mantissa;
        long denominator = 1;
        
        // Calculate denominator = 10^scale
        for (int i = 0; i < scale; i++)
        {
            denominator *= 10;
        }
        
        // Now we need to convert the decimal mantissa to long
        // decimal stores 96-bit mantissa, but we only have 64-bit long
        // For values that fit in long range, this works:
        try
        {
            // Truncate to integer part after scaling
            decimal scaledValue = absValue * denominator;
            
            // Check if it fits in long
            if (scaledValue > long.MaxValue)
            {
                // Value too large - use approximation
                // Divide both mantissa and denominator until it fits
                while (scaledValue > long.MaxValue && denominator > 1)
                {
                    scaledValue /= 10;
                    denominator /= 10;
                }
                
                if (scaledValue > long.MaxValue)
                {
                    // Still too large, use the decimal value directly with power of 10
                    mantissa = (long)decimal.Truncate(absValue * 1000000000); // Use 10^9
                    denominator = 1000000000;
                }
                else
                {
                    mantissa = (long)decimal.Truncate(scaledValue);
                }
            }
            else
            {
                mantissa = (long)decimal.Truncate(scaledValue);
            }
        }
        catch (OverflowException)
        {
            // Fallback: approximate with 10^9 precision
            mantissa = (long)decimal.Truncate(absValue * 1000000000);
            denominator = 1000000000;
        }
        
        if (isNegative)
        {
            mantissa = -mantissa;
        }
        
        return new Rational(mantissa, denominator);
    }
    
    /// <summary>
    /// Explicitly converts a rational number to a decimal.
    /// </summary>
    public static explicit operator decimal(Rational r)
    {
        return r.ToDecimal();
    }
    
    /// <summary>
    /// Explicitly converts a decimal to a rational number.
    /// </summary>
    public static explicit operator Rational(decimal d)
    {
        return FromDecimal(d);
    }
    
    #endregion
    
    #region Utility Methods
    
    /// <summary>
    /// Returns the reciprocal of this rational number.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when attempting to get reciprocal of zero.</exception>
    public Rational Reciprocal()
    {
        if (Numerator == 0)
        {
            throw new InvalidOperationException("Cannot compute reciprocal of zero.");
        }
        
        return new Rational(Denominator, Numerator);
    }
    
    /// <summary>
    /// Returns true if this rational number is zero.
    /// </summary>
    public bool IsZero()
    {
        return Numerator == 0;
    }
    
    /// <summary>
    /// Returns true if this rational number is one.
    /// </summary>
    public bool IsOne()
    {
        return Numerator == 1 && Denominator == 1;
    }
    
    #endregion
    
    #region Equality and Comparison
    
    /// <summary>
    /// Determines whether this rational number equals another.
    /// </summary>
    public bool Equals(Rational other)
    {
        // Since both are normalized, we can compare components directly
        return Numerator == other.Numerator && Denominator == other.Denominator;
    }
    
    /// <summary>
    /// Determines whether this rational number equals an object.
    /// </summary>
    public override bool Equals(object? obj)
    {
        return obj is Rational other && Equals(other);
    }
    
    /// <summary>
    /// Returns the hash code for this rational number.
    /// </summary>
    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 31 + Numerator.GetHashCode();
            hash = hash * 31 + Denominator.GetHashCode();
            return hash;
        }
    }
    
    /// <summary>
    /// Determines whether two rational numbers are equal.
    /// </summary>
    public static bool operator ==(Rational a, Rational b)
    {
        return a.Equals(b);
    }
    
    /// <summary>
    /// Determines whether two rational numbers are not equal.
    /// </summary>
    public static bool operator !=(Rational a, Rational b)
    {
        return !a.Equals(b);
    }
    
    /// <summary>
    /// Compares this rational number to another.
    /// </summary>
    public int CompareTo(Rational other)
    {
        // Compare a/b with c/d by comparing a*d with b*c
        checked
        {
            var left = Numerator * other.Denominator;
            var right = other.Numerator * Denominator;
            return left.CompareTo(right);
        }
    }
    
    /// <summary>
    /// Determines whether one rational number is less than another.
    /// </summary>
    public static bool operator <(Rational a, Rational b)
    {
        return a.CompareTo(b) < 0;
    }
    
    /// <summary>
    /// Determines whether one rational number is greater than another.
    /// </summary>
    public static bool operator >(Rational a, Rational b)
    {
        return a.CompareTo(b) > 0;
    }
    
    /// <summary>
    /// Determines whether one rational number is less than or equal to another.
    /// </summary>
    public static bool operator <=(Rational a, Rational b)
    {
        return a.CompareTo(b) <= 0;
    }
    
    /// <summary>
    /// Determines whether one rational number is greater than or equal to another.
    /// </summary>
    public static bool operator >=(Rational a, Rational b)
    {
        return a.CompareTo(b) >= 0;
    }
    
    #endregion
    
    /// <summary>
    /// Returns a string representation of this rational number.
    /// </summary>
    public override string ToString()
    {
        if (Denominator == 1)
        {
            return Numerator.ToString();
        }
        return $"{Numerator}/{Denominator}";
    }
}
