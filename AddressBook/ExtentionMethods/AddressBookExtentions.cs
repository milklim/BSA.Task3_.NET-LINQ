using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace AddressBooks
{
    public static class AddressBookExtentions
    {
        /// <summary>
        /// The method returns all adult users from Kiev.
        /// </summary>
        /// <param name="users">collection with users</param>
        /// <returns>All adult users from Kiev.</returns>
        public static IEnumerable<IRecord> SelectAdultUsersFromKiev(this AddressBook users)
        {
            foreach (IRecord user in users)
            {   // If date of birth is equal to the default(DateTime), it means BirthDate was not specified when you create the record
                if (user.City == "Kiev" && !user.BirthDate.Equals(default(DateTime)) && user.BirthDate.AddYears(18) <= DateTime.Today)
                    yield return user;
            }
        }

        /// <summary>
        /// The method sends a message to users if they have birthday today
        /// </summary>
        public static void SendMailsIfBirthday(this AddressBook users)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("test-bsa@yandex.ru");
            mail.Subject = "Happy Birthday!";
            mail.Body = "Congratulations!!";

            foreach (IRecord user in users)
            {
                if (user.Email != "n/a" && user.BirthDate.Month == DateTime.Today.Month && user.BirthDate.Day == DateTime.Today.Day && user.BirthDate != default(DateTime))
                {
                    Console.WriteLine("sending e-mail to: {0} {1}", user.LastName, user.FirstName);
                    mail.To.Add(new MailAddress(user.Email));
                } 
            }

            try
            {
                SmtpClient client = new SmtpClient("smtp.yandex.ru", 587);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("test-bsa@yandex.ru", "MegaPass16");
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(mail);
                mail.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Sending of email failed" + ex.Message);
            }
                                
        }

    }
}



