using System;

namespace GenericDataAcessLayer
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DatabaseTableName : Attribute
    {
        public string Name { get; }
        public DatabaseTableName(string name)
        {
            Name = name;
        }
    }
}
