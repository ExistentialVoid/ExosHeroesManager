namespace ExosHeroesManager
{
    /// <summary>
    /// An object to represent a single record from a datatable
    /// </summary>
    internal interface IDataRepresenter
    {
        /// <summary>
        /// The query-building class for the database this object exists
        /// </summary>
        protected TableAccessor Accessor { get; }
        /// <summary>
        /// The record this object represents
        /// </summary>
        protected DataRow? Me { get; set; }
        /// <summary>
        /// Current value of this object's primary key
        /// </summary>
        public string PrimaryKeyValue { get; }

        protected bool UpdateData(int clmn, string val);
    }
}
