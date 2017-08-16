using System;
using System.Collections.Generic;
using System.Text;

namespace GenericDataAcessLayer
{
    [DatabaseTableName("Person")]
    public class Person
    {
        public int Id { get; set; }
        [DatabaseColumn("fName")]
        [DatabaseSearchSelect]
        public string FirstName { get; set; }
        [DatabaseColumn("sName")]
        [DatabaseSearchSelect]
        public string LastName { get; set; }
    }
}
