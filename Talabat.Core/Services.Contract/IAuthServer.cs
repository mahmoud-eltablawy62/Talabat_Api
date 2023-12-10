﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Core.Services.Contract
{
    public interface IAuthServer
    {
        Task<string> CreateTokenAsync(Users userApp  , UserManager <Users> userManager); 
    }
}
