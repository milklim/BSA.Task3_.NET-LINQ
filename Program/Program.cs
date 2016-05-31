using System;
using AddressBooks;

namespace Program
{
    class Program
    {
                    
        public delegate void NotifierDelegate();
        static void Main()
        {
            AddressBook Users = new AddressBookWithUsers();
            
     // Configure logger: (by default - output to console)
            //Logger.Source = new LogToFile();

            Users.AddRecord("Sonya", "Red", "Kiev", "Nova str. 4/45", 0508742499, "s_red@gmail.com", Gender.female, new DateTime(1988, 07, 18), timeAdded: new DateTime(2016, 05, 29));
            Users.AddRecord("Liza", "Bart", "New York", "Main str. 111", 95621478563, "l_bart@outlook.com", Gender.female, new DateTime(1995, 01, 12), new DateTime(2016, 05, 28));
            Users.AddRecord("Marry", "Poppins", "London", "Bridge str. 4", 0563264747, gender: Gender.female, birthDate: new DateTime(1968, 05, 14), timeAdded: new DateTime(2016, 05, 27));
            Users.AddRecord("Sergey", "Torshin", "Kiev", "Soborna str. 34/6", 0668749562, "storshin@gmail.com", Gender.male, new DateTime(1965, 04, 12));
            Users.AddRecord("Alexa", "Fox", "Berlin", email:"a_fox@yahoo.com", phoneNumber:45789652120, gender: Gender.female);
            Users.AddRecord("Maria", "Taranova", "Lviv", "Shevchenko str. 43", 0681234587, "m_taran@ukr.net", Gender.female, new DateTime(1990, 01, 12));
            Users.AddRecord("Vasiliy", "Pupkin", "Zhitomir", phoneNumber: 0990484566, gender: Gender.male, birthDate: new DateTime(1980, 05, 28));
            Users.AddRecord("Vladimir", "Klimenko", "Pavlograd", phoneNumber: 0507757568, email: "klimbox@gmail.com", gender: Gender.male, birthDate: DateTime.Today);
            Users.AddRecord("Sherlock", "Holms", "London", "Baker str.", 0985647531, "eioo@git.ua", Gender.male, birthDate: new DateTime(1987, 05, 31));
            Users.AddRecord("Artem", "Taran", "Lutsk", "Kravchenko str.", 0935896421, "art_zhuk@yandex.ua", Gender.male, new DateTime(1987, 1, 17));
            Users.AddRecord("Rob", "Vatson", gender: Gender.male);
            Users.AddRecord("Ivan", "Semenov", "Rovno", phoneNumber: 0990375566, gender: Gender.male, birthDate: new DateTime(1988, 05, 28));


            Console.WriteLine("\n\t\t === SelectRecordsByEmailDomain(\"WRONG DOMAIN\") ===");
            try 
            {
                foreach (var item in Users.SelectRecordsByEmailDomain(""))
                    Console.WriteLine(item);
            }
            catch (ArgumentException ex) 
            {
                Console.WriteLine(ex.Message); 
            }

            Console.WriteLine("\n\t\t === SelectRecordsByEmailDomain(\"gmail.com\") ===");
            foreach (var item in Users.SelectRecordsByEmailDomain("gmail.com"))
                Console.WriteLine(" > > {0}",item);

            Console.WriteLine("\n\t\t === SelectWomenForLastDays(10) ===");
            foreach (var item in Users.SelectWomenForLastDays(10))
                Console.WriteLine(" > > {0}", item);

            Console.WriteLine("\n\t\t === SelectAllWithContactsBornInMonth(1) ===");
            foreach (var item in Users.SelectAllWithContactsBornInMonth(1))
                Console.WriteLine(" > > {0}", item);

            Console.WriteLine("\n\t\t === SelectMenWomen() ===");
            var menWomenDict = Users.SelectMenWomen();
            foreach (var gender in menWomenDict.Keys)
            {
                Console.WriteLine("{0}:", gender);
                foreach (var user in menWomenDict[gender])
                    Console.WriteLine(" > > {0}", user);
            }

            Console.WriteLine("\n\t\t === SelectByCustomCondition(2, 6, rec => rec == rec) ===");
            foreach (var item in Users.SelectByCustomCondition(2, 6, rec => rec == rec))
                Console.WriteLine(" > > {0}", item);

            Console.WriteLine("\n\t\t === SelectByCustomCondition(4, 7, rec => rec.Gender == Gender.male) ===");
            foreach (var item in Users.SelectByCustomCondition(4, 7, rec => rec.Gender == Gender.male))
                Console.WriteLine(" > > {0}", item);

            Console.WriteLine("\n\t\t === CountUsersWithBirthdayFromCity(\"London\") ===");
            Console.WriteLine(" > > {0} user(s).", Users.CountUsersWithBirthdayFromCity("London"));

            Console.WriteLine("\n\t\t === SelectAdultUsersFromKiev() === [extention method]");
            foreach (var item in Users.SelectAdultUsersFromKiev())
                Console.WriteLine(" > > {0}", item);


            Console.WriteLine("\n\t\t === An example of deferred execution === ");
            Console.WriteLine("\t SelectRecordsByEmailDomain(\"gmail.com\"):");
            foreach (var item in Users.SelectRecordsByEmailDomain("gmail.com"))
                Console.WriteLine("1st query: {0}", item);

            Console.WriteLine("\n\t Change the collection:");
            Users.RemoveRecord("Red Sonya");
            Users.AddRecord(email:"john@gmail.com");
            Users.AddRecord(email:"mymail@gmail.com");

            Console.WriteLine("\n\t SelectRecordsByEmailDomain(\"gmail.com\"):");
            foreach (var item in Users.SelectRecordsByEmailDomain("gmail.com"))
                Console.WriteLine("2nd query: {0}", item);

            Console.WriteLine("\n\t test Notifier:");
            BirthDayNotifier notifier = new BirthDayNotifier(Users.SendMailsIfBirthday, 8, 30); // 8:30 - time for sending e-mails
            
            Console.ReadKey();
        }
    }
}
