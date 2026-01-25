using CodeEditor.Domain.Services.Interfaces;
using CodeEditor.Domain.Specifications.Interfaces;
using FluentValidation;

namespace CodeEditor.Domain.Extensions
{
    public static class FluentValidationExtension
    {
        extension<T, TProperty>(IRuleBuilderInitial<T, TProperty> ruleBuilder)
        {
            public IRuleBuilderOptions<T, TProperty> DbCheck<TEntity>(
                Func<TProperty, ISpecification<TEntity>> specification,
                IBaseEntityService<TEntity> service) where TEntity : class
            {
                return ruleBuilder
                    .MustAsync(async (property, cancellationToken) =>
                    {
                        var result = await service.GetAsync(specification(property));
                        return result != null;
                    });
            }
        }
    }
}
