using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Timers;

namespace AddressBooks
{
    /// <summary>
    /// An instance of a class sends e-mails at the specified time (hours:minutes) every day to all users who have birthday today.
    /// </summary>
    public class BirthDayNotifier
    {
        private Timer _timer = new Timer();
        private DateTime _timeToAlarm;
        private Action SendMessages;

        /// <summary>
        /// Constuctor
        /// </summary>
        /// <param name="doIt">method for execution</param>
        /// <param name="hours">time for sending</param>
        /// <param name="minutes">time for sending</param>
        public BirthDayNotifier(Action doIt, uint hours, uint minutes)
        {
            _timer.Elapsed += SendMail;
            SendMessages = doIt;
            _timeToAlarm = DateTime.Now.Date.AddHours(hours).AddMinutes(minutes);
            if (_timeToAlarm < DateTime.Now)
            {
                SendMessages.Invoke();
            }
            SetTimer();
        }


        private void SetTimer()
        {
            _timer.Stop();
            if (_timeToAlarm < DateTime.Now)
                {
                    _timeToAlarm = _timeToAlarm.AddDays(1);
                }
            _timer.Interval = (int)(_timeToAlarm - DateTime.Now).TotalMilliseconds;
            _timer.Start();
        }
        private void SendMail(Object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                SendMessages.Invoke();
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            SetTimer();
        }
    }
}
