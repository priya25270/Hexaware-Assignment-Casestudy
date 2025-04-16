using System;

namespace SISApp
{
    internal class SISException : Exception
    {
        public SISException() : base() { }

        public SISException(string message) : base(message) { }
    }
}
