using System;

namespace TicketManagementSystem.PriorityFeature.Rules
{
	/// <summary>
	/// Represents an abstract base class for defining rules to manage and modify ticket priorities
	/// within the Ticket Management System.
	/// </summary>
	public abstract class PriorityRule
	{
		/// <summary>
		/// Applies the priority rule to a ticket based on its title, current priority, and creation timestamp.
		/// </summary>
		/// <param name="title">The title of the ticket.</param>
		/// <param name="current">The current priority of the ticket.</param>
		/// <param name="createdTimestamp">The timestamp when the ticket was created.</param>
		/// <returns>The updated <see cref="Priority"/> after applying the rule.</returns>
		public abstract Priority Apply(string title, Priority current, DateTime createdTimestamp);

		/// <summary>
		/// Elevates the priority of a ticket to the next higher level, if applicable.
		/// </summary>
		/// <param name="priority">The current <see cref="Priority"/> of the ticket.</param>
		/// <returns>The elevated <see cref="Priority"/> if applicable; otherwise, the original priority.</returns>
		internal virtual Priority ElevatePriority(Priority priority) => priority switch
		{
			Priority.Low => Priority.Medium,
			Priority.Medium => Priority.High,
			_ => priority
		};
	}
}
