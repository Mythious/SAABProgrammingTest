namespace TicketManagementSystem.NotificationFeature
{
	/// <summary>
	/// Defines the contract for notification communication services within the ticket management system.
	/// </summary>
	public interface INotificationService
	{
		/// <summary>
		/// Sends an email notification to the ticket administrator regarding a specific incident, providing the priority is high.
		/// </summary>
		/// <param name="incidentTitle">The title of the incident requiring attention.</param>
		/// <param name="assignedTo">The username of the individual assigned to the incident.</param>
		/// <param name="priority">The priority level of the incident.</param>
		void EmailTicketAdministrator(string incidentTitle, string assignedTo, Priority priority);
	}
}
