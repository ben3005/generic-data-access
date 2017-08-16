using System;
using System.Collections.Generic;
using System.Text;

namespace GenericDataAcessLayer
{
    [DatabaseTableName("Person")]
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
