using System;

namespace TicketManagementSystem.PriorityFeature.Rules
{
	/// <summary>
	/// Implements a priority rule that adjusts the priority of a ticket based on its age.
	/// Specifically, it elevates the priority of tickets that were created more than one hour ago.
	/// </summary>
	/// <remarks>
	/// This rule is part of the priority management system and is used to ensure that older tickets
	/// receive higher attention by elevating their priority if they meet the specified age condition.
	/// </remarks>
	public sealed class PriorityAgeRule : PriorityRule
	{
		/// <summary>
		/// Applies the priority age rule to a ticket based on its title, current priority, 
		/// and creation timestamp. Elevates the priority if the ticket was created more 
		/// than one hour ago.
		/// </summary>
		/// <param name="title">The title of the ticket.</param>
		/// <param name="current">The current priority of the ticket.</param>
		/// <param name="createdTimestamp">The timestamp when the ticket was created.</param>
		/// <returns>The updated <see cref="Priority"/> after applying the rule.</returns>
		public override Priority Apply(string title, Priority current, DateTime createdTimestamp)
		{
			if (createdTimestamp < DateTime.UtcNow - TimeSpan.FromHours(1))
			{
				return ElevatePriority(current);
			}

			return current;
		}
	}
}
