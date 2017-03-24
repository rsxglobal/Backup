using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentLab.Domain
{
    [MetadataType(typeof(StudentMetaData))]//defined rules for MetaData
    public partial class Student//set rules and validates data before sending to the DB
    {
        public class StudentMetaData
        {
            [Required(ErrorMessage = "First Name is required")]
            public string FirstName { get; set; }
            [Required(ErrorMessage = "Last Name is required")]
            public string LastName { get; set; }
            [Required(ErrorMessage = "Age is required")]
            [Range(18, 65, ErrorMessage = "Age should be 18 and 65")]
            public int Age { get; set; }
           

        }

    }
}
