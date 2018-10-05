using System;
using System.Collections.Generic;
using Test_API.Entity;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Test_API.DAL.Service
{
    public interface IUserService
    {
        Task<IEnumerable<User>> FindUsers();
        bool CreateUser(User user);

    }
}