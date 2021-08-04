namespace Jalasoft.TeamUp.Resumes.Utils
{
    using System;

    public class ResumeException : Exception
    {
        public ResumeException(int code)
        {
            this.Error = new CustomException();
            this.Error.ErrorMessage = new ErrorMessage();
            this.Error.Code = code;

            switch (code)
            {
                case 404:
                    this.Error.ErrorMessage.Message = "The resource couldn't be found.";
                    break;
                case 500:
                    this.Error.ErrorMessage.Message = "Something went wrong, please contact the TeamUp administrator.";
                    break;
                default:
                    break;
            }
        }

        public CustomException Error { get; set; }
    }
}
