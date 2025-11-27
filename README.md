# Original Refactoring Test

## Assignment
You work within a team and are assigned a ticket created by the product owner as part of your sprint work.
The ticket says:

---

### Refactor Ticket Service
The ticket service functionally works well and the function and logic of it must not change. However it requires refactoring to improve readability. When refactoring, focus on applying clean code principles. The final result should be something ready to merge into the next release of the product and meets the definition of done. You are free to define the definition of done for yourself.
Keep in mind principles such as **SOLID**, **KISS**, **DRY** and **YAGNI**.

#### Limitations
Due to dependencies in other areas of the larger product, there are a few limitations that must be followed during the refactoring:

1. The contents of Program.cs cannot change at all including using statements.
2. The EmailService project cannot be changed
3. The method signatures in the repositories cannot change
4. The TicketRepository must remain static

---

Please submit your refactored code to us via a method of your choice (repository link, cloud share, zip file, etc.) to allow us to review it.
Note that while you can clone this repository, you cannot create a branch or commit any code to the repository. It is read only.

Try to limit time spent on this exercise to a maximum of 3 hours. If there is anything you don't have time to complete, write it down and include it with your submission.

Good Luck!

# Summary Of Changes
- Changed TicketService - CreateTicket parameter names to improve readability
- Moved the ticket validation into a rules based system defined by a interfaced IValidate<T>. Description and Title of the ticket are now handled through IoC.
- Moved the user repository communication (GetUser and GetAccountManager) to a singular service to abide by single use principle. Likewise, attached interface for IoC and dependency injection.
- Moved the GetAccountManager logic into a single purpose method to handle with determination of a paying customer or not for a valid object return.
- Removed WriteTicketToFile from the TicketService.cs class due to irrelevancy and unused status to abide by YAGNI
- Implemented a rules based system using a abstract base and inherited rules to allow extensibility without modifying the original service classes. This logic is removed from the TicketService to adhere to SOLID.
- Implemented a service and interface setup for the communication with the email project. This now instead takes the interface definition that would be established through dependency injection to facilitate the request for emailing the ticket adminsitrators.
- Implemented separation of concerns for the ticket price through implementation of PriceCalculatorService and removed the responsibility from TicketService.cs
- Adjusted 'UnknownUserException' and 'InvalidTicketException' to be housed within their respective feature folders.
- Adjusted the 'User' and 'Ticket' models to be housed within their respective feature folders.
- Adjusted the 'UserRepository' and 'TicketRepository' into their respective feature folders.

# Future Improvements
- If time and test limitations were not a concern, the keywords defined within PriorityKeywordRule would be defined as configuration parameters (I.e json file for example) to allow extensibility and prevent modification.
- Similarly, the rules defined within the PriorityCalculator.cs would ideally be defined either through configuration or via the dependency injection bootstrap to facilitate extensibility and abide by solid.
- Due to restrictions on the namespace placement for TicketService in respect to Program.cs, this has remained at the directory root to ensure that the namespaces do not change.
- Due to restrictions on the TicketRepository class, it is not possible to assign or prepare the class for dependency injection if it is statically defined and thus uninstantiable.
- Due to the restrictions on the Program.cs file, dependency injection could not be reasonably applied in a sensible implementation. As such, concretions were provided where applicable within the test.
- Similarly to the above, unit tests were not provided due to the basis of pre-existing implementations of environmental details (i.e. SqlConnection) that could not be sufficiently mocked.