using System.Linq;
using CommanderGQL.Data;
using CommanderGQL.Models;
using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types;

namespace CommanderGQL.GraphQL.Platforms
{
    public class PlatformType : ObjectType<Platform>
    {
        // use the descriptor to add documentation to the Platform Model
        protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
        {
            //base.Configure(descriptor);
            descriptor.Description("Represents any software or service that has a commandline interface");

            // with will configure the licenceKey not to be returned and to be ignored by the query
            descriptor
                .Field(p => p.LicenseKey)
                .Ignore();

        // Add a description config to return the Commands for a platform 
            descriptor
                .Field(p => p.Commands)
                .ResolveWith<Resolvers>(p => p.GetCommands(default!, default!,default!))
                .UseDbContext<AppDbContext>()
                .Description("This is a list of availible commands for this platform");
        }

        // how to let the type access the data from the database 
        // we use resolvers
        private class Resolvers
        {
            public IQueryable<Command> GetCommands([Parent] Platform platform, [ScopedService] AppDbContext context, IResolverContext ctx)
            {
                return context.Commands.Where(c => c.PlatformId == platform.Id);
            }
        }


    }
}