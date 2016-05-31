using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AddressBooks
{
    public abstract class AddressBook : IEnumerable
    {
        // The list to store records, implementing the IRecord interface
        protected List<IRecord> Records = null;

        public IRecord this[int index]
        {
            get { return Records[index]; }
        }

        // The method adds a new record with the specified parameters in the address book
        public abstract void AddRecord(string fname = "John", string lname = "Doe", string city = "n/a", string address = "n/a", long phoneNumber = 0,
                                       string email = "n/a", Gender gender = Gender.unknown, DateTime birthDate = default(DateTime), DateTime timeAdded = default(DateTime));
        // The method removes the record at the specified index
        public abstract void RemoveRecord(int index);

        // The method removes the first record with the specified name.
        public abstract void RemoveRecord(string name);


        /// <summary>
        ///  The method returns all records with the specified domain of E-mail.
        /// </summary>
        /// <param name="domain">the name of e-mail domain.</param>
        /// <returns>The list of the records with the specified e-mail domain.</returns>
        public IEnumerable<IRecord> SelectRecordsByEmailDomain(string domain)
        {
            if (!domain.Contains(".") || domain == "") // if the domain not contains dot in the name or domain is an empty string
            {
                throw new ArgumentException("Wrong format of domain.");
            }
            var result = Records.Where(rec => rec.Email.Contains(string.Format("@{0}", domain)));
            return result;
        }
        /// <summary>
        /// The method returns the records with the women, added for the last specified number of days.
        /// </summary>
        /// <param name="days">number of days.</param>
        /// <returns>The list of records with the women, added for the last days.</returns>
        public IEnumerable<IRecord> SelectWomenForLastDays(uint days)
        {   //Select all women added in the last "days" days.
            var result = from rec in Records
                         where (rec.Gender == Gender.female) && ( DateTime.Now <= rec.TimeAdded.AddDays(days) )
                         select rec;
            return result;
        }
        /// <summary>
        /// The method returns a records containing contact information (phone number and address) and with a given birth month.
        /// </summary>
        /// <param name="month">the month of birth.</param>
        /// <returns>The list of records containing contact information</returns>
        public IEnumerable<IRecord> SelectAllWithContactsBornInMonth(ushort month = 1)
        {
            var result = Records
                // If the date of birth was not specified when a record is created, it is assigned the default value (01.01.0001), so check the value of the Year != 1
                .Where(rec => (rec.BirthDate.Month == month && rec.BirthDate.Year != 1) && (rec.Address != "n/a") && (rec.PhoneNumber != 0))
                .OrderByDescending(rec => rec.LastName);
            return result;
        }
        /// <summary>
        /// The method returns a dictionary with two keys: "man", "woman" corresponding values: "list of men" and "list of women".
        /// </summary>
        public IDictionary<string, IEnumerable<IRecord>> SelectMenWomen()
        {
            Dictionary<string, IEnumerable<IRecord>> result = new Dictionary<string, IEnumerable<IRecord>>
            {
                {"man", Records.Where(man => man.Gender == Gender.male) },
                {"woman", Records.Where(woman => woman.Gender == Gender.female)}
            };
            return result;
        }
        /// <summary>
        /// The method returns the records that match the specified condition, starting at index 'start' and ending with index 'end' inclusive.
        /// </summary>
        /// <param name="start">start index.</param>
        /// <param name="end">end index.</param>
        /// <param name="condition">condition for query.</param>
        /// <returns>The list of records that match the specified condition.</returns>
        public IEnumerable<IRecord> SelectByCustomCondition(int start, int end, Func<IRecord, bool> condition)
        {
            start = (start <= 0) ? 1 : start; // check the correctness of start index
            var result = (Records
                .Where(condition))
                .Skip(start - 1)
                .Take(end - start + 1);
            return result;
        }
        /// <summary>
        ///  The method returns the number of users who have birthday today in a given city.
        /// </summary>
        /// <param name="city">the name of the city.</param>
        /// <returns>The number of users.</returns>
        public int CountUsersWithBirthdayFromCity(string city)
        {
            int result = (Records
                .Where(rec => (rec.BirthDate.Month == DateTime.Today.Month) && (rec.BirthDate.Day == DateTime.Today.Day) && (rec.City.ToLower() == city.ToLower()) ))
                .Count();
            return result;
        }


        // Implementation of the IEnumerable interface
        public IEnumerator GetEnumerator()
        {
            return Records.GetEnumerator();
        }


    }
}
