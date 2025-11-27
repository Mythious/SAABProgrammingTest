using System;
namespace TicketManagementSystem.UserFeature.Exceptions
{
    public class UnknownUserException : Exception
    {
        public UnknownUserException(string message) : base(message)
        {
        }
    }
}
