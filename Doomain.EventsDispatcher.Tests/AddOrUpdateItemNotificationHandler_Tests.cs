using System;
using Doomain.Events;
using Doomain.Shared;
using MediatR;
using NSubstitute;
using Xunit;

namespace Doomain.EventsDispatcher.Tests
{
    public class AddOrUpdateItemNotificationHandler_Tests
    {
        [Fact]
        public void Test1()
        {
            var eventBuilder = Substitute.For<IEventBuilder>();
            var mediator = Substitute.For<IMediator>();
            var coder = Substitute.For<ICoder>();
            var ev = Substitute.For<IEvent>();

            var addOrUpdateItemNotificationHandler = new AddOrUpdateItemNotificationHandler(eventBuilder, mediator, null, coder);

            var addOrUpdateNotification = new AddOrUpdateNotification(ev, Direction.Inbound);

            _ = addOrUpdateItemNotificationHandler.Handle(addOrUpdateNotification, new System.Threading.CancellationToken());

            mediator.Received(0).Publish(Arg.Any<StoreEventNotification>());
        }
    }
}
