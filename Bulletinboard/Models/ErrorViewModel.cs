using Bulletinboard.Helpers;
using System.Net;

namespace Bulletinboard.Models;

public partial class ErrorViewModel
{
    public string? RequestId { get; init; }
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    public HttpRequestException? Exception { get; init; }
    public string? ExceptionMessage => Exception?.Message;
    public int? IntStatusCode => (int?)Exception?.StatusCode;
    public string? StrStatusCode => Exception?.StatusCode?.ToString()?.SplitCamelCaseIntoStr() ?? string.Empty;
    public string? StrStatus => $"{IntStatusCode} {StrStatusCode}";

    public ErrorViewModel(HttpRequestException? Exception = null, string? RequestId = null)
    {
        this.Exception = Exception;
        this.RequestId = RequestId;
    }
    public ErrorViewModel(string ExceptionText, HttpStatusCode StatusCode, string? RequestId = null)
        : this(new HttpRequestException(ExceptionText, null, StatusCode), RequestId) { }
    
    // Common ViewModels
    public static readonly ErrorViewModel BadRequest = new("Something went wrong.", HttpStatusCode.BadRequest);
    public static readonly ErrorViewModel Unauthorized = new("You should authorize before accessing this page.", HttpStatusCode.Unauthorized);
    public static readonly ErrorViewModel Forbidden = new("You don't have rights to access this page.", HttpStatusCode.Forbidden);
    public static readonly ErrorViewModel NotFound = new("The page requested is not found.", HttpStatusCode.NotFound);
    public static readonly ErrorViewModel ServerError = new("Something went wrong (that's server's fault).", HttpStatusCode.InternalServerError);
    public static readonly ErrorViewModel BadGateway = new("The server, while working as a gateway to get a response needed to handle the request, got an invalid response.", HttpStatusCode.BadGateway);
    public static readonly ErrorViewModel Unavailable = new("The server is under maintenance or overloaded. Try again in a few minutes", HttpStatusCode.ServiceUnavailable);

    public static ErrorViewModel GetByStatusCode(HttpStatusCode? statusCode)
        => statusCode switch
        {
            HttpStatusCode.Unauthorized => Unauthorized,
            HttpStatusCode.Forbidden => Forbidden,
            HttpStatusCode.NotFound => NotFound,
            HttpStatusCode.InternalServerError => ServerError,
            HttpStatusCode.BadGateway => BadGateway,
            HttpStatusCode.ServiceUnavailable => Unavailable,
            _ => BadRequest
        };
}