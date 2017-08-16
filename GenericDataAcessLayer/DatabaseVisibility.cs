using System;

namespace GenericDataAcessLayer
{
    /// <summary>
    /// Specifies the visibility of a property
    /// </summary>
    [Flags]
    public enum DatabaseVisibility
    {
        /// <summary>
        /// The property is required.
        /// </summary>
        Required = 1,
        /// <summary>
        /// The property is optional:<para />
        /// If the property value is different to the property types default
        /// value the property will be sent to the database.
        /// </summary>
        Optional = 2,
        /// <summary>
        /// The property is ignored.
        /// </summary>
        Ignored = 4
    }
}
