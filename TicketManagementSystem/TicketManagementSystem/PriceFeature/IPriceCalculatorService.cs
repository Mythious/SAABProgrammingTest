namespace TicketManagementSystem.PriceFeature
{
	/// <summary>
	/// Defines the contract for calculating the price of a ticket based on its priority 
	/// and whether the customer is a paying customer.
	/// </summary>
	public interface IPriceCalculatorService
	{
		/// <summary>
		/// Calculates the price of a ticket based on its priority and whether the customer is a paying customer.
		/// </summary>
		/// <param name="priority">The priority level of the ticket.</param>
		/// <param name="isPayingCustomer">A boolean indicating whether the customer is a paying customer.</param>
		/// <returns>The calculated price of the ticket as a <see cref="double"/>.</returns>
		double Calculate(Priority priority, bool isPayingCustomer);
	}
}
