using System;
using System.Net;

namespace QuickFix.Exceptions
{
    public class CustomException:Exception
    {
        public string Title { get; set; }
        public List<string> Errors { get; set; }
        public IEnumerable<string> ErrorMessages { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        /**
         * @param message
         * @param statusCode
         * @param errors
         */
        public CustomException(string message,
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError,
        params string[] errors
        ):base(message)
        {
            ErrorMessages = errors;
            StatusCode = statusCode;
        }
        /**
         * @param message
         * @param statusCode
         * @param errors
         */
        public CustomException():base()
        {
            StatusCode = HttpStatusCode.BadRequest;
            Errors= new List<string>();
        }
        /**
         * @param message
         * @param statusCode
         * @param errors
         */
        public CustomException(string message):base(message)
        {
            Errors= new List<string>();
        }
        /**
         * @param message
         * @param statusCode
         * @param errors
         */
        public CustomException(string message, Exception exception):base(message,exception)
        {
            Errors= new List<string>();
        }
        /**
         * @param message
         * @param statusCode
         * @param errors
         */
        public CustomException(List<string> errors)
        {
            Errors= errors;
        }
        /**
         * @param message
         * @param statusCode
         * @param errors
         */
        public CustomException(HttpStatusCode statusCode, string Title)
        {
            StatusCode = statusCode;
            this.Title = Title;
            Errors= new List<string>();
        }
        /**
         * @param message
         * @param statusCode
         * @param errors
         */
        public CustomException(HttpStatusCode statusCode, string Title, string message, Exception exception):base(message,exception)
        {
            StatusCode = statusCode;
            this.Title = Title;
            Errors= new List<string>();
        }

    }
}
