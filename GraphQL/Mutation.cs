using System.Threading;
using System.Threading.Tasks;
using CommanderGQL.Data;
using CommanderGQL.GraphQL.Commands;
using CommanderGQL.GraphQL.Platforms;
using CommanderGQL.Models;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Subscriptions;

namespace CommanderGQL.GraphQL
{
    public class Mutation
    {

        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddPlatformPayload> AddPlatformAsync(AddPlatformInput input, 
        [ScopedService] AppDbContext context,
        [Service] ITopicEventSender eventSender,                    // Inject the GraphQL Subscription event Sender
        CancellationToken cancellationToken)                        // the cancellation token will cancel aasync method calls if they are hanging
        {
            var platform = new Platform{
                Name=input.Name
            };

            context.Platforms.Add(platform);
           await  context.SaveChangesAsync(cancellationToken);

            // use the event sender to send the new platform created using the OnPlatformAdded subscriptions method to all subscribers subscribing to the topic
            await eventSender.SendAsync(nameof(Subscription.OnPlatformAdded), platform, cancellationToken);

           return new AddPlatformPayload(platform);
        }



        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddCommandPayload> AddCommandAsync(AddCommandInput input, [ScopedService] AppDbContext context,
        [Service] ITopicEventSender eventSender,                    // Inject the GraphQL Subscription event Sender
        CancellationToken cancellationToken)
        {
            // map input to a command Object
            Command command = new Command{
                HowTo = input.HowTo,
                CommandLine = input.CommandLine,
                PlatformId = input.PlatformId
            };

            // add the record to database
            context.Commands.Add(command);
            await context.SaveChangesAsync(cancellationToken);

            // return the payload// it uses autotracker to get the id
            return new AddCommandPayload(command);
        }
    }
}