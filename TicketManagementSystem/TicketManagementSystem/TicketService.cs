using System;
using TicketManagementSystem.Core.Validators;
using TicketManagementSystem.NotificationFeature;
using TicketManagementSystem.PriceFeature;
using TicketManagementSystem.PriorityFeature;
using TicketManagementSystem.TicketsFeature.Models;
using TicketManagementSystem.TicketsFeature.Repositories;
using TicketManagementSystem.TicketsFeature.Validators;
using TicketManagementSystem.TicketsFeature.Exceptions;
using TicketManagementSystem.UserFeature.Models;
using TicketManagementSystem.UserFeature.Services;
using TicketManagementSystem.UserFeature.Exceptions;

namespace TicketManagementSystem
{
    public class TicketService
    {
        // Implemented concrete implementations due to the requirements of keeping Program.cs unchanged.
        // These implementations would normally be dependency injected within the constructor during the bootstrap of the application.
        private readonly IValidator<TicketTextData> _ticketTextValidator = new TicketTextValidator();
        private readonly IUserService _userService = new UserService();
        private readonly IPriorityCalculator _priorityCalculator = new PriorityCalculator();
        private readonly INotificationService _notificationService = new NotificationService();
        private readonly IPriceCalculatorService _priceCalculatorService = new PriceCalculatorService();

		/// <summary>
		/// Creates a new ticket with the specified details and returns its unique identifier.
		/// </summary>
		/// <param name="title">The title of the ticket. Must not be null or empty.</param>
		/// <param name="initialPriority">The initial priority of the ticket.</param>
		/// <param name="assignedTo">The username of the user to whom the ticket is assigned. Can be null if no user is assigned.</param>
		/// <param name="description">A detailed description of the ticket. Must not be null or empty.</param>
		/// <param name="timeStamp">The timestamp indicating when the ticket was created.</param>
		/// <param name="isPayingCustomer">Indicates whether the ticket is created for a paying customer.</param>
		/// <returns>The unique identifier of the created ticket.</returns>
		/// <exception cref="UnknownUserException">Thrown when the provided assigned ticket username cannot be found.</exception>
		/// <exception cref="InvalidTicketException">Thrown when the provided ticket text data fails validation.</exception>
		public int CreateTicket(string title, Priority initialPriority, string assignedTo, string description, DateTime timeStamp, bool isPayingCustomer)
        {
            // Check to see if the title or description are null. If so, throw an exception.
            _ticketTextValidator.Validate(new TicketTextData(title, description));
            
            // Get the found user or return null if they cannot be found
            var foundUser = _userService.GetUser(assignedTo);

            // Calculate the priority given the provided title, initial priority and description
            var priority = _priorityCalculator.Calculate(title, initialPriority, timeStamp);

			// Notify the administrator if the priority is high
			_notificationService.EmailTicketAdministrator(title, assignedTo, priority);

			// Calculate the price determined by priority. If the user is not a paying customer, 0 is returned.
			var price = _priceCalculatorService.Calculate(priority, isPayingCustomer);

            // Create the ticket within the repository and return the ID
            return TicketRepository.CreateTicket(new Ticket()
            {
	            Title = title,
	            AssignedUser = foundUser,
	            Priority = priority,
	            Description = description,
	            Created = timeStamp,
	            PriceDollars = price,
	            AccountManager = GetAccountManagerIfPayingCustomer(isPayingCustomer)
            });
        }

        public void AssignTicket(int id, string username)
        {
            // Acquire the user from the username
            var user = _userService.GetUser(username);

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
