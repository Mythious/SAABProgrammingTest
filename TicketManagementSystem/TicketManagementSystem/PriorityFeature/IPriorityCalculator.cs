using System;

namespace TicketManagementSystem.PriorityFeature
{
	/// <summary>
	/// Defines a contract for calculating the priority of a ticket based on its title, 
	/// initial priority, and creation timestamp.
	/// </summary>
	public interface IPriorityCalculator
	{
		/// <summary>
		/// Calculates the priority of a ticket based on its title, initial priority, and creation timestamp.
		/// </summary>
		/// <param name="title">
		/// The title of the ticket, which may influence the priority calculation.
		/// </param>
		/// <param name="initialPriority">
		/// The initial priority of the ticket before any adjustments.
		/// </param>
		/// <param name="createdTimestamp">
		/// The timestamp indicating when the ticket was created.
		/// </param>
		/// <returns>
		/// The calculated <see cref="Priority"/> value for the ticket.
		/// </returns>
		Priority Calculate(string title, Priority initialPriority, DateTime createdTimestamp);
	}
}
