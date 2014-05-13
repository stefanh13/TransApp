using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransApp.Models;

namespace TransApp.DAL
{
    public class UserRequestInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<UserRequestContext>
    {
        protected override void Seed(UserRequestContext context)
        {
            var req = new List<UserRequest>
            {
                new UserRequest{
                    userName = "Stefán",
                    requestName = "James Bond",
                    requestText = "mig vantar hjálp",
                    requestLanguage = "Enska",
                    likes = 8,
                    requestTime = DateTime.Parse("2014-02-28 15:00:00")
                },
                new UserRequest{
                    userName = "Rikki",
                    requestName = "Hobbit",
                    requestText = "Halló mig vantar hjálp",
                    requestLanguage = "Enska",
                    likes = 32,
                    requestTime = DateTime.Parse("2014-02-27 15:00:00")
                },
                new UserRequest{
                    userName = "Kristján",
                    requestName = "Men in Black",
                    requestText = "mig vantar hjálp",
                    requestLanguage = "Íslenska",
                    likes = 100,
                    requestTime = DateTime.Parse("2014-02-26 15:00:00")
                },
                new UserRequest{
                    userName = "Hafþór",
                    requestName = "Hobbit",
                    requestText = "mig vantar hjálp",
                    requestLanguage = "Franska",
                    likes = 10,
                    requestTime = DateTime.Parse("2014-02-25 15:00:00")
                },
                new UserRequest{
                    userName = "Sæli",
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