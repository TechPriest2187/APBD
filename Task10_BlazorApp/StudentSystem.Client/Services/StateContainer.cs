using StudentSystem.Shared;

namespace StudentSystem.Client.Services;
public class StateContainer
{
    public List<StudentDto> ObservedStudents { get; private set; } = new();
    public event Action? OnChange;

    public void AddObserved(StudentDto student)
    {
        if (!ObservedStudents.Any(s => s.Id == student.Id))
        {
            ObservedStudents.Add(student);
            NotifyStateChanged();
        }
    }

    public void RemoveObserved(int id)
    {
        ObservedStudents.RemoveAll(s => s.Id == id);
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}