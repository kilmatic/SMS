﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMSWebAPI.Models
{
    public partial class SMSEntities
    {
        public SMSEntities(string password)
        : base("name=SMSEntities")
        {
            this.Database.Connection.ConnectionString += $";Password=";
        }
    }
}