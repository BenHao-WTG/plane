using Plane.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register application services
builder.Services.AddHttpClient<AuthService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"] ?? "http://localhost:8000");
});
builder.Services.AddHttpClient<IssueService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"] ?? "http://localhost:8000");
});

// Add controllers for API endpoints
builder.Services.AddControllers();

// Add OpenAPI/Swagger support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
else
{
    // Enable Swagger in development
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Map API controllers
app.MapControllers();

// Map MVC routes matching the React Router structure
app.MapControllerRoute(
    name: "workspace-default",
    pattern: "{workspaceSlug}",
    defaults: new { controller = "Workspace", action = "Index" });

app.MapControllerRoute(
    name: "workspace-routes",
    pattern: "{workspaceSlug}/{action}",
    defaults: new { controller = "Workspace" });

app.MapControllerRoute(
    name: "project-issues",
    pattern: "{workspaceSlug}/projects/{projectId}/issues/{issueId?}",
    defaults: new { controller = "Project", action = "Issues" });

app.MapControllerRoute(
    name: "project-cycles",
    pattern: "{workspaceSlug}/projects/{projectId}/cycles/{cycleId?}",
    defaults: new { controller = "Project", action = "Cycles" });

app.MapControllerRoute(
    name: "project-modules",
    pattern: "{workspaceSlug}/projects/{projectId}/modules/{moduleId?}",
    defaults: new { controller = "Project", action = "Modules" });

app.MapControllerRoute(
    name: "profile",
    pattern: "{workspaceSlug}/profile/{userId}/{profileViewId?}",
    defaults: new { controller = "Profile", action = "Index" });

app.Run();
