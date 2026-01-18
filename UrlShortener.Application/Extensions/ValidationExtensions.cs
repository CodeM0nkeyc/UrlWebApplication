namespace UrlShortener.Application.Extensions;

public static class ValidationExtensions
{
    public static IRuleBuilderOptions<TEntity, TProperty> WithCodeAndMessage<TEntity, TProperty>(
        this IRuleBuilderOptions<TEntity, TProperty> ruleBuilder,
        string code,
        string message)
    {
        return ruleBuilder.WithErrorCode(code).WithMessage(message);
    }
}