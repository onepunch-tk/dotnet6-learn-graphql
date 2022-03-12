using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrephQL_Web.API.Schema
{
    //subcription(구독 웹소켓 api)
    public class Subscription
    {
        [Subscribe]
        public CourseResult CourseCreate([EventMessage] CourseResult course) => course;

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<CourseResult>> CourseUpdate(Guid courseId, [Service] ITopicEventReceiver topicEventReceiver)
        {
            string topic = $"{courseId}_{nameof(Subscription.CourseUpdate)}";
            return topicEventReceiver.SubscribeAsync<string, CourseResult>(topic);
        }
    }
}
