namespace Jalasoft.TeamUp.Resumes.ResumesException
{
    public class ErrorDAO
    {
        public string PropertyName { get; set; }

        public string ErrorMessage { get; set; }

        public object AttemptedValue { get; set; }
    }
}