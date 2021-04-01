using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.Domain.Exceptions
{
    public class EmailException : Exception
    {
        public EmailException(string email, Exception ex) : base($"Email \"{email}\" is invalid.", ex)
        {

        }
    }
}
