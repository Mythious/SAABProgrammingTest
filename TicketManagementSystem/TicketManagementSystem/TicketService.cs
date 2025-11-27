using System;
using System.Configuration;
using System.IO;
using System.Text.Json;
using EmailService;
using TicketManagementSystem.Core.Validators;
using TicketManagementSystem.TicketsFeature.Models;
using TicketManagementSystem.TicketsFeature.Validators;

namespace TicketManagementSystem
{
    public class TicketService
    {
        // Implemented concrete implementations due to the requirements of keeping Program.cs unchanged.
        // These implementations would normally be dependency injected during the bootstrap of the application.
        private readonly IValidator<TicketTextData> _ticketTextValidator = new TicketTextValidator();
        
        public int CreateTicket(string title, Priority priority, string assignedTo, string description, DateTime timeStamp, bool isPayingCustomer)
        {
            _ticketTextValidator.Validate(new TicketTextData(title, description));

            User user = null;
            using (var ur = new UserRepository())
            {
                if (assignedTo != null)
                {
                    user = ur.GetUser(assignedTo);
                }
            }

            if (user == null)
            {
                throw new UnknownUserException("User " + assignedTo + " not found");
            }

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
            User accountManager = null;
            if (isPayingCustomer)
            {
                // Only paid customers have an account manager.
                accountManager = new UserRepository().GetAccountManager();
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
                AssignedUser = user,
                Priority = priority,
                Description = description,
                Created = timeStamp,
                PriceDollars = price,
                AccountManager = accountManager
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

        private void WriteTicketToFile(Ticket ticket)
        {
            var ticketJson = JsonSerializer.Serialize(ticket);
            File.WriteAllText(Path.Combine(Path.GetTempPath(), $"ticket_{ticket.Id}.json"), ticketJson);
        }
    }

    public enum Priority
    {
        High,
        Medium,
        Low
    }
}
