using SpendingsWebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpendingsWebAPI.Extentions
{
    public static class UserExtention
    {
        public static string TestExtention(this User user)
        {
            return user.Email + " " + user.UserName;
        }
    }
}
