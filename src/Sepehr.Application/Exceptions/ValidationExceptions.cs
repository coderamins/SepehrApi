using FluentValidation.Results;

namespace Sepehr.Application.Exceptions
{
    public class ValidationExceptions : Exception
    {
        public ValidationExceptions() : base("خطایی در اجرای درخواست پیش آمده است.")
        {
            Errors = new List<string>();
        }

        public List<string> Errors { get; }

        public ValidationExceptions(IEnumerable<ValidationFailure> failures)
            : this()
        {
            foreach (var failure in failures)
            {
                Errors.Add(failure.ErrorMessage);
            }
        }
    }
}