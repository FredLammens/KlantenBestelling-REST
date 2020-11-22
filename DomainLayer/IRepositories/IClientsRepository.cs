namespace DomainLayer.IRepositories
{
    public interface IClientsRepository
    {
        /// <summary>
        /// Adds client to database if not already in database. 
        /// </summary>
        /// <param name="client">client to insert</param>
        /// <returns></returns>
        void AddClient(Client client);
        /// <summary>
        /// Updates client from database with id from client object and values from client object.
        /// </summary>
        /// <param name="client">client to update gotten from database</param>
        /// <returns></returns>
        void UpdateClient(Client client);
        /// <summary>
        /// Gets client with all orders from database with id given.
        /// if client is in database.
        /// </summary>
        /// <param name="id">id from client to get</param>
        /// <returns></returns>
        Client GetClient(int id);
        /// <summary>
        /// Gets client with the given name and address.
        /// </summary>
        /// <param name="Name">Name</param>
        /// <param name="Address">Address</param>
        /// <returns></returns>
        Client GetClient(string Name, string Address);
        /// <summary>
        /// Deletes client from database with id given.
        /// if in database and has no orders
        /// </summary>
        /// <param name="id">id from client to delete</param>
        void DeleteClient(int id);
        /// <summary>
        /// Checks if client has orders
        /// </summary>
        /// <param name="id">clientId</param>
        /// <returns></returns>
        bool HasOrders(int id);
        /// <summary>
        /// Checks if client with id is in the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool IsInClients(int id);
    }
}
