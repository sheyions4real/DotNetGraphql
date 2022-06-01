using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HotChocolate;

namespace CommanderGQL.Models
{
    // to add documentation to the GraphQLAPI  this is not advisible as you may want to use the same model class library
    //for the REST part of the API. its effective to segregrate documentation from the model to a View Model
    //[GraphQLDescription("Represents any software or service that has a commandline interface")]  // this documentation will be added to the PlatformType
    public class Platform
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        // documentation for the model prop
       //  [GraphQLDescription("Represents a purchase enterprise valid license for the platform")]
        public string LicenseKey { get; set; }

        public ICollection<Command> Commands {get; set;} = new List<Command>(); // to set the many to many relationship btw Platform and Commands
        
    }    
}
