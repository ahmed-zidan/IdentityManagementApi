﻿using Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IJWTService
    {
        string CreateJWT(AppUser user);
        
    }
}
