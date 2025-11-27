using TicketManagementSystem.UserFeature.Models;

namespace TicketManagementSystem.UserFeature.Services
{
	/// <summary>
	/// Defines the contract for user-related operations and services.
	/// </summary>
	/// <remarks>
	/// This interface provides methods for retrieving user information, including specific users
	/// and account managers. Implementations of this interface should handle user data retrieval
	/// and related exceptions.
	/// </remarks>
	public interface IUserService
	{
		User GetUser(string username);

		User GetAccountManager();
	}
}
