using StudentSystem.Shared;

var builder = WebApplication.CreateBuilder(args);

// Enable CORS for the Blazor app
builder.Services.AddCors(options => {
    options.AddDefaultPolicy(policy => {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();
app.UseCors();

// In-Memory Data Stores
var students = new List<StudentDto> {
    new StudentDto { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@test.com", Semester = 3, IndexNumber = "s12345" }
};
var courses = new List<CourseDto>();

// Endpoints
app.MapGet("/api/students", () => Results.Ok(students));
app.MapGet("/api/students/{id}", (int id) => {
    var student = students.FirstOrDefault(s => s.Id == id);
    return student is not null ? Results.Ok(student) : Results.NotFound();
});
app.MapPost("/api/students", (StudentDto newStudent) => {
    newStudent.Id = students.Any() ? students.Max(s => s.Id) + 1 : 1;
    students.Add(newStudent);
    return Results.Created($"/api/students/{newStudent.Id}", newStudent);
});
app.MapGet("/api/courses", () => Results.Ok(courses));

app.Run();