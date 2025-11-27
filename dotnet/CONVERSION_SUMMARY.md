# TypeScript to .NET Core Conversion - Implementation Summary

## Branch Information
- **Branch Name**: `netcore`
- **Conversion Date**: November 9, 2025 (Updated: November 10, 2025)
- **Status**: ASP.NET Core MVC application mirroring actual TypeScript app structure

## Conversion Approach

**CORRECTED APPROACH**: This conversion now properly mirrors the actual React Router application from `apps/web` (preview branch) into an ASP.NET Core MVC application, not a generic Blazor learning template.

### Source Application Structure
The source TypeScript application (`apps/web` from preview branch) is a React Router application with:
- Dynamic routing with parameters: `[workspaceSlug]`, `[projectId]`, `[issueId]`, etc.
- Multiple nested route groups
- Client-side state management with MobX
- API services with axios

### Target .NET Structure
The .NET implementation creates an ASP.NET Core MVC application that:
- Maps React Router routes to MVC controller actions
- Converts TypeScript API services to Web API controllers
- Uses the same type definitions (converted to C#)
- Maintains the same route structure and parameters

## What Has Been Converted

### 1. Solution Structure ✅
A complete .NET 9.0 solution mirroring the actual application structure:

```
Plane.sln                              # Main solution file
├── dotnet/
│   ├── Plane.Web/                     # ASP.NET Core MVC Application
│   │   ├── Controllers/
│   │   │   ├── WorkspaceController.cs     # Workspace routes
│   │   │   ├── ProjectController.cs       # Project/Issues/Cycles/Modules
│   │   │   ├── ProfileController.cs       # User profile routes
│   │   │   └── Api/
│   │   │       └── IssuesController.cs    # REST API endpoints
│   │   ├── Program.cs                     # App configuration with routing
│   │   └── Views/                         # MVC views (to be added)
│   ├── Plane.Types/                   # Type definitions library
│   │   ├── Auth.cs                    # Authentication types
│   │   ├── User.cs                    # User types
│   │   ├── DescriptionVersion.cs      # Description version types
│   │   └── Issues/
│   │       ├── Base.cs                # Base issue types and enums
│   │       ├── Issue.cs               # Issue models
│   │       └── IssueLabel.cs          # Issue label model
│   └── Plane.Services/                # Service layer
│       ├── AuthService.cs             # Authentication service
│       └── IssueService.cs            # Issue service
```

### 2. Route Mappings ✅

**From React Router (apps/web) → To ASP.NET Core MVC:**

| React Router Path | MVC Controller | Action | Source File |
|-------------------|----------------|--------|-------------|
| `/{workspaceSlug}` | WorkspaceController | Index | `app/(all)/[workspaceSlug]/(projects)/page.tsx` |
| `/{workspaceSlug}/active-cycles` | WorkspaceController | ActiveCycles | `app/(all)/[workspaceSlug]/(projects)/active-cycles/page.tsx` |
| `/{workspaceSlug}/analytics/{tabId}` | WorkspaceController | Analytics | `app/(all)/[workspaceSlug]/(projects)/analytics/[tabId]/page.tsx` |
| `/{workspaceSlug}/browse/{workItem}` | WorkspaceController | Browse | `app/(all)/[workspaceSlug]/(projects)/browse/[workItem]/page.tsx` |
| `/{workspaceSlug}/drafts` | WorkspaceController | Drafts | `app/(all)/[workspaceSlug]/(projects)/drafts/page.tsx` |
| `/{workspaceSlug}/notifications` | WorkspaceController | Notifications | `app/(all)/[workspaceSlug]/(projects)/notifications/page.tsx` |
| `/{workspaceSlug}/projects/{projectId}/issues` | ProjectController | Issues | `app/(all)/[workspaceSlug]/(projects)/projects/(detail)/[projectId]/issues/(list)/page.tsx` |
| `/{workspaceSlug}/projects/{projectId}/issues/{issueId}` | ProjectController | IssueDetail | `app/(all)/[workspaceSlug]/(projects)/projects/(detail)/[projectId]/issues/(detail)/[issueId]/page.tsx` |
| `/{workspaceSlug}/projects/{projectId}/cycles` | ProjectController | Cycles | `app/(all)/[workspaceSlug]/(projects)/projects/(detail)/[projectId]/cycles/(list)/page.tsx` |
| `/{workspaceSlug}/projects/{projectId}/cycles/{cycleId}` | ProjectController | CycleDetail | `app/(all)/[workspaceSlug]/(projects)/projects/(detail)/[projectId]/cycles/(detail)/[cycleId]/page.tsx` |
| `/{workspaceSlug}/projects/{projectId}/modules` | ProjectController | Modules | `app/(all)/[workspaceSlug]/(projects)/projects/(detail)/[projectId]/modules/(list)/page.tsx` |
| `/{workspaceSlug}/projects/{projectId}/modules/{moduleId}` | ProjectController | ModuleDetail | `app/(all)/[workspaceSlug]/(projects)/projects/(detail)/[projectId]/modules/(detail)/[moduleId]/page.tsx` |
| `/{workspaceSlug}/projects/{projectId}/intake` | ProjectController | Intake | `app/(all)/[workspaceSlug]/(projects)/projects/(detail)/[projectId]/intake/page.tsx` |
| `/{workspaceSlug}/projects/{projectId}/archives/issues` | ProjectController | ArchivedIssues | `app/(all)/[workspaceSlug]/(projects)/projects/(detail)/[projectId]/archives/issues/(list)/page.tsx` |
| `/{workspaceSlug}/projects/{projectId}/archives/cycles` | ProjectController | ArchivedCycles | `app/(all)/[workspaceSlug]/(projects)/projects/(detail)/[projectId]/archives/cycles/page.tsx` |
| `/{workspaceSlug}/projects/{projectId}/archives/modules` | ProjectController | ArchivedModules | `app/(all)/[workspaceSlug]/(projects)/projects/(detail)/[projectId]/archives/modules/page.tsx` |
| `/{workspaceSlug}/profile/{userId}` | ProfileController | Index | `app/(all)/[workspaceSlug]/(projects)/profile/[userId]/page.tsx` |
| `/{workspaceSlug}/profile/{userId}/activity` | ProfileController | Activity | `app/(all)/[workspaceSlug]/(projects)/profile/[userId]/activity/page.tsx` |
| `/{workspaceSlug}/profile/{userId}/{profileViewId}` | ProfileController | ProfileView | `app/(all)/[workspaceSlug]/(projects)/profile/[userId]/[profileViewId]/page.tsx` |

### 3. API Endpoints ✅

**REST API Controller (IssuesController):**  
**Source**: `packages/services/src/issue/sites-issue.service.ts`

| HTTP Method | Endpoint | Action | Description |
|-------------|----------|--------|-------------|
| GET | `/api/issues/{workspaceSlug}/{projectId}` | GetIssues | List issues with pagination |
| GET | `/api/issues/{workspaceSlug}/{projectId}/{issueId}` | GetIssue | Get issue details |
| GET | `/api/issues/{workspaceSlug}/{projectId}/{issueId}/votes` | GetVotes | Get issue votes |
| POST | `/api/issues/{workspaceSlug}/{projectId}/{issueId}/votes` | AddVote | Add vote to issue |
| DELETE | `/api/issues/{workspaceSlug}/{projectId}/{issueId}/votes/{voteId}` | RemoveVote | Remove vote |
| GET | `/api/issues/{workspaceSlug}/{projectId}/{issueId}/comments` | GetComments | Get issue comments |
| POST | `/api/issues/{workspaceSlug}/{projectId}/{issueId}/comments` | AddComment | Add comment |

### 2. Type Conversions ✅

**Authentication Types**  
**Source**: `packages/types/src/auth.ts`  
**Target**: `dotnet/Plane.Types/Plane.Types/Auth.cs`

Converted types:
- `TEmailCheckTypes` → `EmailCheckTypes` (enum)
- `IEmailCheckData` → `EmailCheckData` (class)
- `IEmailCheckResponse` → `EmailCheckResponse` (class)
- `ILoginTokenResponse` → `LoginTokenResponse` (class)
- `IMagicSignInData` → `MagicSignInData` (class)
- `IPasswordSignInData` → `PasswordSignInData` (class)
- `ICsrfTokenData` → `CsrfTokenData` (class)

**User Types**  
**Source**: `packages/types/src/users.ts`  
**Target**: `dotnet/Plane.Types/Plane.Types/User.cs`

Converted types:
- `EStartOfTheWeek` → `StartOfTheWeek` (enum)
- `TLoginMediums` → `LoginMediums` (enum)
- `IUserTheme` → `UserTheme` (class)
- `IUserLite` → `UserLite` (class)
- `IUser` → `User` (class)
- `IUserAccount` → `UserAccount` (class)
- `TUserProfile` → `UserProfile` (class)
- `IInstanceAdminStatus` → `InstanceAdminStatus` (class)
- `IUserSettings` → `UserSettings` (class)
- `IUserActivity` → `UserActivity` (class)
- `UserAuth` → `UserAuth` (class)

**Issue Types**  
**Source**: `packages/types/src/issues/`  
**Target**: `dotnet/Plane.Types/Plane.Types/Issues/`

Converted types:
- `TIssuePriorities` → `IssuePriorities` (enum)
- `EIssueLayoutTypes` → `IssueLayoutTypes` (enum)
- `EIssueServiceType` → `IssueServiceType` (enum)
- `EIssuesStoreType` → `IssuesStoreType` (enum)
- `TLoader` → `LoaderType` (enum)
- `TIssueRelationTypes` → `IssueRelationTypes` (enum)
- `TGroupedIssues` → `GroupedIssues` (class)
- `TSubGroupedIssues` → `SubGroupedIssues` (class)
- `TPaginationData` → `PaginationData` (class)
- `TBaseIssue` → `BaseIssue` (class)
- `IssueRelation` → `IssueRelation` (class)
- `TIssueAttachment` → `IssueAttachment` (class)
- `TIssueLink` → `IssueLink` (class)
- `TIssueReaction` → `IssueReaction` (class)
- `TIssue` → `Issue` (class)
- `TIssueMap` → `IssueMap` (class)
- `IIssueLabel` → `IssueLabel` (class)

**Description Version Types**  
**Source**: `packages/types/src/description_version.ts`  
**Target**: `dotnet/Plane.Types/Plane.Types/DescriptionVersion.cs`

Converted types:
- `TDescriptionVersion` → `DescriptionVersion` (class)
- `TDescriptionVersionDetails` → `DescriptionVersionDetails` (class)
- `TDescriptionVersionsListResponse` → `DescriptionVersionsListResponse` (class)

### 3. Component Conversions ✅

**Issue Confirm Dialog Component**  
**Source**: `apps/web/core/components/issues/confirm-issue-discard.tsx`  
**Target**: `dotnet/Plane.Web/Plane.Web/Components/Issues/ConfirmIssueDiscard.razor`

Conversion highlights:
- React functional component → Blazor Razor component
- `useState` hooks → Component fields with `StateHasChanged()`
- Props → `[Parameter]` attributes
- Event callbacks → `EventCallback` parameters
- JSX → Razor syntax

**Issue Labels List Component**  
**Source**: `apps/web/core/components/ui/labels-list.tsx`  
**Target**: `dotnet/Plane.Web/Plane.Web/Components/UI/IssueLabelsList.razor`

Conversion highlights:
- React functional component → Blazor component
- Props destructuring → `[Parameter]` properties
- Array operations → LINQ methods
- Tooltip component → title attribute
- Tailwind CSS classes maintained

### 4. Service Layer ✅

**Authentication Service**  
**Target**: `dotnet/Plane.Services/Plane.Services/AuthService.cs`

Converted authentication service methods:
- `CheckEmailAsync()` - Email authentication check
- `SignInWithMagicCodeAsync()` - Magic code sign-in
- `SignInWithPasswordAsync()` - Password sign-in
- `GetCsrfTokenAsync()` - CSRF token retrieval
- `SignOutAsync()` - User sign-out

**Issue Service**  
**Source**: `packages/services/src/issue/sites-issue.service.ts`  
**Target**: `dotnet/Plane.Services/Plane.Services/IssueService.cs`

Converted issue service methods:
- `ListAsync()` - Retrieve paginated list of issues
- `RetrieveAsync()` - Get issue details
- `ListVotesAsync()` - Get issue votes
- `AddVoteAsync()` - Create new vote
- `RemoveVoteAsync()` - Delete vote
- `ListCommentsAsync()` - Get issue comments
- `AddCommentAsync()` - Create new comment

Supporting types:
- `PublicIssuesResponse` - Paginated response model
- `IssueVote` - Vote model
- `IssueComment` - Comment model

### 5. Documentation ✅

Created comprehensive documentation:
- `dotnet/README.md` - Project overview and getting started guide
- `dotnet/CONVERSION_GUIDE.md` - Detailed conversion patterns and examples (14KB)

### 6. Build Configuration ✅

- Solution file configured with all projects
- Project references properly set up
- Build verified successful
- .gitignore configured for .NET artifacts

## Build Status

```bash
$ dotnet build Plane.sln
Build succeeded in 3.3s ✅
```

All projects compile without errors:
- ✅ Plane.Types
- ✅ Plane.Services  
- ✅ Plane.Web

## Original Project Statistics

- **Total TypeScript Files**: 3,541+
- **Projects to Convert**:
  - `apps/admin` - Admin panel application
  - `apps/web` - Main web application (largest)
  - `apps/space` - Space features
  - `apps/live` - Live features
  - `apps/proxy` - Proxy service
- **Shared Packages**: 14 TypeScript packages
  - @plane/constants
  - @plane/decorators
  - @plane/editor
  - @plane/eslint-config
  - @plane/hooks
  - @plane/i18n
  - @plane/logger
  - @plane/propel
  - @plane/services
  - @plane/shared-state
  - @plane/tailwind-config
  - @plane/types
  - @plane/ui
  - @plane/utils

## Conversion Coverage

- **Types**: ~2% converted (8 type files out of ~200)
  - Auth types (1 file)
  - User types (1 file) 
  - Issue types (3 files: Base, Issue, IssueLabel)
  - Description version types (1 file)
  - Additional models in services (2 files)
- **Components**: ~0.1% converted (2 components out of ~2,000)
  - ConfirmIssueDiscard (Issues)
  - IssueLabelsList (UI)
- **Services**: ~2% converted (2 service files)
  - AuthService
  - IssueService
- **Overall**: ~1-2% of total codebase
- **Redundant template files removed**: Counter.razor, Weather.razor

## What Remains

This is a **partial demonstration** of the conversion approach. A complete conversion would require:

### Remaining Type Conversions
- ~192 more TypeScript type/interface files
- Workspace, project, module types
- State, workflow, and cycle types
- Additional enums and type unions
- Generic types and utility types
- Complex nested types

### Remaining Component Conversions
- ~1,998+ React components
- Page components with routing
- Layout components (partially cleaned up)
- Complex form components
- Data visualization components
- Integration components
- Custom hooks conversion to services
- Context providers to dependency injection

### Remaining Service Conversions
- All API service classes
- State management (MobX stores)
- Utility functions
- Custom hooks

### Infrastructure Setup Needed
- Authentication/authorization setup
- API integration with backend
- Routing configuration
- State management patterns
- Error handling
- Logging
- Testing infrastructure
- CI/CD pipelines
- Docker configuration
- Deployment setup

## Conversion Patterns Demonstrated

The implemented conversions demonstrate key patterns:

1. **TypeScript interfaces → C# classes** with proper null handling
2. **React functional components → Blazor Razor components** with @code blocks
3. **React hooks → Blazor component state** and lifecycle methods
4. **API calls with axios → HttpClient** in C# services
5. **Props → Parameters** with EventCallback for events
6. **Tailwind CSS** maintained in Blazor components

## Technology Stack

### Original
- Frontend: Next.js 14, React 18, TypeScript
- State: MobX
- Styling: Tailwind CSS
- Build: Turbo (monorepo)
- Package Manager: pnpm

### New (.NET)
- Frontend: Blazor Web App (.NET 9)
- State: Blazor services + dependency injection
- Styling: Tailwind CSS (maintained)
- Build: MSBuild
- Package Manager: NuGet

## How to Use This Conversion

### Prerequisites
- .NET 9.0 SDK or later
- Visual Studio 2022, VS Code, or Rider

### Build and Run
```bash
# Build the entire solution
dotnet build Plane.sln

# Run the web application
cd dotnet/Plane.Web/Plane.Web
dotnet run

# Run tests (when added)
dotnet test
```

### Project References
The solution uses proper project references:
- Plane.Services references Plane.Types
- Plane.Web can reference both Services and Types

## Next Steps for Full Conversion

To complete the conversion, follow the phases outlined in `CONVERSION_GUIDE.md`:

1. **Phase 1**: Infrastructure (✅ Complete)
2. **Phase 2**: Type conversion (0.5% complete)
3. **Phase 3**: Service layer (1% complete)
4. **Phase 4**: Component conversion (<0.1% complete)
5. **Phase 5**: State management (Not started)
6. **Phase 6**: Testing & deployment (Not started)

## Estimated Effort

Based on the project size:
- **3,541 TypeScript files** to convert
- **Estimated timeline**: 6-12 months
- **Recommended team**: 4-6 developers
- **Man-hours**: ~5,000-10,000 hours

## Benefits of .NET Core Conversion

1. **Type Safety**: Strong typing with C# compiler
2. **Performance**: Potentially better server-side rendering performance
3. **Integration**: Better integration with .NET backend services
4. **Tooling**: Excellent Visual Studio tooling and debugging
5. **Enterprise Support**: Microsoft's long-term support and updates

## Conclusion

This conversion provides a **working foundation** and **proof of concept** for converting the Plane TypeScript application to .NET Core. The structure is in place, the build succeeds, and the conversion patterns are documented.

The remaining ~99% of the conversion would follow the same patterns demonstrated here, scaled across all files and components in the project.

## Files Modified/Created

### New Files (in dotnet/ directory)
- `Plane.sln` - Main solution file
- `README.md` - Project documentation
- `CONVERSION_GUIDE.md` - Detailed conversion guide
- `.gitignore` - .NET specific ignore rules
- `Plane.Types/Plane.Types/Auth.cs` - Type definitions
- `Plane.Types/Plane.Types/Plane.Types.csproj` - Project file
- `Plane.Services/Plane.Services/AuthService.cs` - Auth service
- `Plane.Services/Plane.Services/Plane.Services.csproj` - Project file
- `Plane.Web/Plane.Web/Components/Issues/ConfirmIssueDiscard.razor` - Blazor component
- `Plane.Web/Plane.Web/Plane.Web.csproj` - Project file
- Plus generated files (Program.cs, default Blazor components, etc.)

### Original Files
- No original TypeScript files were modified or deleted
- All original code remains intact
- The .NET implementation is additive, not replacing

## Build Artifacts Location

Build outputs are in standard .NET locations:
- `dotnet/*/bin/Debug/net9.0/` - Debug builds
- `dotnet/*/obj/` - Intermediate build files

All build artifacts are excluded via `.gitignore`.
