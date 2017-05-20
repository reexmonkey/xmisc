namespace xmisc.backbone.identity.concretes.infrastructure
{
    public struct State
    {
        public ulong Timestamp { get; }

        public ushort Sequence { get; }

        public byte N0 { get; }

        public byte N1 { get; }

        public byte N2 { get; }

        public byte N3 { get; }

        public byte N4 { get; }

        public byte N5 { get; }

        public State(ulong timestamp, ushort sequence, byte n0, byte n1, byte n2, byte n3, byte n4, byte n5)
        {
            Timestamp = timestamp;
            Sequence = sequence;
            N0 = n0;
            N1 = n1;
            N2 = n2;
            N3 = n3;
            N4 = n4;
            N5 = n5;
        }

    }
}