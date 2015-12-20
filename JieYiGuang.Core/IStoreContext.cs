using JieYiGuang.Core.Domain.Stores;

namespace JieYiGuang.Core
{
    /// <summary>
    /// Store context
    /// </summary>
    public interface IStoreContext
    {
        /// <summary>
        /// Gets or sets the current store
        /// </summary>
        Store CurrentStore { get; }
    }
}
