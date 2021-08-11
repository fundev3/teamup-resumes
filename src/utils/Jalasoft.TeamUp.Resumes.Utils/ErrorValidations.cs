namespace Jalasoft.TeamUp.Resumes.ResumesException
{
    using System.Collections.Generic;

    public class ErrorValidations : BaseError
    {
        public ErrorValidations()
        {
            this.Errors = new List<Error>();
        }

        public List<Error> Errors { get; set; }
    }
}