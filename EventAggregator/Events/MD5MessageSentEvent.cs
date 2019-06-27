using Commons;
using Prism.Events;

namespace EventAggregator
{
    public class MD5MessageSentEvent : PubSubEvent<MD5Message> { }
}
