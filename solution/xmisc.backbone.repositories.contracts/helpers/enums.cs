namespace reexmonkey.xmisc.backbone.repositories.contracts.helpers
{
    /// <summary>
    /// Represents the type of action to undertake when the parent data model of a related data model is deleted or updated.
    /// </summary>
    public enum ReferentialIntegrityType
    {
        /// <summary>
        /// When a parent data model is deleted or updated, cascade the operation to dependent (child) data models.
        /// </summary>
        CASCADE,

        /// <summary>
        /// hen a parent data model is deleted or updated, set the foreign key of affected data models to null value.
        /// </summary>
        SET_NULL,

        /// <summary>
        ///  When a parent data model is deleted or updated, check the referential integrity constraint, raise an exception and roll back the deletion.
        /// </summary>
        RESTRICT,

        /// <summary>
        ///  When a parent entity is deleted or updated, set the foreign key of affected entities to to its default value.
        /// </summary>
        SET_DEFAULT,

        /// <summary>
        ///  When a parent data model is deleted or updated, raise an exception, roll back the deletion and check the referential integrity constraint.
        /// </summary>
        NO_ACTION
    }
}
