namespace DomainLayer
{
    public interface IDomainController
    {
        /// <summary>
        /// Adds client to database if not already in database. 
        /// And returns client from database
        /// </summary>
        /// <param name="client">client to insert</param>
        /// <returns></returns>
        Client AddClient(Client client);
        /// <summary>
        /// Deletes client from database with id given.
        /// if in database and if client doesnt have any orders.
        /// </summary>
        /// <param name="id">id from client to delete</param>
        void DeleteClient(int id);
        /// <summary>
        /// Gets client with all orders from database with id given.
        /// if client is in database.
        /// </summary>
        /// <param name="id">id from client to get</param>
        /// <returns></returns>
        Client GetClient(int id);
        /// <summary>
        /// Updates client from database with id from client object and values from client object.
        /// </summary>
        /// <param name="client">client to update gotten from database</param>
        /// <returns></returns>
        Client UpdateClient(Client client);
        /// <summary>
        /// Adds order from client to database via the foreign key.
        /// if order already is in database with the same client => adds amounts together and updates the database object.
        /// </summary>
        /// <param name="order"></param>
        /// <param name="clientID"></param>
        /// <returns></returns>
        Order AddOrder(Order order, int clientId);
        /// <summary>
        /// Deletes order from Client derived with ClientId from database
        /// </summary>
        /// <param name="id">id from order to delete</param>
        /// <param name="clientId">clientId to remove link</param>
        void DeleteOrder(int id);
        /// <summary>
        /// Gets order from Client derived with ClientId from database
        /// </summary>
        /// <param name="id">id from order to get</param>
        /// <param name="clientId">clientId to get client link</param>
        /// <returns></returns>
        Order GetOrder(int id);
        /// <summary>
        /// Updates order from client derived with clientId from database
        /// </summary>
        /// <param name="order">order to update</param>
        /// <param name="clientId">clientId for link</param>
        /// <returns></returns>
        Order UpdateOrder(Order order);
        /// <summary>
        /// Checks if Client is in Database based on clientId.
        /// </summary>
        /// <param name="id">clientId</param>
        /// <returns></returns>
        bool IsInClients(int id);
        /// <summary>
        /// Checks if Order is in Database based on orderId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool IsInOrders(int id);
    }
}
