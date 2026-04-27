namespace ClinicAppointmentsApi.Services;

public class AppointmentException : Exception
{
    public int StatusCode { get; }
    
    public AppointmentException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}