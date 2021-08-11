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
            this.Error = new ObjectResult(this.ErrorMessage);
            this.Error.StatusCode = (int)ResumesErrors.NotFound;
        }

        public ResumesException(ResumesErrors code, Exception exception)
        {
            this.ErrorMessage = new ErrorMessage();
            this.ErrorValidations = new ErrorValidations();

            switch (code)
            {
                case ResumesErrors.BadRequest:
                    var validationException = (ValidationException)exception;
                    this.ErrorValidations.Message = "Please review the errors, inconsistent data.";

                    foreach (var error in validationException.Errors)
                    {
                        var myErrorDao = new ErrorDAO();
                        myErrorDao.PropertyName = error.PropertyName;
                        myErrorDao.ErrorMessage = error.ErrorMessage;
                        myErrorDao.AttemptedValue = error.AttemptedValue;
                        this.ErrorValidations.Errors.Add(myErrorDao);
                    }

                    this.Error = new ObjectResult(this.ErrorValidations);
                    this.Error.StatusCode = (int)ResumesErrors.BadRequest;
                    break;
                case ResumesErrors.InternalServerError:
                    this.ErrorMessage.Message = "Something went wrong, please contact the TeamUp administrator.";
                    this.Error = new ObjectResult(this.ErrorMessage);
                    this.Error.StatusCode = (int)ResumesErrors.InternalServerError;
                    break;
                default:
                    break;
            }
        }

        public ErrorMessage ErrorMessage { get; set; }

        public ErrorValidations ErrorValidations { get; set; }

        public ObjectResult Error { get; set; }
    }
}
