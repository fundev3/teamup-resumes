namespace Jalasoft.TeamUp.Resumes.Utils.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Property
    {
        public string PropertyName { get; set; }

        public string ErrorMessage { get; set; }

        public string AttemptedValue { get; set; }
    }
}
