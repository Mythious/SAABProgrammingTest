using System;
namespace TicketManagementSystem.TicketsFeature.Exceptions
{
    public class InvalidTicketException : Exception
    {
        public InvalidTicketException(string message) : base(message)
        {
        }
    }
}
