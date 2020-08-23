using System;
using System.Timers;

namespace CloudShop.WebApp.Services
{
    public class TimerService
    {
        private Timer _timer;
        public event Action OnElapsed;

        public void SetTimer(TimeSpan interval) {
            _timer = new Timer(interval.TotalMilliseconds);
            _timer.Elapsed += NotifyTimerElapsed;
            _timer.Enabled = true;
        }
        
        private void NotifyTimerElapsed(object source, ElapsedEventArgs e) {
            OnElapsed?.Invoke();
            _timer.Dispose();
        }
    }
}