using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodies.Api.Buisness.DTO.Auth
{
    // DTO pour les réponses des opérations d'authentification
    public class Response
    {
        public string? Status { get; set; }

        public string? Message { get; set; }
    }
}
