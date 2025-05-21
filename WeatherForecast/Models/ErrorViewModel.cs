using System.Net;

namespace SectionAssignment.Models;

public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    public string? ErrorMessage { get; set; }

    public HttpStatusCode? StatusCode { get; set; }
}