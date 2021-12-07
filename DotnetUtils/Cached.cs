using System.Linq.Expressions;

namespace ReactRio.Utils;

public sealed class AsyncLazy<T>
{
    private readonly Expression<Func<CancellationToken, Task<T>>> _valueFactory;
    private Task<T>? _factoryTask;

    public AsyncLazy(Expression<Func<CancellationToken, Task<T>>> valueFactory)
    {
        _valueFactory = valueFactory;
    }

    public Task<T> GetValueAsync(CancellationToken cancellationToken)
    {
        return _factoryTask ??= _valueFactory.Compile().Invoke(cancellationToken);
    }
}
