# BSA 16. .NET - Best Practices

Create three projects, which must contain the following logic:

1. Logger - is a class that has methods Info, Debug, Warning, Error. Logger must be able to write in different sources (Console, File, etc).
2. AddressBook - is a class that stores a list of users (User). The user list should not be accessible outside the class. AddressBook should have the methods AddUser, RemoveUser and event UserAdded, UserRemoved.
3. Program - is a class for testing the first two libraries. To test the AddressBook class in the Program class to call the methods add, remove). Must be subscribe to events of the AddressBook class.

User: [LastName, FirstName, Birthdate, TimeAdded, City, Address, PhoneNumber, Gender, Email].
