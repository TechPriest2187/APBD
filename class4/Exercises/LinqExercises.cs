using LinqConsoleLab.EN.Data;

namespace LinqConsoleLab.EN.Exercises;

public sealed class LinqExercises
{
    /// <summary>
    /// Task:
    /// Find all students who live in Warsaw.
    /// Return the index number, full name, and city.
    ///
    /// SQL:
    /// SELECT IndexNumber, FirstName, LastName, City
    /// FROM Students
    /// WHERE City = 'Warsaw';
    /// </summary>
    public IEnumerable<string> Task01_StudentsFromWarsaw()
    {
        return UniversityData.Students.Where(s => s.City == "Warsaw")
            .Select(e => $"{e.IndexNumber} {e.FirstName} {e.LastName} {e.City}").ToList();
    }

    /// <summary>
    /// Task:
    /// Build a list of all student email addresses.
    /// Use projection so that you do not return whole objects.
    ///
    /// SQL:
    /// SELECT Email
    /// FROM Students;
    /// </summary>
    public IEnumerable<string> Task02_StudentEmailAddresses()
    {
        return UniversityData.Student.Select(s => s.email).ToList();
    }

    /// <summary>
    /// Task:
    /// Sort students alphabetically by last name and then by first name.
    /// Return the index number and full name.
    ///
    /// SQL:
    /// SELECT IndexNumber, FirstName, LastName
    /// FROM Students
    /// ORDER BY LastName, FirstName;
    /// </summary>
    public IEnumerable<string> Task03_StudentsSortedAlphabetically()
    {
        return UniversityData.Students.OrderBy(s => s.LastName).ThenBy(s => s.FirstName)
            .Select(e => $"{e.IndexNumber} {e.FirstName} {e.LastName} {e.City}").ToList();
    }

    /// <summary>
    /// Task:
    /// Find the first course from the Analytics category.
    /// If such a course does not exist, return a text message.
    ///
    /// SQL:
    /// SELECT TOP 1 Title, StartDate
    /// FROM Courses
    /// WHERE Category = 'Analytics';
    /// </summary>
    public IEnumerable<string> Task04_FirstAnalyticsCourse()
    {
        return UniversityData.Courses.Where(s => s.Category == "Analytics")
            .Select(e => $"{e.Title} {e.StartDate}").FirstOrDefault() 
            ?? "No course of Analytics found";
    }

    /// <summary>
    /// Task:
    /// Check whether there is at least one inactive enrollment in the data set.
    /// Return one line with a True/False or Yes/No answer.
    ///
    /// SQL:
    /// SELECT CASE WHEN EXISTS (
    ///     SELECT 1
    ///     FROM Enrollments
    ///     WHERE IsActive = 0
    /// ) THEN 1 ELSE 0 END;
    /// </summary>
    public IEnumerable<string> Task05_IsThereAnyInactiveEnrollment()
    {
        return UniversityData.Enrollments.Any(e => e.IsActive == True);
    }

    /// <summary>
    /// Task:
    /// Check whether every lecturer has a department assigned.
    /// Use a method that validates the condition for the whole collection.
    ///
    /// SQL:
    /// SELECT CASE WHEN COUNT(*) = COUNT(Department)
    /// THEN 1 ELSE 0 END
    /// FROM Lecturers;
    /// </summary>
    public IEnumerable<string> Task06_DoAllLecturersHaveDepartment()
    {
        return UniversityData.Lecturers.All(l => l.Department != null);
    }

    /// <summary>
    /// Task:
    /// Count how many active enrollments exist in the system.
    ///
    /// SQL:
    /// SELECT COUNT(*)
    /// FROM Enrollments
    /// WHERE IsActive = 1;
    /// </summary>
    public IEnumerable<string> Task07_CountActiveEnrollments()
    {
        return UniversityData.Enrollments.Count(e => e.IsActive == True);
    }

    /// <summary>
    /// Task:
    /// Return a sorted list of distinct student cities.
    ///
    /// SQL:
    /// SELECT DISTINCT City
    /// FROM Students
    /// ORDER BY City;
    /// </summary>
    public IEnumerable<string> Task08_DistinctStudentCities()
    {
        return UniversityData.Students.Select(s => s.City).Distinct();
    }

    /// <summary>
    /// Task:
    /// Return the three newest enrollments.
    /// Show the enrollment date, student identifier, and course identifier.
    ///
    /// SQL:
    /// SELECT TOP 3 EnrollmentDate, StudentId, CourseId
    /// FROM Enrollments
    /// ORDER BY EnrollmentDate DESC;
    /// </summary>
    public IEnumerable<string> Task09_ThreeNewestEnrollments()
    {
        UniversityData.Enrollments.OrderBy(e => e.EnrollmentDate).take(3);
    }

    /// <summary>
    /// Task:
    /// Implement simple pagination for the course list.
    /// Assume a page size of 2 and return the second page of data.
    ///
    /// SQL:
    /// SELECT Title, Category
    /// FROM Courses
    /// ORDER BY Title
    /// OFFSET 2 ROWS FETCH NEXT 2 ROWS ONLY;
    /// </summary>
    public IEnumerable<string> Task10_SecondPageOfCourses()
    {
        return UniversityData.Courses.Select(c => $"{c.Title} {c.Category}").Skip(2);
    }

    /// <summary>
    /// Task:
    /// Join students with enrollments by StudentId.
    /// Return the full student name and the enrollment date.
    ///
    /// SQL:
    /// SELECT s.FirstName, s.LastName, e.EnrollmentDate
    /// FROM Students s
    /// JOIN Enrollments e ON s.Id = e.StudentId;
    /// </summary>
    public IEnumerable<string> Task11_JoinStudentsWithEnrollments()
    {
        return UniversityData.Student.Join(UniversityData.Enrollment,
        s => Student.ID,
        e => enrollment.ID,
        (s, e) => new {Student = s, Enrollment = e})
        .Select(s.FirstName, s.LastName, e.EnrollmentDate);
    }

    /// <summary>
    /// Task:
    /// Prepare all student-course pairs based on enrollments.
    /// Use an approach that flattens the data into a single result sequence.
    ///
    /// SQL:
    /// SELECT s.FirstName, s.LastName, c.Title
    /// FROM Enrollments e
    /// JOIN Students s ON s.Id = e.StudentId
    /// JOIN Courses c ON c.Id = e.CourseId;
    /// </summary>
    public IEnumerable<string> Task12_StudentCoursePairs()
    {
        return UniversityData.Enrollment.Join(UniversityData.Student,
        e => Enrollment.ID,
        s => Student.ID,
        (e, s) => new {Enrollment = e, Student = s})
        .Join(UniversityData.Course,
        c => Course.ID,
        e => Enrollment.ID,
        (c, e) => new {Course = c, Enrollment = e})
        .Select(s.FirstName, s.LastName, c.Title);
    }

    /// <summary>
    /// Task:
    /// Group enrollments by course and return the course title together with the number of enrollments.
    ///
    /// SQL:
    /// SELECT c.Title, COUNT(*)
    /// FROM Enrollments e
    /// JOIN Courses c ON c.Id = e.CourseId
    /// GROUP BY c.Title;
    /// </summary>
    public IEnumerable<string> Task13_GroupEnrollmentsByCourse()
    {
        return UniversityData.Enrollment.Join(UniversityData.Course,
        e => Enrollment.ID,
        c => Course.ID,
        (e, c) => new { Enrollment = e, Course = c})
        .GroupBy(c.Title).Select(c.Title, Count(c.Title));
    }

    /// <summary>
    /// Task:
    /// Calculate the average final grade for each course.
    /// Ignore records where the final grade is null.
    ///
    /// SQL:
    /// SELECT c.Title, AVG(e.FinalGrade)
    /// FROM Enrollments e
    /// JOIN Courses c ON c.Id = e.CourseId
    /// WHERE e.FinalGrade IS NOT NULL
    /// GROUP BY c.Title;
    /// </summary>
    public IEnumerable<string> Task14_AverageGradePerCourse()
    {
        return UniversityData.Enrollment.Join(UniversityData.Course,
        e => Enrollment.ID,
        c => Course.ID,
        (e, c) => new {Enrollment = e, Course = c}).Where(e.FinalGrade != null)
        .Select(c.Title, AVG(e.FinalGrade)).GroupBy(c.Title);
    }

    /// <summary>
    /// Task:
    /// For each lecturer, count how many courses are assigned to that lecturer.
    /// Return the full lecturer name and the course count.
    ///
    /// SQL:
    /// SELECT l.FirstName, l.LastName, COUNT(c.Id)
    /// FROM Lecturers l
    /// LEFT JOIN Courses c ON c.LecturerId = l.Id
    /// GROUP BY l.FirstName, l.LastName;
    /// </summary>
    public IEnumerable<string> Task15_LecturersAndCourseCounts()
    {
        return UniversityData.Lecturers.LeftJoin(UniversityData.Course
        l => Lecturers,
        c => Course,
        (l, c) => new {Lecturers = l, Course = c})
        .Select(l.FirstName, l.LastName, Count(c.ID))
        .GroupBy(l.FirstName, l.LastName);
    }

    /// <summary>
    /// Task:
    /// For each student, find the highest final grade.
    /// Skip students who do not have any graded enrollment yet.
    ///
    /// SQL:
    /// SELECT s.FirstName, s.LastName, MAX(e.FinalGrade)
    /// FROM Students s
    /// JOIN Enrollments e ON s.Id = e.StudentId
    /// WHERE e.FinalGrade IS NOT NULL
    /// GROUP BY s.FirstName, s.LastName;
    /// </summary>
    public IEnumerable<string> Task16_HighestGradePerStudent()
    {
        return UniversityData.Student.Join(UniversityData.Enrollment,
        s => Student.ID,
        e => Enrollment,
        (s, e) => new {Student = s, Enrollment = e})
        .Where(e.FinalGrade != null)
        .Select(s.FirstName, s.LastName, MAX(e.FinalGrade))
        .GroupBy(s.FirstName, s.LastName);
    }

    /// <summary>
    /// Challenge:
    /// Find students who have more than one active enrollment.
    /// Return the full name and the number of active courses.
    ///
    /// SQL:
    /// SELECT s.FirstName, s.LastName, COUNT(*)
    /// FROM Students s
    /// JOIN Enrollments e ON s.Id = e.StudentId
    /// WHERE e.IsActive = 1
    /// GROUP BY s.FirstName, s.LastName
    /// HAVING COUNT(*) > 1;
    /// </summary>
    public IEnumerable<string> Challenge01_StudentsWithMoreThanOneActiveCourse()
    {
        return UniversityData.Student.Join(UniversityData.Enrollment,
        s => Student.ID,
        e => Enrollment.ID,
        (s, e) => new {Student = s, Enrollment = e})
        .Where(e.IsActive == True)
        .GroupBy(s.FirstName, s.LastName)
        .Select(s.FirstName, s.LastName, Count(*))
        .HAVING(Count(*) > 1);
    }

    /// <summary>
    /// Challenge:
    /// List the courses that start in April 2026 and do not have any final grades assigned yet.
    ///
    /// SQL:
    /// SELECT c.Title
    /// FROM Courses c
    /// JOIN Enrollments e ON c.Id = e.CourseId
    /// WHERE MONTH(c.StartDate) = 4 AND YEAR(c.StartDate) = 2026
    /// GROUP BY c.Title
    /// HAVING SUM(CASE WHEN e.FinalGrade IS NOT NULL THEN 1 ELSE 0 END) = 0;
    /// </summary>
    public IEnumerable<string> Challenge02_AprilCoursesWithoutFinalGrades()
    {
        return UniversityData.Course.Join(UniversityData.Enrollment,
        c => Course.ID,
        e => Enrollment.ID,
        (c, e) => new {Course = c, Enrollment = e})
        .Where(MONTH(c.StartDate) = 4 AND YEAR(c.StartDate) = 2026)
        .Select(c.Title)
        .GroupBy(c.Title)
        .HAVING(SUM())
    }

    /// <summary>
    /// Challenge:
    /// Calculate the average final grade for every lecturer across all of their courses.
    /// Ignore missing grades but still keep the lecturers in mind as the reporting dimension.
    ///
    /// SQL:
    /// SELECT l.FirstName, l.LastName, AVG(e.FinalGrade)
    /// FROM Lecturers l
    /// LEFT JOIN Courses c ON c.LecturerId = l.Id
    /// LEFT JOIN Enrollments e ON e.CourseId = c.Id
    /// WHERE e.FinalGrade IS NOT NULL
    /// GROUP BY l.FirstName, l.LastName;
    /// </summary>
    public IEnumerable<string> Challenge03_LecturersAndAverageGradeAcrossTheirCourses()
    {
        throw NotImplemented(nameof(Challenge03_LecturersAndAverageGradeAcrossTheirCourses));
    }

    /// <summary>
    /// Challenge:
    /// Show student cities and the number of active enrollments created by students from each city.
    /// Sort the result by the active enrollment count in descending order.
    ///
    /// SQL:
    /// SELECT s.City, COUNT(*)
    /// FROM Students s
    /// JOIN Enrollments e ON s.Id = e.StudentId
    /// WHERE e.IsActive = 1
    /// GROUP BY s.City
    /// ORDER BY COUNT(*) DESC;
    /// </summary>
    public IEnumerable<string> Challenge04_CitiesAndActiveEnrollmentCounts()
    {
        throw NotImplemented(nameof(Challenge04_CitiesAndActiveEnrollmentCounts));
    }

    private static NotImplementedException NotImplemented(string methodName)
    {
        return new NotImplementedException(
            $"Complete method {methodName} in Exercises/LinqExercises.cs and run the command again.");
    }
}
