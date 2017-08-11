using System;

namespace reexmonkey.xmisc.backbone.identifiers.contracts.generators
{
    public interface IKeyGenerator<out TKey>
        where TKey : IEquatable<TKey>
    {
        TKey GetNullKey();

        TKey GetNext();
    }
}