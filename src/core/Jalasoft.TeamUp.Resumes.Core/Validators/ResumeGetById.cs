namespace Jalasoft.TeamUp.Resumes.Core.Validators
{
    using System.Collections.Generic;
    using FluentValidation;

    public static class ResumeGetById
    {
        public static IRuleBuilderOptions<T, IList<TElement>> ListMustContainFewerThan<T, TElement>(this IRuleBuilder<T, IList<TElement>> ruleBuilder, int id)
        {
            return ruleBuilder.Must(list => list.Count < id).WithMessage("The list contains too many items");
        }
    }
}
