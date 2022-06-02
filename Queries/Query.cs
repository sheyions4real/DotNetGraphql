using System.Linq;
using CommanderGQL.Data;
using CommanderGQL.Models;
using HotChocolate;
using HotChocolate.Data;

namespace CommanderGQL.Queries{
    public class Query
    {
        [UseDbContext(typeof(AppDbContext))] // to enable the mothod use multilevel query
        //[UseProjection]                         // this will allow the inner join to return platforms and commands children object
       // you dont need to use the UseProjection  attribute approach if you are using CommandType approach and the AddProjection in the Startup services configuration
       [UseFiltering]                   // to enable filtering on the get platform
       [UseSorting]                     // to enable sorting
        public IQueryable<Platform> GetPlatform([ScopedService] AppDbContext context)
        {
            return context.Platforms;
        }
   


        [UseDbContext(typeof(AppDbContext))] // to enable the mothod use multilevel query
       // [UseProjection] 
       [UseFiltering]                   // to enable filtering
       [UseSorting]                     // to enable sorting
        public IQueryable<Command> GetCommand([ScopedService] AppDbContext context)
        {
            return context.Commands;
        }
    }

}