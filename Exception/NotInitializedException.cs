using System;

namespace Test_API.Exception
{
    public class NotInitializedException : System.Exception
    {
        public NotInitializedException(string message) : base(message) { }

    }

}