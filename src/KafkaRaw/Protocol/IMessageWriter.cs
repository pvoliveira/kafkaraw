using System.Buffers;

namespace KafkaRaw.Protocol
{
    internal interface IMessageWriter<T>
    {
        void WriteMessage(T message, IBufferWriter<byte> output);
    }
}