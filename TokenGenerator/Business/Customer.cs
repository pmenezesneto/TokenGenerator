namespace TokenGenerator
{
    public partial class Customer
    {
        public bool Exists(int customerId)
        {
            return this.Id == customerId;
        }
    }
}