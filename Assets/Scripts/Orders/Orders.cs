namespace Cuvium.Core
{
    public class Orders
    {
        public OrderGameEvent OnOrderSent;

        public void SendOrder(Order order)
        {
            OnOrderSent.Raise(order);
        }
    }
}

