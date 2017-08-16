using System;

namespace GenericDataAcessLayer
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class DatabaseEvent : Attribute
    {
        public DatabaseVisibility VisibilityTargets { get; set; }

        public abstract bool Can(bool archived, bool deleted);

        public DatabaseEvent(DatabaseVisibility visibilityTargets)
        {
            VisibilityTargets = visibilityTargets;
        }

        public DatabaseEvent()
        {
            VisibilityTargets = DatabaseVisibility.Ignored;
        }
    }
}
