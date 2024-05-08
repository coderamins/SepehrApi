using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Common
{
    public class ErrorMessageFactory
    {       
        public string PropertyName { get; set; }
        public Language Language { get; set; }
        public ErrorType ErrorType { get; set; }
        public string ErrorMesssage { get; set; }

        public string MakeError(string propertyName, ErrorType errorType, Language language=Language.Fa)
        {
            switch(language)
            {
                case Language.Fa:
                    switch(errorType)
                    {
                        case ErrorType.CreatedSuccess:
                            ErrorMesssage = string.Concat(propertyName, " با موفقیت ایجاد گردید .");
                            break;
                        case ErrorType.DeletedSuccess:
                            ErrorMesssage = string.Concat(propertyName, " با موفقیت حذف گردید .");
                            break;
                        case ErrorType.UpdatedSuccess:
                            ErrorMesssage = string.Concat(propertyName, " با موفقیت ویرایش شد .");
                            break;
                        case ErrorType.DuplicateForCreate:
                            ErrorMesssage = string.Concat(propertyName, " قبلا ایجاد شده است .");
                            break;
                        case ErrorType.NotFound:
                            ErrorMesssage = string.Concat(propertyName, " یافت نشد . ");
                            break;
                    }
                    break;
            }

            return ErrorMesssage;
        }

    }


    public enum Language
    {
        Fa,
        En,
        Ar
    }

    public enum ErrorType
    {
        CreatedSuccess,
        DeletedSuccess,
        UpdatedSuccess,
        DuplicateForCreate,
        NotFound,
    }

}
