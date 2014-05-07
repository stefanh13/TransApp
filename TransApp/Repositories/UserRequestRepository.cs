using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransApp.DAL;
using TransApp.Models;

namespace TransApp.Repositories
{
    public class UserRequestRepository
    {
        UserRequestContext UserRequestDb = new UserRequestContext();

        public IEnumerable<UserRequest> GetAllUserRequests()
        {
            return UserRequestDb.userRequest;
        }

    }
}