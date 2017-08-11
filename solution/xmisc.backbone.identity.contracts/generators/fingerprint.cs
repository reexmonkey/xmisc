﻿namespace xmisc.backbone.identity.contracts.generators
{

    public interface IFingerprintGenerator<out TKey>
    {
        TKey GetNullFingerprint();

        TKey GetFingerprint<TModel>(TModel model);
    }
}