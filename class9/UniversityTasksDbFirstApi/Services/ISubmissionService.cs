using System.Threading.Tasks;
using UniversityTasksDbFirstApi.DTOs;

namespace UniversityTasksDbFirstApi.Services
{
    public interface ISubmissionService
    {
        Task<(int StatusCode, string ErrorMessage, int? CreatedId)> CreateSubmissionAsync(CreateSubmissionDto dto);
        Task<(int StatusCode, string ErrorMessage)> GradeSubmissionAsync(int submissionId, GradeSubmissionDto dto);
        Task<(int StatusCode, string ErrorMessage)> DeleteSubmissionAsync(int submissionId);
    }
}