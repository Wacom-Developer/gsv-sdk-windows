using System;
using GsvPersistence.Interfaces;

namespace GsvPersistence.DataModels
{
    /// <summary>
    /// The base data model.
    /// </summary>
    public abstract class DataModelBase : IEntity
    {
        protected string _id;
        protected string _name;

        /// <summary>
        /// Id of the entity
        /// </summary>
        public string Id => _id;

        /// <summary>
        /// Name of the entity.
        /// </summary>
        public string Name => _name;

        /// <summary>
        /// Contstructor
        /// </summary>
        public DataModelBase()
        {
            _id = Guid.NewGuid().ToString();
        }
    }
}
