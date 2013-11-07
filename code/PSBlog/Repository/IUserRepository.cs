﻿using PSBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSBlog.Repository
{
    public interface IUserRepository:IRepository<User>
    {
        User FindByUserName(string username);
    }
}
