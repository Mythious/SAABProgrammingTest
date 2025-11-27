using System;
using System.Collections.Generic;
using TicketManagementSystem.PriorityFeature.Rules;

namespace TicketManagementSystem.PriorityFeature
{
	/// <summary>
	/// Provides functionality to calculate the priority of a ticket based on a set of predefined rules.
	/// </summary>
	/// <remarks>
	/// The <see cref="PriorityCalculator"/> class evaluates a ticket's priority by applying a series of 
	/// rules defined in the system. Each rule has the potential to elevate the priority of a ticket 
	/// based on specific conditions, such as the ticket's age or keywords in its title. Only one rule 
	/// is allowed to elevate the priority during the evaluation process.
	/// </remarks>
	public class PriorityCalculator : IPriorityCalculator
	{
		private readonly IEnumerable<PriorityRule> _rules;

		/// <summary>
		/// Initializes a new instance of the <see cref="PriorityCalculator"/> class.
		/// </summary>
		/// <remarks>
		/// This constructor initializes the priority calculator with a predefined set of rules, 
		/// including <see cref="PriorityAgeRule"/> and <see cref="PriorityKeywordRule"/>. These rules 
		/// are used to evaluate and potentially elevate the priority of tickets based on specific 
		/// conditions.
		/// </remarks>
		public PriorityCalculator()
		{
			_rules = new List<PriorityRule>
			{
				new PriorityAgeRule(),
				new PriorityKeywordRule()
			};
		}

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
		/// The calculated <see cref="Priority"/> value for the ticket, potentially adjusted by the rules.
		/// </returns>
		/// <remarks>
		/// This method evaluates the ticket's priority by applying a series of predefined rules. 
		/// If any rule modifies the priority, the evaluation stops, and the modified priority is returned.
		/// If no rule modifies the priority, the initial priority is returned.
		/// </remarks>
		public Priority Calculate(string title, Priority initialPriority, DateTime createdTimestamp)
		{
			var priority = initialPriority;

			foreach (var rule in _rules)
			{
				var result = rule.Apply(title, priority, createdTimestamp);

				if (result != priority)
				{
					// Return the result if the priority has already been escalated.
					return result;
				}
			}

			return priority;
		}
	}
}
