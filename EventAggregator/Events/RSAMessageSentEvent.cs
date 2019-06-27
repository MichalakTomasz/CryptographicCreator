using Commons;
using Prism.Events;

namespace EventAggregator
{
    public class RSAMessageSentEvent : PubSubEvent<RSAMessage> { }
}
