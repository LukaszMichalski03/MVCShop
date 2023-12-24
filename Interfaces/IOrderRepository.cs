using LoginRegisterIdentity.Models;

namespace LoginRegisterIdentity.Interfaces
{
    public interface IOrderRepository
    {
        public Task<IEnumerable<Order>> GetOrdersByUser(string id);
        public bool Add (Order order);
        public bool Save ();
        
    }
}
