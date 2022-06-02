namespace CommanderGQL.GraphQL.Commands
{
    // a record is like a property with {get; set;} but 
    // its more like an object type Object<string, string, string>
    public record AddCommandInput(string HowTo, string CommandLine, int PlatformId);
}