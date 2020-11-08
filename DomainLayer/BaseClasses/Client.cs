using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLayer
{
    public class Client
    {
        public int Id { get; set; }
        private string _name;
        /// <summary>
        /// Checks if name is null or empty before setting
        /// </summary>
        public string Name { get => _name; set { if (string.IsNullOrEmpty(value)) { throw new ArgumentException("Name can't be null or empty."); } _name = value; } }
        private string _address;
        /// <summary>
        /// Checks if aders is smaller than or equal to 10 characters or 
        /// </summary>
        public string Address { get => _address; set { if (value.Length >= 10) { throw new ArgumentException("Adress can't be bigger or equal to 10 characters"); } _address = value; } }
        public HashSet<Order> Orders { private get; set; } = new HashSet<Order>();

        public Client(string name, string address)
        {
            Name = name;
            Address = address;
        }
        /// <summary>
        /// if client is the same adds order to orders list.
        /// If order is already in orders adds the amount together
        /// </summary>
        /// <param name="order">order to add</param>
        public void AddOrder(Order order)
        {
            if (order.Client == this)
            {
                if (!Orders.Add(order))
                {
                    Orders.TryGetValue(order, out Order orderInSet);
                    orderInSet.Amount = orderInSet.Amount + order.Amount;
                }
            }
            else
            {
                throw new ArgumentException("client is not the same.");
            }
        }
        /// <summary>
        /// Returns the list as readonly
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<Order> GetOrders()
        {
            return Orders.ToList().AsReadOnly();
        }

        public override bool Equals(object obj)
        {
            return obj is Client client &&
                   _name == client._name &&
                   _address == client._address;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_name, _address);
        }
    }
}
