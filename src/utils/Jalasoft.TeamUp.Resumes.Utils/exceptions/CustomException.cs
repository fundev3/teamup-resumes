namespace Jalasoft.TeamUp.Resumes.Utils.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CustomException
    {
        public int Code { get; set; }

        public ErrorMessage ErrorMessage { get; set; }
    }
}
