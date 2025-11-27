namespace TicketManagementSystem.Core.Validators
{
	/// <summary>
	/// Defines a generic mechanism for validating objects of a specified type.
	/// </summary>
	/// <typeparam name="T">The type of object to be validated.</typeparam>
	public interface IValidator<T>
	{
		void Validate(T value);
	}
}
