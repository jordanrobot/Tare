# GitHub Copilot Configuration

This repository is configured with comprehensive GitHub Copilot instructions to help both the coding agent and developers working with Copilot.

## Overview

The Copilot configuration provides context about the Tare library's architecture, coding standards, and development practices to ensure consistent, high-quality contributions.

## Configuration Files

### Repository Instructions
- **`.github/copilot-instructions.md`**: Main repository-specific instructions that provide context about the Tare library, its goals, coding rules, and safety guidelines.

### Pattern-Based Instructions
Located in `.github/instructions/`, these files apply specific rules based on file patterns:

- **`dotnet-architecture-good-practices.instructions.md`**: DDD and .NET architecture guidelines for C# files
  - Applies to: `**/*.cs`, `**/*.csproj`, `**/Program.cs`, `**/*.razor`
  - Focuses on Domain-Driven Design, SOLID principles, and .NET best practices

- **`taming-copilot.instructions.md`**: General coding philosophy and interaction rules
  - Applies to: `**` (all files)
  - Emphasizes minimal changes, surgical edits, and preserving existing code

### Custom Agents
Located in `.github/agents/`:

- **`CSharpExpert.agent.md`**: Specialized C#/.NET development agent
  - Provides expert guidance on .NET tasks
  - Includes best practices for async programming, testing, error handling, and more

## How It Works

When you use GitHub Copilot in this repository:

1. The **repository instructions** provide overall context about the project
2. **Pattern-based instructions** apply automatically based on the files you're editing
3. **Custom agents** can be invoked for specialized tasks

## For Contributors

These instructions help ensure:

- **Consistency**: All contributions follow the same coding standards and architectural patterns
- **Quality**: Code adheres to best practices for immutability, value semantics, and decimal precision
- **Safety**: Changes are minimal, surgical, and preserve existing behavior
- **Testing**: All changes include appropriate tests following the `MethodName_Condition_ExpectedResult()` convention

## Maintaining the Configuration

When updating Copilot instructions:

1. Keep changes focused and relevant to the project's needs
2. Test that instructions don't conflict with each other
3. Ensure frontmatter is properly formatted:
   - `.instructions.md` files need `applyTo` and `description`
   - `.agent.md` files need `name` and `description`
4. Update this documentation if you add new instruction files or agents

## References

- [GitHub Copilot Best Practices](https://gh.io/copilot-coding-agent-tips)
- [Copilot Instructions Documentation](https://docs.github.com/en/copilot/customizing-copilot/adding-custom-instructions-for-github-copilot)
