using System;

namespace GenericDataAcessLayer
{
    public class DatabaseColumn : Attribute
    {
        public string Name { get; }

        public DatabaseColumn(string name)
        {
            Name = name;
        }
    }
}