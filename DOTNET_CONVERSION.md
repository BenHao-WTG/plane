# .NET Core Conversion

This repository includes a .NET Core implementation of the Plane project management application.

## Branch: netcore

The TypeScript to .NET Core conversion is available on the **netcore** branch (also tracked in the copilot branch).

## Quick Start

### View the Conversion

```bash
# Checkout the netcore branch
git checkout netcore

# Navigate to the .NET implementation
cd dotnet/

# Build the solution
dotnet build ../Plane.sln

# Run the web application
cd Plane.Web/Plane.Web
dotnet run
```

## What's Included

The `dotnet/` directory contains:

- **Plane.sln** - Main .NET 9.0 solution file (at repository root)
- **Plane.Web/** - Blazor Web Application (converted from Next.js/React)
- **Plane.Types/** - C# type definitions (converted from TypeScript)
- **Plane.Services/** - Service layer with HttpClient (converted from axios services)
- **README.md** - Getting started guide
- **CONVERSION_GUIDE.md** - Comprehensive 14KB guide with conversion patterns
- **CONVERSION_SUMMARY.md** - Implementation status and statistics

## Conversion Statistics

- **Total TypeScript Files**: 3,541+
- **Sample Conversions**: 3 representative files demonstrating patterns
- **Build Status**: ✅ Successful
- **Documentation**: 3 comprehensive guides

## Key Conversions Demonstrated

1. **TypeScript Types → C# Classes** (`packages/types/src/auth.ts` → `Plane.Types/Auth.cs`)
2. **React Components → Blazor** (`apps/web/.../*.tsx` → `Plane.Web/Components/*.razor`)
3. **API Services → HttpClient** (axios → C# HttpClient services)

## Documentation

See the `dotnet/` directory for:

- **README.md** - Project overview and getting started
- **CONVERSION_GUIDE.md** - Detailed conversion patterns including:
  - Type conversion examples
  - Component conversion patterns
  - State management migration
  - Hooks to lifecycle methods
  - Routing patterns
  - API service patterns
  - Common pitfalls and solutions
- **CONVERSION_SUMMARY.md** - Implementation details and statistics

## Build Requirements

- .NET 9.0 SDK or later
- Any C# IDE (Visual Studio 2022, VS Code, Rider)

## Original Code

All original TypeScript code remains intact and unchanged. The .NET implementation is additive and located entirely within the `dotnet/` directory.

## Scope

This is a **demonstration implementation** showing the conversion approach. The implementation provides:

- ✅ Working .NET solution structure
- ✅ Build pipeline configured
- ✅ Representative conversions demonstrating all key patterns
- ✅ Comprehensive conversion documentation

A complete conversion of all 3,541+ TypeScript files would follow the same patterns documented in the conversion guide.
