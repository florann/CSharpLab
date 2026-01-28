using CodeEditor.Domain.Services.Interfaces;
using CodeEditor.Domain.Specifications.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CodeEditor.Domain.Extensions
{
    public static class FluentValidationExtension
    {
        public static IRuleBuilderOptions<T, TProperty> DbCheck<T, TProperty, TEntity>(
            this IRuleBuilder<T, TProperty> ruleBuilder,
            IEntityService<TEntity> service,
            Func<TProperty, ISpecification<TEntity>> specification,
            bool specFindSomething = true)
            where TEntity : class
        {
            return ruleBuilder.MustAsync(async (property, cancellationToken) =>
            {
                try
                {
                    var result = await service.GetAsync(specification(property));
                    return (specFindSomething) ? result != null : result == null;
                }
                catch(Exception ex)
                {
                    return false;
                }
            });
        }
    }
}
