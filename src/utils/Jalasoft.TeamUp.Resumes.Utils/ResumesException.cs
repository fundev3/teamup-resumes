namespace Jalasoft.TeamUp.Resumes.ResumesException
{
    using System;
    using FluentValidation;
    using Microsoft.AspNetCore.Mvc;

    public class ResumesException : Exception
    {
        public ResumesException(ResumesErrors code)
        {
            this.ErrorMessage = new ErrorMessage();
            this.ErrorMessage.Message = "The resource couldn't be found";
            this.StatusCode = (int)ResumesErrors.NotFound;
            this.Error = new ObjectResult(this.ErrorMessage);
            this.Error.StatusCode = this.StatusCode;
        }

        public ResumesException(ResumesErrors code, Exception exception)
        {
            this.ErrorMessage = new ErrorMessage();

            switch (code)
            {
                case ResumesErrors.BadRequest:
                    var validationException = (ValidationException)exception;
                    this.ErrorMessage.Message = "Please review the errors, inconsistent data.";
                    this.ErrorMessage.Errors = validationException.Errors;
                    this.StatusCode = (int)ResumesErrors.BadRequest;
                    this.Error = new ObjectResult(this.ErrorMessage);
                    this.Error.StatusCode = this.StatusCode;
                    break;
                case ResumesErrors.InternalServerError:
                    this.ErrorMessage.Message = "Something went wrong, please contact the TeamUp administrator.";
                    this.StatusCode = (int)ResumesErrors.InternalServerError;
                    this.Error = new ObjectResult(this.ErrorMessage);
                    this.Error.StatusCode = this.StatusCode;
                    break;
                case ResumesErrors.NotFound:
                    this.ErrorMessage.Message = "The resource couldn't be found";
                    this.StatusCode = (int)ResumesErrors.NotFound;
                    this.Error = new ObjectResult(this.ErrorMessage);
                    this.Error.StatusCode = this.StatusCode;
                    break;
                default:
                    break;
            }
        }

        public ErrorMessage ErrorMessage { get; set; }

        public int StatusCode { get; set; }

        public ObjectResult Error { get; set; }
    }
}
