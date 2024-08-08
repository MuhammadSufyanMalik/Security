using FluentValidation.Results;
using Newtonsoft.Json;

namespace Security.Extensions
{
    public class ErrorDetails
    {
        public string Message { get; set; } = null!;
        public int StatusCode { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class ValidationErrorDetails : ErrorDetails
    {
        public ValidationErrorDetails(IEnumerable<ValidationFailure> errors)
        {
            Errors = errors;
        }

        public IEnumerable<ValidationFailure> Errors { get; set; }
    }
}
