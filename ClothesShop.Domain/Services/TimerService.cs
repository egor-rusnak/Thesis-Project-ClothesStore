using ClothesStore.Domain.Interfaces;
using System.Timers;

namespace ClothesStore.Domain.Services
{
    public class TimerService : ITimerService
    {
        public double Interval { get; set; }
        public event ElapsedEventHandler Elapsed;

        private Timer timer;

        public TimerService()
        {
            timer = new Timer();
            timer.Elapsed += OnElapsed;
        }

        private void OnElapsed(object sender, ElapsedEventArgs e)
            => Elapsed?.Invoke(sender, e);

        public void Dispose()
            => timer.Dispose();

        public void Start()
        {
            timer.Interval = Interval;
            timer.Start();
        }

        public void Stop()
            => timer.Stop();
    }
}
