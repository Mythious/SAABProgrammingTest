namespace TicketManagementSystem.PriceFeature
{
	/// <summary>
	/// Provides functionality to calculate the price of a ticket based on its priority
	/// and whether the customer is a paying customer.
	/// </summary>
	public class PriceCalculatorService : IPriceCalculatorService
	{
		/// <summary>
		/// Calculates the price of a ticket based on its priority and whether the customer is a paying customer.
		/// </summary>
		/// <param name="priority">The priority level of the ticket. It can be <see cref="Priority.High"/>, <see cref="Priority.Medium"/>, or <see cref="Priority.Low"/>.</param>
		/// <param name="isPayingCustomer">A boolean indicating whether the customer is a paying customer.</param>
		/// <returns>The calculated price of the ticket as a <see cref="double"/>. Returns 0 if the customer is not a paying customer.</returns>
		public double Calculate(Priority priority, bool isPayingCustomer)
		{
			if (!isPayingCustomer) { return 0; }
			if (priority == Priority.High) { return 100; }
			return 50;
		}
	}
}
