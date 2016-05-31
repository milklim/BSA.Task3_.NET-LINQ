using System;

namespace AddressBooks
{
    /// <summary>
    /// The class describes the user data that will be contained in instances of the class
    /// </summary>
    class User : IRecord
    {
        public User() { }
        public User(string fname, string lname, string city, string address, long phoneNumber, string email, Gender gender, DateTime birthDate, DateTime timeAdded )
        {
            FirstName = fname;
            LastName = lname;
            City = city;
            Address = (address != "") ? address : "n/a";
            PhoneNumber = phoneNumber;
            Email = (email.Contains("@")) ? email : "n/a";
            Gender = gender;
            BirthDate = birthDate;
            TimeAdded = (timeAdded == default(DateTime)) ? DateTime.Now : timeAdded;
        }

#region  IRecord interface implementation
        private DateTime _birthDate;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public long PhoneNumber { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public DateTime TimeAdded { get; set; }
        public DateTime BirthDate
        {
            get { return _birthDate; }
            set 
            {
                if (value <= DateTime.Now)
                    _birthDate = value;
                else
                    _birthDate = new DateTime();
            }
        }
 #endregion
        public override string ToString()
        {
            // configure output format of phone numbers and date of birth
            string tel = (PhoneNumber != 0) ? PhoneNumber.ToString("(000) 000-00-00") : "n/a";
            string born = (BirthDate.Year == 1) ? "n/a" : BirthDate.ToString("dd/MM/yyyy");

            return string.Format("[{0} {1}, gender:{6}][city:{2}, address:{3}][tel:{4}, e-mail:{5}][born:{7:yyyy}][added:{8:dd/MM/yy HH:mm}]", LastName, FirstName, City, Address, tel, Email, Gender, born, TimeAdded);
        }
    }
}
