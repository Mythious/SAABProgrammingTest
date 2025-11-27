using TicketManagementSystem.Core.Validators;
using TicketManagementSystem.TicketsFeature.Models;

namespace TicketManagementSystem.TicketsFeature.Validators
{
	/// <summary>
	/// Provides validation logic for ticket text data, ensuring that required fields such as title and description are not null or empty.
	/// </summary>
	/// <remarks>
	/// This class implements the <see cref="TicketManagementSystem.Core.Validators.IValidator{T}"/> interface to validate instances of <see cref="TicketManagementSystem.TicketsFeature.Models.TicketTextData"/>.
	/// </remarks>
	public class TicketTextValidator : IValidator<TicketTextData>
	{
		public void Validate(TicketTextData data)
		{
			if (string.IsNullOrWhiteSpace(data.Title) || string.IsNullOrWhiteSpace(data.Description))
			{
				throw new InvalidTicketException("Title or description were null or empty");
			}
		}
	}
}
