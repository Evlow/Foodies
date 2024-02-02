﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodies.Common.Models
{
    public class EmailConfiguration
    {
        public string From { get; set; } = null;

        public string SmtpServer { get; set; } = null;

        public int Port { get; set; }

        public string Username { get; set; } = null;

        public string Password { get; set; } = null;

    }
}