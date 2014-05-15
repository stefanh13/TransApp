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
        UserRequestContext userRequestDb = new UserRequestContext();

        public IEnumerable<UserRequest> GetAllUserRequests()
        {
            return userRequestDb.userRequest;
        }

        public void AddUserRequests(UserRequest newReq)
        {
            userRequestDb.userRequest.Add(newReq);
            Save();
        }

        public void Save()
        {
            userRequestDb.SaveChanges();
        }

        public void UpdateLike(int? reqId)
        {
            foreach(var item in userRequestDb.userRequest)
            {
                if (item.ID == reqId)
                {
                    item.likes += 1;
                    int tala = item.likes;
                    //userRequestDb.Entry(item).CurrentValues.SetValues(item.likes);
           
                    break;
                }
            }
            Save();
            /*
            var x = userRequestDb.userRequest.Find(reqId);
            userRequestDb.Entry(x).CurrentValues.SetValues(x.Us)
             */
        }

        public bool IsIdValid(int? id)
        {
            foreach(var item in userRequestDb.userRequest)
            {
                if(item.ID == id)
                {
                    return true;
                }
            }

            return false;
        }
    }
}