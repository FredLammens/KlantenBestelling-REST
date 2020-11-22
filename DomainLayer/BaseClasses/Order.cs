using System;

namespace DomainLayer
{
    public class Order
    {
        /// <summary>
        /// Order id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Product of order
        /// </summary>
        public Product Product { get; set; } //kan per definitie niet null zijn.
        private int _amount;
        /// <summary>
        /// if amount is not less then 0 sets amount
        /// </summary>
        public int Amount { get => _amount; set { if (value < 0) throw new Exception("Amount can't be empty."); _amount = value; } }
        private Client _client;
        /// <summary>
        /// if client is not empty sets client
        /// </summary>
        public Client Client { get => _client; set { if (value == null) throw new Exception("Client can't be empty"); _client = value; } }
        /// <summary>
        /// Constructor for order
        /// </summary>
        /// <param name="product">product</param>
        /// <param name="amount">mount</param>
        /// <param name="client">client object</param>
        public Order(Product product, int amount, Client client)
        {
            Product = product;
            Amount = amount;
            Client = client;
        }
        /// <summary>
        /// Empty constructor
        /// </summary>
        public Order()
        {

        }

        public override bool Equals(object obj)
        {
            return obj is Order order &&
                   Product == order.Product;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Product);
        }
    }
}
