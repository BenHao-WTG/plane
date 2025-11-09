# TypeScript to .NET Core Conversion Guide

This document provides a comprehensive guide for converting the Plane TypeScript codebase to .NET Core.

## Project Overview

**Original Stack:**
- Frontend: Next.js 14, React 18, TypeScript
- State Management: MobX
- Styling: Tailwind CSS
- Build Tool: Turbo (monorepo)
- Package Manager: pnpm

**New Stack:**
- Frontend: Blazor Web App (.NET 9)
- State Management: Blazor component state + services
- Styling: Tailwind CSS (maintained)
- Build Tool: MSBuild
- Package Manager: NuGet

## File Count Statistics

- **Total TypeScript Files**: 3,541+
- **React Components**: ~2,000+
- **Type Definitions**: ~500+
- **Service/API Files**: ~200+

## Conversion Mapping

### 1. Project Structure Conversion

| TypeScript/Node | .NET Core |
|----------------|-----------|
| `packages/types/` | `Plane.Types/` (Class Library) |
| `packages/ui/` | `Plane.UI/` (Razor Class Library) |
| `packages/services/` | `Plane.Services/` (Class Library) |
| `packages/utils/` | `Plane.Utils/` (Class Library) |
| `apps/web/` | `Plane.Web/` (Blazor Web App) |
| `apps/admin/` | `Plane.Admin/` (Blazor Web App) |
| `apps/api/` | Keep as Django or convert to `Plane.Api/` (ASP.NET Core Web API) |

### 2. Language & Syntax Conversion

#### TypeScript Types → C# Classes/Interfaces

**TypeScript:**
```typescript
export interface IUser {
  id: string;
  email: string;
  first_name: string | null;
  last_name: string | null;
  avatar?: string;
  created_at: Date;
  is_active: boolean;
}

export type UserRole = "admin" | "member" | "viewer";
```

**C#:**
```csharp
namespace Plane.Types;

public class User
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Avatar { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
}

public enum UserRole
{
    Admin,
    Member,
    Viewer
}
```

#### React Components → Blazor Components

**TypeScript/React:**
```tsx
import React, { useState } from "react";

interface Props {
  title: string;
  onSubmit: (value: string) => Promise<void>;
  isOpen: boolean;
}

export const MyComponent: React.FC<Props> = ({ title, onSubmit, isOpen }) => {
  const [value, setValue] = useState("");
  const [loading, setLoading] = useState(false);

  const handleSubmit = async () => {
    setLoading(true);
    await onSubmit(value);
    setLoading(false);
  };

  return (
    <div className={isOpen ? "block" : "hidden"}>
      <h2>{title}</h2>
      <input 
        value={value} 
        onChange={(e) => setValue(e.target.value)} 
      />
      <button onClick={handleSubmit} disabled={loading}>
        {loading ? "Loading..." : "Submit"}
      </button>
    </div>
  );
};
```

**Blazor/C#:**
```razor
@* MyComponent.razor *@

<div class="@(IsOpen ? "block" : "hidden")">
    <h2>@Title</h2>
    <input @bind="value" @bind:event="oninput" />
    <button @onclick="HandleSubmit" disabled="@loading">
        @(loading ? "Loading..." : "Submit")
    </button>
</div>

@code {
    [Parameter]
    public string Title { get; set; } = string.Empty;

    [Parameter]
    public EventCallback<string> OnSubmit { get; set; }

    [Parameter]
    public bool IsOpen { get; set; }

    private string value = string.Empty;
    private bool loading = false;

    private async Task HandleSubmit()
    {
        loading = true;
        await OnSubmit.InvokeAsync(value);
        loading = false;
        StateHasChanged();
    }
}
```

### 3. Hooks to Blazor Patterns

#### useState → Component Fields

**React:**
```typescript
const [count, setCount] = useState(0);
const [name, setName] = useState("");
```

**Blazor:**
```csharp
private int count = 0;
private string name = string.Empty;

// To trigger re-render after update:
StateHasChanged();
```

#### useEffect → Lifecycle Methods

**React:**
```typescript
useEffect(() => {
  fetchData();
}, [dependency]);

useEffect(() => {
  return () => cleanup();
}, []);
```

**Blazor:**
```csharp
protected override async Task OnInitializedAsync()
{
    await FetchData();
}

protected override void OnParametersSet()
{
    // Called when parameters change
}

public void Dispose()
{
    // Cleanup
}
```

#### useContext → Dependency Injection

**React:**
```typescript
const { user } = useContext(UserContext);
```

**Blazor:**
```csharp
@inject IUserService UserService

@code {
    private User? user;

    protected override async Task OnInitializedAsync()
    {
        user = await UserService.GetCurrentUser();
    }
}
```

#### Custom Hooks → Services

**React:**
```typescript
export const useIssues = (workspaceId: string) => {
  const [issues, setIssues] = useState<Issue[]>([]);
  const [loading, setLoading] = useState(false);

  const fetchIssues = async () => {
    setLoading(true);
    const data = await issueService.getAll(workspaceId);
    setIssues(data);
    setLoading(false);
  };

  return { issues, loading, fetchIssues };
};
```

**Blazor:**
```csharp
// IssueService.cs
public class IssueService
{
    private readonly HttpClient _httpClient;

    public IssueService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Issue>> GetAllAsync(string workspaceId)
    {
        return await _httpClient.GetFromJsonAsync<List<Issue>>(
            $"/api/workspaces/{workspaceId}/issues"
        ) ?? new List<Issue>();
    }
}

// Component usage:
@inject IssueService IssueService

@code {
    private List<Issue> issues = new();
    private bool loading = false;

    private async Task FetchIssues(string workspaceId)
    {
        loading = true;
        issues = await IssueService.GetAllAsync(workspaceId);
        loading = false;
    }
}
```

### 4. State Management: MobX → Blazor Services

**MobX Store:**
```typescript
class IssueStore {
  @observable issues: Issue[] = [];
  @observable loading = false;

  @action
  async fetchIssues(workspaceId: string) {
    this.loading = true;
    this.issues = await issueService.getAll(workspaceId);
    this.loading = false;
  }

  @computed
  get openIssues() {
    return this.issues.filter(i => i.state === "open");
  }
}
```

**Blazor Service with State:**
```csharp
public class IssueStateService
{
    private List<Issue> _issues = new();
    public IReadOnlyList<Issue> Issues => _issues.AsReadOnly();
    
    private bool _loading = false;
    public bool Loading => _loading;

    public event Action? OnChange;

    private readonly HttpClient _httpClient;

    public IssueStateService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task FetchIssuesAsync(string workspaceId)
    {
        _loading = true;
        NotifyStateChanged();

        var response = await _httpClient.GetFromJsonAsync<List<Issue>>(
            $"/api/workspaces/{workspaceId}/issues"
        );
        _issues = response ?? new List<Issue>();
        
        _loading = false;
        NotifyStateChanged();
    }

    public IEnumerable<Issue> GetOpenIssues()
    {
        return _issues.Where(i => i.State == "open");
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}

// Register in Program.cs:
builder.Services.AddScoped<IssueStateService>();

// Component usage:
@inject IssueStateService IssueState
@implements IDisposable

@code {
    protected override void OnInitialized()
    {
        IssueState.OnChange += StateHasChanged;
    }

    public void Dispose()
    {
        IssueState.OnChange -= StateHasChanged;
    }
}
```

### 5. Routing

**React Router:**
```typescript
<Routes>
  <Route path="/workspace/:workspaceId" element={<Workspace />} />
  <Route path="/workspace/:workspaceId/projects/:projectId" element={<Project />} />
</Routes>
```

**Blazor Router:**
```razor
@* App.razor *@
<Router AppAssembly="@typeof(App).Assembly">
    <Found Context="routeData">
        <RouteView RouteData="@routeData" />
    </Found>
    <NotFound>
        <h1>Page not found</h1>
    </NotFound>
</Router>

@* Workspace.razor *@
@page "/workspace/{WorkspaceId}"

@code {
    [Parameter]
    public string WorkspaceId { get; set; } = string.Empty;
}

@* Project.razor *@
@page "/workspace/{WorkspaceId}/projects/{ProjectId}"

@code {
    [Parameter]
    public string WorkspaceId { get; set; } = string.Empty;
    
    [Parameter]
    public string ProjectId { get; set; } = string.Empty;
}
```

### 6. API Calls

**Axios (TypeScript):**
```typescript
class IssueService {
  async getAll(workspaceId: string): Promise<Issue[]> {
    const response = await axios.get(`/api/workspaces/${workspaceId}/issues`);
    return response.data;
  }

  async create(workspaceId: string, data: CreateIssueData): Promise<Issue> {
    const response = await axios.post(
      `/api/workspaces/${workspaceId}/issues`,
      data
    );
    return response.data;
  }
}
```

**HttpClient (C#):**
```csharp
public class IssueService
{
    private readonly HttpClient _httpClient;

    public IssueService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Issue>?> GetAllAsync(string workspaceId)
    {
        return await _httpClient.GetFromJsonAsync<List<Issue>>(
            $"/api/workspaces/{workspaceId}/issues"
        );
    }

    public async Task<Issue?> CreateAsync(string workspaceId, CreateIssueData data)
    {
        var response = await _httpClient.PostAsJsonAsync(
            $"/api/workspaces/{workspaceId}/issues",
            data
        );
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Issue>();
    }
}
```

### 7. Form Handling

**React Hook Form:**
```typescript
const { register, handleSubmit, errors } = useForm<FormData>();

const onSubmit = async (data: FormData) => {
  await createIssue(data);
};

<form onSubmit={handleSubmit(onSubmit)}>
  <input {...register("title", { required: true })} />
  {errors.title && <span>Required</span>}
  <button type="submit">Submit</button>
</form>
```

**Blazor EditForm:**
```razor
<EditForm Model="@formData" OnValidSubmit="@HandleSubmit">
    <DataAnnotationsValidator />
    <ValidationSummary />

    <InputText @bind-Value="formData.Title" />
    <ValidationMessage For="@(() => formData.Title)" />

    <button type="submit">Submit</button>
</EditForm>

@code {
    private FormData formData = new();

    private async Task HandleSubmit()
    {
        await CreateIssue(formData);
    }
}

// FormData.cs
public class FormData
{
    [Required]
    public string Title { get; set; } = string.Empty;
}
```

### 8. Styling with Tailwind CSS

Both React and Blazor can use Tailwind CSS with similar syntax:

**React:**
```tsx
<div className="flex items-center justify-between px-4 py-2 bg-white">
```

**Blazor:**
```razor
<div class="flex items-center justify-between px-4 py-2 bg-white">
```

### 9. Package Dependencies

| npm/pnpm Package | NuGet Package |
|------------------|---------------|
| `axios` | `HttpClient` (built-in) |
| `react-router-dom` | Blazor Router (built-in) |
| `react-hook-form` | EditForm (built-in) |
| `date-fns` | Built-in DateTime or `NodaTime` |
| `lodash` | `MoreLINQ` or built-in LINQ |
| `uuid` | `Guid` (built-in) |
| `clsx` | Custom helper or CSS class builder |

### 10. Testing

**Jest (TypeScript):**
```typescript
describe("IssueService", () => {
  it("should fetch issues", async () => {
    const issues = await issueService.getAll("workspace-1");
    expect(issues).toHaveLength(5);
  });
});
```

**xUnit (C#):**
```csharp
public class IssueServiceTests
{
    [Fact]
    public async Task GetAllAsync_ShouldReturnIssues()
    {
        // Arrange
        var service = CreateService();

        // Act
        var issues = await service.GetAllAsync("workspace-1");

        // Assert
        Assert.Equal(5, issues?.Count);
    }
}
```

## Migration Strategy

### Phase 1: Infrastructure Setup
1. ✅ Create .NET solution structure
2. ✅ Set up project templates (Web, Types, Services)
3. Add to solution file

### Phase 2: Type Conversion
1. Convert all TypeScript interfaces/types to C# classes
2. Convert enums and type unions
3. Set up proper namespaces

### Phase 3: Service Layer
1. Convert API service classes
2. Set up HttpClient configuration
3. Implement authentication/authorization

### Phase 4: Component Conversion
1. Convert UI components to Blazor
2. Convert page components
3. Set up routing

### Phase 5: State Management
1. Convert MobX stores to Blazor services
2. Implement state notification patterns
3. Set up dependency injection

### Phase 6: Testing & Deployment
1. Write unit tests
2. Write integration tests
3. Set up CI/CD pipelines
4. Deploy to production

## Conversion Tools & Automation

### Recommended Approach
1. **Manual conversion** for core types and components (for quality)
2. **TypeScript to C# converters** for initial pass (then manual cleanup)
3. **Code generation** for repetitive patterns

### Useful Tools
- TypeScript AST parsers for automated conversion
- Roslyn for C# code generation
- Custom scripts for batch file processing

## Common Pitfalls

1. **Null handling**: C# has nullable reference types, TypeScript has undefined/null
2. **Async patterns**: C# uses Task/async-await differently than Promises
3. **Event handling**: Blazor event binding differs from React
4. **Component lifecycle**: Different lifecycles between React and Blazor
5. **State updates**: Blazor requires explicit `StateHasChanged()` calls

## Performance Considerations

- **Blazor Server**: Lower bandwidth, higher latency, maintains server state
- **Blazor WebAssembly**: Higher initial load, runs entirely in browser
- **Blazor Hybrid**: Can be rendered on server or client

## Resources

- [Blazor Documentation](https://docs.microsoft.com/aspnet/core/blazor)
- [C# Language Reference](https://docs.microsoft.com/dotnet/csharp)
- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core)
- [.NET CLI Reference](https://docs.microsoft.com/dotnet/core/tools)

## Conclusion

This conversion is a **massive undertaking** that requires:
- Deep understanding of both TypeScript/React and C#/Blazor
- Careful planning and phased execution
- Comprehensive testing at each phase
- Team coordination and code reviews

**Estimated Timeline**: 6-12 months with a dedicated team of 4-6 developers.
