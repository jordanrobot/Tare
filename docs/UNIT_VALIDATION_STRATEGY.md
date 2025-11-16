# Unit Validation Strategy

## Purpose

This document outlines the strategy for ensuring unit conversion correctness in the Tare library. Given the critical nature of accurate unit conversions in scientific, engineering, and financial applications, we maintain rigorous validation procedures.

## Validation Approach

### 1. Independent Test Project

All unit conversion validation tests are isolated in a dedicated test project (`Tare.UnitValidation.Tests`) separate from functional tests. This separation:

- Clearly distinguishes validation tests from behavioral/functional tests
- Allows independent execution and reporting of conversion accuracy
- Enables focused attention on this critical aspect of the library
- Facilitates adversarial testing by multiple models/reviewers

### 2. Authoritative Reference Sources

All conversion factors are derived from internationally recognized authoritative sources:

- **NIST SP 811 (2008)** - Guide for the Use of the International System of Units (SI)
- **NIST SP 1038** - The International System of Units (SI) - Conversion Factors for General Use
- **BIPM (Bureau International des Poids et Mesures)** - The International System of Units (SI) 9th Edition (2019)
- **ISO 80000-1:2009** - Quantities and units - Part 1: General
- **1959 International Yard and Pound Agreement** - Exact definitions for imperial/US customary units

### 3. Test Coverage Requirements

Every unit conversion test must include:

1. **Inline Source Reference** - Direct citation of the authoritative source
2. **Exact Definition** - The precise conversion factor from the source
3. **Calculation Explanation** - How the expected value was derived
4. **Precision Tolerance** - Appropriate tolerance level (typically 7 decimal places)

### 4. Test Categories

#### Primary Conversion Tests
- Direct conversions between base units (e.g., inch → meter)
- Verify against exact definitions from authoritative sources
- Test both directions where applicable

#### Bidirectional Consistency Tests
- Verify A → B → A maintains precision
- Ensures no accumulation of rounding errors
- Validates symmetry of conversion factors

#### Multi-Step Conversion Tests
- Complex conversion chains (e.g., mile → meter → kilometer)
- Validates consistency across unit systems
- Ensures transitive property holds

#### Edge Case Tests
- Zero values
- Very small values (near precision limits)
- Very large values (scale testing)
- Boundary conditions

### 5. Validation Before Publishing

Before each release, the following validation steps must be completed:

#### Pre-Commit Validation
1. Run all unit validation tests (`dotnet test Tare.UnitValidation.Tests`)
2. Verify 100% pass rate
3. Review any precision warnings

#### Pre-Release Validation
1. Run comprehensive test suite
2. Generate accuracy audit report
3. Review confidence intervals for all unit categories
4. Document any known limitations or precision concerns
5. Adversarial testing by independent reviewers/models

#### Continuous Integration
1. All validation tests run on every commit
2. Failures block merge/deployment
3. Precision degradation is flagged

### 6. Code Update Procedure

When modifying unit definitions or conversion logic:

1. **Document Change Intent**
   - Specify which units are affected
   - Provide justification for changes
   - Reference authoritative sources

2. **Update Tests**
   - Add or modify validation tests
   - Include inline references
   - Verify against multiple sources when possible

3. **Run Full Validation Suite**
   - Execute all unit validation tests
   - Generate audit report
   - Compare with baseline

4. **Peer Review**
   - Code review focusing on conversion accuracy
   - Verification of reference sources
   - Independent calculation verification

5. **Documentation Update**
   - Update audit report
   - Document any precision changes
   - Update changelog with conversion-related changes

### 7. Adversarial Testing Protocol

To ensure maximum confidence in conversion accuracy:

#### Multi-Model Validation
- Periodically submit unit definitions and test results to multiple AI models
- Request independent verification of conversion factors
- Compare results across models to identify discrepancies

#### Independent Human Review
- Periodic review by domain experts (physicists, engineers, metrologists)
- Cross-reference with alternative authoritative sources
- Validate interpretation of standards documents

#### Continuous Monitoring
- Track precision degradation over time
- Monitor for rounding error accumulation
- Alert on any unexpected test failures

### 8. Precision Management

#### Decimal Precision
- All conversions use `decimal` type for maximum precision
- Rational arithmetic used where appropriate for exact conversions
- Minimum 7 decimal place accuracy maintained

#### Known Limitations
- Document any units with inherent precision limitations
- Clearly mark approximate vs. exact conversions
- Provide confidence intervals for derived units

#### Tolerance Levels
- Standard tolerance: 1E-7 (7 decimal places)
- Exact conversions (rational): Zero tolerance acceptable
- Temperature conversions: May require adjusted tolerance

### 9. Audit Trail

Every conversion factor must maintain:
- Source reference (standard document and section)
- Date of last verification
- Confidence level (Exact, High, Medium, Low)
- Any known issues or limitations

### 10. Reporting

#### Accuracy Audit Report
Generated for each release, containing:
- Summary of all tested conversions
- Pass/fail statistics by category
- Confidence intervals
- Known issues or limitations
- Comparison with previous release

#### Continuous Monitoring Dashboard
- Real-time test results
- Precision trends
- Historical accuracy metrics
- Failure alerts

## Implementation Checklist

- [x] Create dedicated validation test project
- [x] Implement comprehensive conversion tests
- [x] Include inline source references
- [x] Document validation strategy (this document)
- [ ] Generate initial accuracy audit report
- [ ] Set up CI pipeline for validation tests
- [ ] Establish adversarial testing schedule
- [ ] Create monitoring dashboard

## Maintenance

This strategy document should be reviewed and updated:
- Annually, or
- When new unit categories are added, or
- When validation procedures change, or
- After significant library updates

## References

- NIST SP 811: https://www.nist.gov/pml/special-publication-811
- NIST SP 1038: https://www.nist.gov/publications/sp1038
- BIPM SI Brochure: https://www.bipm.org/en/publications/si-brochure/
- ISO 80000 series: https://www.iso.org/standard/31887.html
- Frink Programming Language Units Database: https://frinklang.org/frinkdata/units.txt (referenced but network-blocked)
