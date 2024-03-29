﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainLayer
{
    public class Client
    {
        /// <summary>
        /// client id
        /// </summary>
        public int Id { get; set; }
        private string _name;
        /// <summary>
        /// Checks if name is null or empty before setting
        /// </summary>
        public string Name { get => _name; set { if (string.IsNullOrEmpty(value)) { throw new ArgumentException("Name can't be null or empty."); } _name = value; } }
        private string _address;
        /// <summary>
        /// Checks if address is smaller than or equal to 10 characters and is not empty before setting
        /// </summary>
        public string Address { get => _address; set { if (value.Length <= 10) throw new ArgumentException("Adress must be bigger or equal to 10 characters"); if (string.IsNullOrEmpty(value)) throw new Exception("Address can't be null"); _address = value; } }
        private HashSet<Order> orders = new HashSet<Order>();
        /// <summary>
        /// Constructor for Domain/PresentationLayer
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="address">address</param>
        public Client(string name, string address)
        {
            Name = name;
            Address = address;
        }
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Client()
        {

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
                if (!orders.Add(order))
                {
                    orders.TryGetValue(order, out Order orderInSet);
                    orderInSet.Amount += order.Amount;
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
            return orders.ToList().AsReadOnly();
        }
        /// <summary>
        /// Removes order
        /// </summary>
        /// <param name="order">order to remove</param>
        public void RemoveOrder(Order order)
        {
            if (orders.Contains(order))
                orders.Remove(order);
            else
                throw new Exception("order not found.");
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
