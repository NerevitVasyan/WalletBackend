﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpendingsWebAPI.Entities
{
    public class User : IdentityUser
    {
        public List<Spending> Spendings { get; set; }
    }
}
