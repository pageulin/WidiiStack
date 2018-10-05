using System;
using MongoDB.Driver;
using Test_API.Exception;

namespace Test_API.DAL
{
    public class MongoDBContext
    {
        private string _dbServer;
        private string _dbUser;
        private string _dbPassword;
        private string _dbName;
        private string _connectionString;
        private readonly IMongoDatabase _database = null;
        private MongoClient _client = null;
        private static MongoDBContext _context = null;

        private MongoDBContext(string dbServer, string dbUser, string dbPassword, string dbName)
        {
            this._dbServer = dbServer;
            this._dbUser = dbUser;
            this._dbPassword = dbPassword;
            this._dbName = dbName;
            this._connectionString = "mongodb://" + dbUser + ":" + dbPassword + "@" + dbServer;
            this._client = new MongoClient(this._connectionString);
            this._database = this._client.GetDatabase(dbName);  
        }

        public static void init(string dbServer, string dbUser, string dbPassword, string dbName)
        {
             _context = new MongoDBContext(dbServer, dbUser, dbPassword, dbName);
        }

        /// <summary>
        /// 
        /// </summary>
        public static MongoDBContext GetInstance()
        {
            if(_context == null)
            {
               throw new NotInitializedException("Le contexte de la base de donnée MongoDB n'a pas été initalisé");
            }
            return _context;
        }

        public IMongoDatabase GetDatabase()
        {
            return this._database;
        }
    }
}