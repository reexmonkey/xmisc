using System;

namespace xmisc.backbone.identity.contracts.generators
{
    public interface IKeyGenerator<out TKey>
        where TKey : IEquatable<TKey>
    {
        TKey GetNullKey();

        TKey GetNext();
    }
}