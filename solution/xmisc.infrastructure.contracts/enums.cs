﻿namespace reexjungle.xmisc.infrastructure.contracts
{
    public enum FlushMode { soft, hard };

    public enum ExpectationMode
    {
        optimistic,
        pessimistic,
        unknown
    }

    public enum Authority
    {
        ISO = 0x1,
        NonStandard = 0x2,
        None = 0x4
    }
}