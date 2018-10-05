using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test_API.Entity;
using MongoDB.Driver;

namespace Test_API.DAL.Service
{
    class UserService : IUserService
    {
        private IMongoCollection<User> _data = null;
        public UserService()
        {
            this._data = MongoDBContext.GetInstance().GetDatabase().GetCollection<User>("User");
            CreateIndexOptions indexOptions = new CreateIndexOptions();
            indexOptions.Unique = true;
            CreateIndexModel<User> modelIndex = new CreateIndexModel<User>(Builders<User>.IndexKeys.Ascending(_ => _.login), indexOptions);
            this._data.Indexes.CreateOneAsync(modelIndex);
        }

        public bool CreateUser(User user)
        {
            try
            {
                this._data.InsertOne(user);
            }
            catch(System.Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<User>> FindUsers()
        {
            return await _data.Find(u => true).ToListAsync();
        }
    }
}