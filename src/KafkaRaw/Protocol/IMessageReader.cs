using System;
using System.Buffers;

namespace KafkaRaw.Protocol
{
    internal interface IMessageReader<T>
    {
        bool TryParseMessage(in ReadOnlySequence<byte> input, ref SequencePosition consumed, ref SequencePosition examined, out T message);
    }
}