using System;
using System.Configuration;
using System.IO;
using System.Text.Json;
using EmailService;
using TicketManagementSystem.Core.Validators;
using TicketManagementSystem.TicketsFeature.Models;
using TicketManagementSystem.TicketsFeature.Validators;
using TicketManagementSystem.UserFeature.Services;

namespace TicketManagementSystem
{
    public class TicketService
    {
        // Implemented concrete implementations due to the requirements of keeping Program.cs unchanged.
        // These implementations would normally be dependency injected within the constructor during the bootstrap of the application.
        private readonly IValidator<TicketTextData> _ticketTextValidator = new TicketTextValidator();
        private readonly IUserService _userService = new UserService();
        
        public int CreateTicket(string title, Priority priority, string assignedTo, string description, DateTime timeStamp, bool isPayingCustomer)
        {
            // Check to see if the title or description are null. If so, throw an exception.
            _ticketTextValidator.Validate(new TicketTextData(title, description));
            
            // Get the found user or return null if they cannot be found
            var foundUser = _userService.GetUser(assignedTo);

            var priorityRaised = false;
            if (timeStamp < DateTime.UtcNow - TimeSpan.FromHours(1))
            {
                if (priority == Priority.Low)
                {
	                priority = Priority.Medium;
                    priorityRaised = true;
                }
                else if (priority == Priority.Medium)
                {
	                priority = Priority.High;
                    priorityRaised = true;
                }
            }

            if ((title.Contains("Crash") || title.Contains("Important") || title.Contains("Failure")) && !priorityRaised)
            {
                if (priority == Priority.Low)
                {
                    priority = Priority.Medium;
                }
                else if (priority == Priority.Medium)
                {
                    priority = Priority.High;
                }
            }

            if (priority == Priority.High)
            {
                var emailService = new EmailServiceProxy();
                emailService.SendEmailToAdministrator(title, assignedTo);
            }

            double price = 0;
            if (isPayingCustomer)
            {
                if (priority == Priority.High)
                {
                    price = 100;
                }
                else
                {
                    price = 50;
                }
            }

            var ticket = new Ticket()
            {
                Title = title,
                AssignedUser = foundUser,
                Priority = priority,
                Description = description,
                Created = timeStamp,
                PriceDollars = price,
                AccountManager = GetAccountManagerIfPayingCustomer(isPayingCustomer)
            };

            var id = TicketRepository.CreateTicket(ticket);

            // Return the id
            return id;
        }

        public void AssignTicket(int id, string username)
        {
            User user = null;
            using (var ur = new UserRepository())
            {
                if (username != null)
                {
                    user = ur.GetUser(username);
                }
            }

            if (user == null)
            {
                throw new UnknownUserException("User not found");
            }

            var ticket = TicketRepository.GetTicket(id);

            if (ticket == null)
            {
                throw new ApplicationException("No ticket found for id " + id);
            }

            ticket.AssignedUser = user;

            TicketRepository.UpdateTicket(ticket);
        }

        /// <summary>
        /// Retrieves the account manager if the customer is a paying customer.
        /// </summary>
        /// <param name="isPayingCustomer">A boolean value indicating whether the customer is a paying customer.</param>
        /// <returns>
        /// The <see cref="User"/> object representing the account manager if the customer is a paying customer; 
        /// otherwise, <c>null</c>.
        /// </returns>
		private User GetAccountManagerIfPayingCustomer(bool isPayingCustomer)
		{
			return isPayingCustomer ? _userService.GetAccountManager() : null;
		}
    }

    public enum Priority
    {
        High,
        Medium,
        Low
    }
}
