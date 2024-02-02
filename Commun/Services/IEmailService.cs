using Foodies.Api.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodies.Api.Common.Services
{
    public interface IEmailService
    {
        void SendEmail(Message message);
    }
}
