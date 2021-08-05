namespace Jalasoft.TeamUp.Resumes.Utils.Exceptions
{
    using System;

    public class ResumeException : Exception
    {
        public ResumeException(ErrorsTypes code)
        {
            this.Error = new CustomException();
            this.Error.ErrorMessage = new ErrorMessage();
            this.Error.Code = (int)code;

            switch (code)
            {
                case ErrorsTypes.NotFoundError:
                    this.Error.ErrorMessage.Message = "The resource couldn't be found.";
                    break;
                case ErrorsTypes.ServerError:
                    this.Error.ErrorMessage.Message = "Something went wrong, please contact the TeamUp administrator.";
                    break;
                default:
                    break;
            }
        }

        public CustomException Error { get; set; }
    }
}
