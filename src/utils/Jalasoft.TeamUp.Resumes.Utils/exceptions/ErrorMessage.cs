namespace Jalasoft.TeamUp.Resumes.Utils.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ErrorMessage
    {
        public List<Property> Errors { get; set; }

        public string Message { get; set; }
    }
}
