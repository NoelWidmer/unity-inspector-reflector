namespace InspectorReflector
{
    /// <summary>
    ///     This type supports various ways to customize the IR visuals.
    /// </summary>
    public enum InspectionKind
    {
        /// <summary>
        ///     The member will be drawn using the default IR visuals.
        /// </summary>
        DefaultIR,
        /// <summary>
        ///     The member won't be mutated through the Inspector.
        /// </summary>
        Immutable,
        /// <summary>
        ///     The member won't be mutated through the Inspector. The text displayed can be selected and copyied.
        /// </summary>
        ImmutableSelectable,
        /// <summary>
        ///     An asset from the project can be assigned to this member through drag and drop. 
        ///     Scene objects are not allowed.
        /// </summary>
        DropableObject,
        /// <summary>
        ///     An asset from the project can be assigned to this member through drag and drop. 
        ///     Scene objects are allowed.
        /// </summary>
        DropableObjectAllowSceneObjects,
        /// <summary>
        /// TODO. 
        /// Build into unity. Works for: double, float, int and string. Effect unknown.
        /// </summary>
        Delayed,
        /// <summary>
        /// TDOO
        /// </summary>
        Tag,
        /// <summary>
        /// TODO.
        /// </summary>
        TextArea
    }
}