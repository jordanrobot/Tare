# F-012 Implementation Summary

**Quick Reference Guide for Feature F-012: Error Handling and Diagnostics**

---

## TL;DR

**Goal**: Standardize error handling and fix critical null-handling bug

**Status**: ✅ **Specification Complete - Ready for Implementation**

**Effort**: 1-2 days (M)

**Impact**: Better developer experience, fixes test failure, zero breaking changes

---

## What Gets Fixed

### 🐛 Critical Bug (BLOCKING)
**Problem**: `CompositeParser.TryParse(null, ...)` throws instead of returning false
**Impact**: Test failure `TryParse_NullInput_ReturnsFalse`
**Fix**: Add guard clause at method start
**Time**: <1 hour

### 📝 Error Message Standardization
**Problem**: Inconsistent error messages across 25+ throw sites
**Impact**: Confusing developer experience, hard to debug
**Fix**: Apply consistent format and ubiquitous language
**Time**: 4-6 hours

### ✅ Test Coverage
**Problem**: Not all exception scenarios tested
**Impact**: Potential regressions, unclear error behavior
**Fix**: Ensure 100% of throw statements have tests
**Time**: 2-4 hours

---

## Implementation Phases

### Phase 1: Critical Bug Fix (URGENT)
**File**: `src/Internal/Units/CompositeParser.cs`

**Before**:
```csharp
public bool TryParse(string compositeUnit, out DimensionSignature signature, out decimal factor)
{
    // BUG: ConcurrentDictionary throws on null key
    if (_parseCache.TryGetValue(compositeUnit, out var cachedResult))
    {
        // ...
    }
}
```

**After**:
```csharp
public bool TryParse(string compositeUnit, out DimensionSignature signature, out decimal factor)
{
    // Guard against null/empty input (TryParse should not throw)
    if (string.IsNullOrWhiteSpace(compositeUnit))
    {
        signature = DimensionSignature.Dimensionless;
        factor = 1m;
        return false;
    }
    
    // Safe to check cache now
    if (_parseCache.TryGetValue(compositeUnit, out var cachedResult))
    {
        // ...
    }
}
```

**Verify**: Test `TryParse_NullInput_ReturnsFalse` should pass

---

### Phase 2: Message Standardization

**Standard Format**: `"Cannot [action]. [Reason]. [Optional guidance]."`

**Examples**:

✅ **Good** (keep as-is):
```csharp
throw new ArgumentException(
    "Unknown or malformed unit: 'xyz'. " +
    "Unit must be either a valid catalog unit or a composite unit. " +
    "Use UnitDefinitions.IsValidUnit() to check catalog units.",
    nameof(unit));
```

⚠️ **Needs Enhancement**:
```csharp
// Current
throw new InvalidOperationException("Cannot add quantities of incompatible units.");

// Enhanced
throw new InvalidOperationException(
    "Cannot add quantities with incompatible units. " +
    "Quantities must have the same dimension (e.g., Length + Length). " +
    $"Attempted: {UnitType} + {other.UnitType}");
```

⚠️ **Needs Consistency**:
```csharp
// Current (inconsistent capitalization)
throw new InvalidOperationException("Cannot divide integers by quantities with units");

// Fixed
throw new InvalidOperationException(
    "Cannot divide integers by quantities with units. " +
    "Division result would have inverse units.");
```

**Files to Review**:
- `src/Quantity.cs` (15+ throw statements)
- `src/UnitDefinitions.cs` (1 throw)
- `src/Internal/Rational.cs` (3 throws)
- `src/Internal/Units/*.cs` (~10 throws across multiple files)

---

### Phase 3: Test Coverage Expansion

**Ensure Every Exception Scenario Tested**

**Test Naming**: `MethodName_Condition_ExpectedResult()`

**Required Tests**:

```csharp
// Null handling
[Test]
public void Constructor_NullUnit_ThrowsArgumentNullException()
{
    var ex = Assert.Throws<ArgumentNullException>(() => new Quantity(10, null));
    Assert.That(ex.ParamName, Is.EqualTo("unit"));
}

// Empty/whitespace handling
[Test]
public void Constructor_EmptyUnit_ThrowsArgumentException()
{
    var ex = Assert.Throws<ArgumentException>(() => new Quantity(10, ""));
    Assert.That(ex.ParamName, Is.EqualTo("unit"));
    Assert.That(ex.Message, Does.Contain("empty or whitespace"));
}

// Incompatible operations with message verification
[Test]
public void Add_IncompatibleUnits_ThrowsInvalidOperationExceptionWithContext()
{
    var length = Quantity.Parse("10 m");
    var mass = Quantity.Parse("5 kg");
    
    var ex = Assert.Throws<InvalidOperationException>(() => _ = length + mass);
    Assert.That(ex.Message, Does.Contain("incompatible"));
    Assert.That(ex.Message, Does.Contain("Length"));
    Assert.That(ex.Message, Does.Contain("Mass"));
}

// TryParse null handling (already exists, should pass after Phase 1)
[Test]
public void TryParse_NullInput_ReturnsFalse()
{
    var success = _parser.TryParse(null, out var signature, out var factor);
    Assert.That(success, Is.False);
}
```

**Coverage Goal**: 100% of throw statements

---

## Exception Type Standards

| Scenario | Exception Type | When to Use |
|----------|---------------|-------------|
| Null parameter | `ArgumentNullException` | Parameter is null |
| Empty/whitespace string | `ArgumentException` | String is empty or whitespace |
| Unknown/malformed unit | `ArgumentException` | Unit not in catalog and not valid composite |
| Incompatible operation | `InvalidOperationException` | Adding Length to Mass, etc. |
| Malformed composite syntax | `FormatException` | Composite parsing fails structurally |
| Division by zero | `DivideByZeroException` | Dividing by zero value |

---

## Ubiquitous Language in Error Messages

**Always Use These Terms**:
- ✅ Quantity (not "value", "number", "amount")
- ✅ Unit (not "unit of measure", "UOM")
- ✅ Catalog (not "definition", "lookup")
- ✅ Composite (not "complex", "compound", "derived")
- ✅ Signature (not "dimension vector", "exponents")
- ✅ Dimension (not "dimension type", "unit type")

**Examples**:
```csharp
// Good
"Unit must be either a valid catalog unit or a composite unit."

// Bad
"Unit must be in the definition list or be a compound unit."
```

---

## Quality Gates

### Before Committing
- [ ] All tests pass (including `TryParse_NullInput_ReturnsFalse`)
- [ ] Every throw statement has a test
- [ ] All error messages follow standard format
- [ ] All error messages use ubiquitous language
- [ ] XML `<exception>` tags updated

### Before PR Merge
- [ ] Code review passed
- [ ] CHANGELOG.md updated
- [ ] Build succeeds with zero warnings related to changes
- [ ] Test coverage at 100% for exception paths

---

## Breaking Changes

✅ **NONE** - This feature is 100% backwards compatible

**Why No Breaking Changes**:
1. Exception types unchanged (still standard .NET exceptions)
2. Error messages enhanced, not removed
3. Null handling fix only affects broken scenarios
4. Tests may need updates if they assert exact message text

**Migration Guide**: Not needed (backwards compatible)

---

## Documentation Updates

### Required
- [ ] XML `<exception>` tags on all public APIs
- [ ] CHANGELOG.md entry for F-012

### Optional
- [ ] Contributor guide for error handling patterns
- [ ] README examples showing error handling

---

## Success Metrics

### Objective
- ✅ Test suite passes 100%
- ✅ Zero throw statements without tests
- ✅ All public APIs have XML exception docs

### Subjective  
- ✅ Error messages clear and actionable
- ✅ Consistent terminology throughout
- ✅ Team agrees error handling is improved

---

## Out of Scope (Not in F-012)

- ❌ Custom exception types (`TareException`, etc.)
- ❌ Localization of error messages
- ❌ Structured logging framework
- ❌ Error recovery mechanisms
- ❌ Public diagnostics API (deferred to F-013)

---

## Timeline

**Total Estimated Time**: 1-2 days

**Day 1**:
- Morning: Fix critical null handling bug, verify test passes
- Afternoon: Standardize error messages across all files

**Day 2**:
- Morning: Add/enhance tests for 100% exception coverage
- Afternoon: Update documentation, final verification

---

## Dependencies

### Consumes
- **F-011**: Uses cache statistics (already implemented)

### Enables
- **F-013**: API Helpers (may expose diagnostics publicly)

---

## Risk Assessment

| Risk | Likelihood | Impact | Mitigation |
|------|-----------|--------|------------|
| Test brittleness from message assertions | Medium | Low | Assert on keywords, not exact text |
| Performance impact of guards | Low | Low | Guards are cheap, fail-fast saves work |
| Breaking consumer code parsing messages | Low | Medium | Document: catch types, not parse messages |

---

## Key Decisions

1. **TryParse Null Handling**: Return false, don't throw
2. **Error Message Format**: "Cannot [action]. [Reason]. [Guidance]."
3. **Diagnostics**: Keep cache stats internal, defer public API to F-013
4. **Exception Types**: Use standard .NET, no custom types

---

## Quick Command Reference

```bash
# Build project
dotnet build

# Run all tests
dotnet test

# Run specific test
dotnet test --filter "TryParse_NullInput_ReturnsFalse"

# Check for coverage gaps (if tool installed)
dotnet-coverage collect -f cobertura -o coverage.xml dotnet test
```

---

## Files to Modify

**Critical (Phase 1)**:
- `src/Internal/Units/CompositeParser.cs` (null guard)

**Message Standardization (Phase 2)**:
- `src/Quantity.cs` (~15 throw statements)
- `src/UnitDefinitions.cs` (1 throw)
- `src/Internal/Rational.cs` (3 throws)
- `src/Internal/Units/UnitResolver.cs` (~4 throws)
- `src/Internal/Units/DimensionalMath.cs` (1 throw)
- `src/Internal/Units/CompositeFormatter.cs` (1 throw)
- `src/Internal/Units/UnitToken.cs` (2 throws)
- `src/Internal/Units/PreferredUnit.cs` (2 throws)

**Test Enhancement (Phase 3)**:
- `tests/CompositeParserTests.cs` (verify null test passes)
- `tests/QuantityTests.cs` (add missing null/empty tests)
- `tests/QuantityOperatorTests.cs` (enhance incompatible operation tests)
- Various test files (add message content assertions)

**Documentation (Phase 4)**:
- `docs/CHANGELOG.md` (add F-012 entry)
- `src/**/*.cs` (XML comments for exceptions)

---

## References

**Internal**:
- `.github/features/F-012.md` - Complete specification
- `.github/features/E-001.md` - Epic context

**External**:
- [.NET Exception Best Practices](https://learn.microsoft.com/en-us/dotnet/standard/exceptions/best-practices-for-exceptions)
- [Fail-Fast Principle](https://enterprisecraftsmanship.com/posts/fail-fast-principle/)

---

## Getting Started

1. Read full spec: `.github/features/F-012.md`
2. Fix critical bug: Phase 1 (CompositeParser null guard)
3. Standardize messages: Phase 2 (25+ throw sites)
4. Add tests: Phase 3 (100% coverage)
5. Document: Phase 4 (XML comments + CHANGELOG)

**Questions?** Review the "Open Questions" section in F-012.md
