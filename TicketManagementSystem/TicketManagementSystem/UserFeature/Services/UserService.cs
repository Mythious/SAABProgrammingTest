using TicketManagementSystem.UserFeature.Exceptions;

namespace TicketManagementSystem.UserFeature.Services
{
	/// <summary>
	/// Provides services for managing and retrieving user-related data.
	/// </summary>
	/// <remarks>
	/// This class implements the <see cref="IUserService"/> interface and provides methods
	/// to retrieve user information, including specific users and account managers.
	/// </remarks>
	public class UserService : IUserService
	{
		/// <summary>
		/// Retrieves a user by their username.
		/// </summary>
		/// <param name="username">The username of the user to retrieve.</param>
		/// <returns>The <see cref="User"/> object corresponding to the specified username.</returns>
		/// <exception cref="UnknownUserException">
		/// Thrown when a user with the specified username cannot be found.
		/// </exception>
		public User GetUser(string username)
		{
			using var repo = new UserRepository();

			var foundUser = repo.GetUser(username);

			if (foundUser == null)
			{
				throw new UnknownUserException($"User {username} not found");
			}

			return foundUser;
		}

		/// <summary>
		/// Retrieves the account manager user.
		/// </summary>
		/// <returns>The <see cref="User"/> object representing the account manager.</returns>
		/// <exception cref="UnknownUserException">
		/// Thrown when the account manager cannot be found.
		/// </exception>
		public User GetAccountManager()
		{
			using var repo = new UserRepository();

			var foundAccountManager = repo.GetAccountManager();

			if (foundAccountManager == null)
			{
				throw new UnknownUserException("Account manager could not be found.");
			}

			return foundAccountManager;
		}
	}
}
