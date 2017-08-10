using System;
using xmisc.backbone.identity.contracts.generators;

namespace xmisc.backbone.identity.concretes.generators
{
    public abstract class NumericKeyGeneratorBase<TNumeric> : IKeyGenerator<TNumeric>
        where TNumeric : struct, IEquatable<TNumeric>, IComparable<TNumeric>, IComparable, IFormattable, IConvertible
    {
        public TNumeric GetNullKey() => default(TNumeric);

        public abstract TNumeric GetNext();

    }

    public abstract class ResuableNumericKeyGenerator<TNumeric> : NumericKeyGeneratorBase<TNumeric>
        where TNumeric : struct, IEquatable<TNumeric>, IComparable<TNumeric>, IComparable, IFormattable, IConvertible

    {
        public abstract void Reuse(TNumeric value);

        public abstract void Reset();
    }
}