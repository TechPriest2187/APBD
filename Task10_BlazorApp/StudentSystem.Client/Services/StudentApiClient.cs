using System.Net.Http.Json;
using StudentSystem.Shared;

namespace StudentSystem.Client.Services;

// Check this exact line! Make sure there is no "s" at the end of Student
public class StudentApiClient(HttpClient httpClient) 
{
    public async Task<List<StudentDto>> GetStudentsAsync() => 
        await httpClient.GetFromJsonAsync<List<StudentDto>>("api/students") ?? new();

    public async Task<StudentDto?> GetStudentAsync(int id) => 
        await httpClient.GetFromJsonAsync<StudentDto>($"api/students/{id}");

    public async Task<HttpResponseMessage> CreateStudentAsync(StudentDto student) => 
        await httpClient.PostAsJsonAsync("api/students", student);
}