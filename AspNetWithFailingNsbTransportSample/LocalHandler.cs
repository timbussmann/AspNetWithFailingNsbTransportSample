namespace AspNetWithFailingNsbTransportSample
{
    using System;
    using System.Threading.Tasks;
    using NServiceBus;

    public class DemoMessage : IMessage
    {
    }

    public class LocalHandler : IHandleMessages<DemoMessage>
    {
        public Task Handle(DemoMessage message, IMessageHandlerContext context)
        {
            Console.WriteLine("Message received");
            return Task.CompletedTask;
        }
    }
}