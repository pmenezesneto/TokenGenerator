namespace TokenGenerator
{
    /// <summary>
    /// Customer class.
    /// </summary>
    public partial class Customer
    {
        /// <summary>
        /// Check if customer exists.
        /// </summary>
        /// <param name="customerId">customerId</param>
        /// <returns>True if exists, false otherwise.</returns>
        public bool Exists(int customerId)
        {
            return this.Id == customerId;
        }
    }
}
