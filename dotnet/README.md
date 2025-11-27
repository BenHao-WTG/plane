# Plane .NET Core Implementation

This directory contains the .NET Core implementation of the Plane project management application.

## Overview

The original Plane application is built with:
- **Frontend**: Next.js with React and TypeScript
- **Backend**: Django/Python API

This .NET Core implementation provides:
- **Frontend**: Blazor Web App (replacing React/Next.js)
- **Backend**: ASP.NET Core Web API (replacing Django)
- **Types**: C# classes (replacing TypeScript interfaces)
- **Services**: C# service layer

## Project Structure

```
dotnet/
├── Plane.Web/           # Blazor Web Application
│   └── Components/      # Blazor components (converted from React/TSX)
│       └── Issues/      # Issue-related components
├── Plane.Types/         # Type definitions (converted from TypeScript)
├── Plane.Services/      # Service layer for API calls
└── Plane.UI/           # Reusable UI components
```

## Prerequisites

- .NET 9.0 SDK or later
- Visual Studio 2022, Visual Studio Code, or Rider

## Getting Started

### Build the Solution

```bash
dotnet build Plane.sln
```

### Run the Web Application

```bash
cd dotnet/Plane.Web/Plane.Web
dotnet run
```

The application will be available at `https://localhost:5001` or `http://localhost:5000`.

## Conversion Examples

### TypeScript to C# Types

**Original TypeScript (`packages/types/src/auth.ts`):**
```typescript
export interface IEmailCheckData {
  email: string;
}

export interface ILoginTokenResponse {
  access_token: string;
  refresh_token: string;
}
```

**Converted C# (`Plane.Types/Auth.cs`):**
```csharp
public class EmailCheckData
{
    public string Email { get; set; } = string.Empty;
}

public class LoginTokenResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}
```

### React/TSX to Blazor Components

**Original React Component (`apps/web/core/components/issues/confirm-issue-discard.tsx`):**
```tsx
export const ConfirmIssueDiscard: React.FC<Props> = (props) => {
  const { isOpen, handleClose, onDiscard, onConfirm } = props;
  const [isLoading, setIsLoading] = useState(false);
  
  return (
    <Dialog>
      {/* JSX content */}
    </Dialog>
  );
};
```

**Converted Blazor Component (`Plane.Web/Components/Issues/ConfirmIssueDiscard.razor`):**
```razor
<div class="@(IsOpen ? "fixed" : "hidden")">
    <!-- Razor content -->
</div>

@code {
    [Parameter]
    public bool IsOpen { get; set; }
    
    private bool IsLoading { get; set; }
}
```

## Key Conversion Patterns

### 1. Component Structure
- **React**: Functional components with hooks
- **Blazor**: Razor components with `@code` blocks

### 2. State Management
- **React**: `useState`, `useEffect`, Context API, MobX
- **Blazor**: Component properties, `StateHasChanged()`, Dependency Injection

### 3. Props/Parameters
- **React**: Props passed to components
- **Blazor**: `[Parameter]` attributes

### 4. Event Handling
- **React**: Callback props (e.g., `onClick`)
- **Blazor**: `EventCallback` parameters and `@onclick` directives

### 5. Styling
- **React**: Tailwind CSS with className
- **Blazor**: Tailwind CSS with class attribute

## Development Status

This is a partial conversion demonstrating the conversion approach. The full application consists of:
- **3,541+ TypeScript files** to be converted
- **Multiple applications**: admin, web, space, live, proxy
- **14 shared packages**: UI components, utilities, types, services, etc.

### Completed Conversions:
- ✅ Solution structure setup
- ✅ Blazor Web project created
- ✅ Type definitions (Auth types converted)
- ✅ Sample component (ConfirmIssueDiscard converted)

### Remaining Conversions:
The full conversion would require:
1. Converting all remaining TypeScript types to C# classes
2. Converting all React components to Blazor components
3. Converting API service calls to HttpClient-based services
4. Setting up authentication and authorization
5. Migrating state management from MobX to Blazor state patterns
6. Converting all UI components and utilities
7. Setting up routing in ASP.NET Core
8. Configuring build and deployment pipelines

## Architecture Changes

### Frontend
- **React Router** → **Blazor Router**
- **Next.js SSR** → **Blazor Server or WebAssembly**
- **MobX State** → **Blazor State Management**
- **npm/pnpm packages** → **NuGet packages**

### Backend
The existing Django/Python API can remain as-is, or be converted to:
- **Django REST Framework** → **ASP.NET Core Web API**
- **Python models** → **C# Entity Framework models**

## Testing

Run tests with:
```bash
dotnet test
```

## Building for Production

```bash
dotnet publish -c Release -o ./publish
```

## Contributing

When converting additional TypeScript files to C#:
1. Maintain the same folder structure
2. Follow C# naming conventions (PascalCase)
3. Add XML documentation comments
4. Ensure null safety with nullable reference types
5. Use async/await patterns consistently

## License

This project maintains the same AGPL-3.0 license as the original Plane project.
