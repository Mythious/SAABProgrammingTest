using EmailService;

namespace TicketManagementSystem.NotificationFeature
{
	/// <summary>
	/// Provides notification services for the ticket management system, 
	/// including sending email notifications to administrators based on ticket priority.
	/// </summary>
	public class NotificationService : INotificationService
	{
		// Utilized interface implemented that typically is provided during construction through dependency injection.
		private readonly IEmailService _emailService = new EmailServiceProxy();

		/// <summary>
		/// Sends an email notification to the ticket administrator if the priority of the incident is high.
		/// </summary>
		/// <param name="incidentTitle">The title of the incident requiring attention.</param>
		/// <param name="assignedTo">The username of the individual assigned to the incident.</param>
		/// <param name="priority">The priority level of the incident. Only high-priority incidents trigger an email notification.</param>
		public void EmailTicketAdministrator(string incidentTitle, string assignedTo, Priority priority)
		{
			if (priority == Priority.High)
			{
				_emailService.SendEmailToAdministrator(incidentTitle, assignedTo);
			}
		}
	}
}
