using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_BusinessLayer.Concrete.Tools.ErrorHandling.Exceptions
{
    public class InvalidBotModeException : Exception
    {
        public InvalidBotModeException()
            : base("Geçersiz bot modu hatası.") { }
        public InvalidBotModeException(string message)
            : base(message) { }
        public InvalidBotModeException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
