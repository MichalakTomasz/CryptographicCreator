using Commons;
using Prism.Events;

namespace EventAggregator
{
    public class AESMessageSentEvent :PubSubEvent<AESMessage> { }
}
