﻿using System;
using System.Security.AccessControl;
using Eagle.Domain.Core.Model;
using Eagle.Infrastructrue.Utility;

namespace Eagle.Model
{
    public class Account : IEntity
    {
        public Guid ID { get; set; }

        public string LoginID { get; set; }

        public string Password { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime LastLoginTime { get; set; }

        public bool State { get; set; }

        public void SetPassword(string newPassword)
        {
            Password = newPassword.MD5().ToLower();
        }
    }
}