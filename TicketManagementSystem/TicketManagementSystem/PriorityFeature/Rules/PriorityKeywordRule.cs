using System;
using System.Linq;

namespace TicketManagementSystem.PriorityFeature.Rules
{
	/// <summary>
	/// Represents a rule that evaluates ticket titles for specific keywords to determine if the ticket's priority should be elevated.
	/// </summary>
	/// <remarks>
	/// This rule checks for predefined keywords such as "Crash", "Important", and "Failure" in the ticket title.
	/// If any of these keywords are found, the priority of the ticket is elevated to the next higher level.
	/// </remarks>
	public sealed class PriorityKeywordRule : PriorityRule
	{
		private static readonly string[] Keywords = { "Crash", "Important", "Failure" };

		/// <summary>
		/// Applies the priority keyword rule to evaluate and potentially elevate the priority of a ticket.
		/// </summary>
		/// <param name="title">The title of the ticket to be evaluated.</param>
		/// <param name="current">The current priority of the ticket.</param>
		/// <param name="createdTimestamp">The timestamp when the ticket was created.</param>
		/// <returns>
		/// The updated <see cref="Priority"/> if the ticket title contains any predefined keywords; 
		/// otherwise, returns the current priority.
		/// </returns>
		/// <remarks>
		/// This method checks the ticket title for specific keywords such as "Crash", "Important", and "Failure".
		/// If any of these keywords are found, the priority is elevated to the next higher level.
		/// </remarks>
		public override Priority Apply(string title, Priority current, DateTime createdTimestamp)
		{
			if (Keywords.Any(k => title.Contains(k, StringComparison.OrdinalIgnoreCase)))
			{
				return ElevatePriority(current);
			}

			return current;
		}
	}
}
