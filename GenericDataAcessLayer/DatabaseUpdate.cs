using System;

namespace GenericDataAcessLayer
{
    /// <summary>
    /// Used to specify the visibility of a property
    /// when updating the database.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DatabaseUpdate : DatabaseEvent
    {
        public DatabaseUpdate(DatabaseVisibility visibilityTargets) : base(visibilityTargets)
        {
        }

        public override bool Can(bool archived, bool deleted) => !deleted && !archived;
    }
}
