using System;
using System.Collections.Generic;
using LogInfo;

namespace AddressBooks
{
    public class AddressBookWithUsers : AddressBook
    {
        public AddressBookWithUsers()
        {
            Records = new List<IRecord>();
            // the event subscription
            UserAdded += Logger.Instance.Info;
            UserRemoved += Logger.Instance.Info;
            OnWarning += Logger.Instance.Warning;
            OnDebug += Logger.Instance.Debug;
            OnError += Logger.Instance.Error;
        }

        public delegate void AddLogDelegate(string s);
        public delegate void RemoveLogDelegate(string s);
        public delegate void WarningDelegate(string s);
        public delegate void DebugDelegate(string s);
        public delegate void ErrorDelegate(string s);
        public event AddLogDelegate UserAdded;
        public event RemoveLogDelegate UserRemoved;
        public event WarningDelegate OnWarning;
        public event DebugDelegate OnDebug;
        public event ErrorDelegate OnError;

#region Implementation of the abstract methods of the AddressBook class
        public override void AddRecord(string fname, string lname, string city, string address, long phoneNumber, string email, Gender gender, DateTime birthDate, DateTime timeAdded)
        {
            try
            {
                IRecord newUser = new User(fname, lname, city, address, phoneNumber, email, gender, birthDate, timeAdded);
                string message = newUser.ToString();
                Records.Add(newUser);
                UserAdded("User added: " + message);
            }
            catch (Exception ex)
            {
                OnError(string.Format("Failed to create new entry. (Args: {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}). -- {8}", fname, lname, city, address, phoneNumber, email, gender, birthDate, ex.StackTrace));
            }
        }
        public override void RemoveRecord(int index)
        {
            try
            {
                string message = Records[index].ToString();
                Records.RemoveAt(index);
                UserRemoved(string.Format("User removed: {0}", message));
            }
            catch (ArgumentOutOfRangeException ex)
            {
                OnDebug(string.Format("Failed to remove the entry at index {0}. -- {1}", index, ex.StackTrace));
            }

        }
        public override void RemoveRecord(string name)
        {
            string[] fullName = name.Split(' ');
            bool removed = false;
            switch (fullName.Length)
            {
               case 1:
                   foreach (IRecord user in Records)
	               {
                       if (user.LastName.ToLower() == fullName[0].ToLower())
                       {
                           RemoveRecord(Records.IndexOf(user));
                           removed = true;
                           break;
                       }
	               }
                   break;
                case 2:
                   foreach (IRecord user in Records)
	               {
                       if (user.LastName.ToLower() == fullName[0].ToLower())
                       {
                           if (user.FirstName.ToLower() == fullName[1].ToLower())
                           {
                               RemoveRecord(Records.IndexOf(user));
                               removed = true;
                               break;
                           }
                       }
	               }
                   break;
            }

            if (!removed)
                OnWarning(string.Format("Failed to remove the entry by name. (name: {0})", name));
        }
#endregion

    }
}
