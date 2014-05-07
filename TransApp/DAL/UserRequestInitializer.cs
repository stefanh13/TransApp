using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransApp.Models;

namespace TransApp.DAL
{
    public class UserRequestInitializer : System.Data.Entity.DropCreateDatabaseAlways<UserRequestContext>
    {
        protected override void Seed(UserRequestContext context)
        {
            var req = new List<UserRequest>
            {
                new UserRequest{
                    uID = 1,
                    requestName = "James Bond",
                    requestText = "mig vantar hjálp",
                    requestLanguage = "Enska",
                    likes = 8,
                    requestTime = DateTime.Parse("2014-02-20 15:00:00")
                },
                new UserRequest{
                    uID = 4,
                    requestName = "Hobbit",
                    requestText = "Halló mig vantar hjálp",
                    requestLanguage = "Enska",
                    likes = 32,
                    requestTime = DateTime.Parse("2014-02-21 15:00:00")
                },
                new UserRequest{
                    uID = 2,
                    requestName = "Men in Black",
                    requestText = "mig vantar hjálp",
                    requestLanguage = "Íslenska",
                    likes = 100,
                    requestTime = DateTime.Parse("2014-02-22 15:00:00")
                },
                new UserRequest{
                    uID = 3,
                    requestName = "Hobbit",
                    requestText = "mig vantar hjálp",
                    requestLanguage = "Franska",
                    likes = 10,
                    requestTime = DateTime.Parse("2014-02-23 15:00:00")
                },
                new UserRequest{
                    uID = 9,
                    requestName = "Lord of the rings",
                    requestText = "what mig vantar hjálp",
                    requestLanguage = "Þýska",
                    likes = 2,
                    requestTime = DateTime.Parse("2014-02-24 15:00:00")
                },
            };

            req.ForEach(s => context.userRequest.Add(s));
            context.SaveChanges();
        }
    }
}