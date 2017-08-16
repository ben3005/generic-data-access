using System;

namespace GenericDataAcessLayer
{
    /// <summary>
    /// Used to signify the size of the property in 
    /// the database
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DatabaseSize : Attribute
    {
        public int Size { get; set; }
        public DatabaseSize(int size)
        {
            Size = size;
        }
    }
}
