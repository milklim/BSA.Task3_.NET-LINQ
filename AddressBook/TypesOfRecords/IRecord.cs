using System;

namespace AddressBooks
{
    public interface IRecord
    {
        string Address { get; set; }
        DateTime BirthDate { get; set; }
        DateTime TimeAdded { get; set; }
        string City { get; set; }
        string Email { get; set; }
        string FirstName { get; set; }
        Gender Gender { get; set; }
        string LastName { get; set; }
        long PhoneNumber { get; set; }
    }

    public enum Gender
    {
        female = 0,
        male,
        unknown
    }
}
