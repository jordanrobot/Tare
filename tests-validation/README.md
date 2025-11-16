# Tare Unit Validation Tests

This test project is **dedicated exclusively to validating unit conversion accuracy** in the Tare library.

## Purpose

Unit conversion accuracy is **critical** for scientific, engineering, and financial applications. This separate test project:

- Isolates conversion validation from functional/behavioral tests
- Enables focused accuracy auditing
- Facilitates adversarial testing by multiple reviewers/models
- Provides clear documentation of conversion sources

## Test Methodology

Every test in this project:

1. **Uses Authoritative Sources**: NIST SP 811, BIPM SI Brochure, ISO 80000 standards
2. **Includes Inline Citations**: Each test documents its source, definition, and calculation
3. **Independent Verification**: Test values NOT derived from implementation code
4. **High Precision**: 7+ decimal place accuracy (TOLERANCE = 1E-7)

## Test Categories

- **Primary Conversions**: Direct unit-to-unit conversions with exact definitions
- **Bidirectional Tests**: Verify A→B→A maintains precision (no rounding accumulation)
- **Multi-Step Tests**: Validate transitive property across conversion chains
- **Edge Cases**: Zero, very small, and very large values

## Running Tests

```bash
# Run all validation tests
dotnet test Tare.UnitValidation.Tests

# Run with detailed output
dotnet test Tare.UnitValidation.Tests --logger "console;verbosity=detailed"

# Run specific category
dotnet test --filter "FullyQualifiedName~Length"
```

## Interpreting Results

- **100% Pass Rate Required**: Any failure indicates a critical accuracy issue
- **Tolerance**: 1E-7 (7 decimal places) - adequate for most applications
- **Confidence Levels**:
  - **Exact**: Mathematically exact by definition (e.g., 1 inch = 25.4 mm exactly)
  - **High**: Precision >10 decimal places (e.g., mathematical constants)

## Documentation

See also:
- `/docs/UNIT_VALIDATION_STRATEGY.md` - Comprehensive validation strategy
- `/docs/UNIT_ACCURACY_AUDIT.md` - Current accuracy audit report

## Maintenance

- **Before each release**: Run full validation suite and generate audit report
- **After code changes**: Run validation tests in CI/CD pipeline
- **Periodic review**: Re-validate against updated standards annually

## Source References

All tests reference authoritative sources:

- **NIST SP 811**: https://www.nist.gov/pml/special-publication-811
- **NIST SP 1038**: https://www.nist.gov/publications/sp1038
- **BIPM SI Brochure**: https://www.bipm.org/en/publications/si-brochure/
- **ISO 80000 series**: https://www.iso.org/standard/31887.html

## Contact

For questions about conversion accuracy or test methodology, please open an issue on GitHub.
