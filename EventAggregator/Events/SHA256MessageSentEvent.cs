using Commons;
using Prism.Events;

namespace EventAggregator
{
    public class SHA256MessageSentEvent : PubSubEvent<SHA256Message> { }
}
