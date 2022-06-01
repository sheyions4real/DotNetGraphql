using System.ComponentModel.DataAnnotations;
using HotChocolate;

namespace CommanderGQL.Models
{
    // this description will be taken to the descriptor iin the Commant Types
    // [GraphQLDescription("Represents command to execute a function on the specific platform")]
    public class Command
    {
        [Key]
        public int Id {get; set;}

        [Required]
        public  string HowTo {get; set;}

        [Required]
        public string CommandLine {get; set;}
        
        [Required]
        public int PlatformId {get; set;}
        public Platform Platform {get;set;}
    }
}
