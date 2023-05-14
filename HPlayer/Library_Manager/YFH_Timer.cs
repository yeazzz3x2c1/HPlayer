using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HPlayer.Library_Manager
{
    class YFH_Timer
    {
        class Timer_Tag
        {
            public bool IsRunning = true;
        }
        private Timer_Tag tag = null;
        public event EventHandler Tick;
        public TimeSpan Interval { get; set; } = new TimeSpan(10000);
        public async void Start()
        {
            if (tag != null)
                if (tag.IsRunning)
                    return;
            Timer_Tag new_tag = new Timer_Tag();
            tag = new_tag;
            while (true)
            {
                await Task.Delay(Interval);
                if (!new_tag.IsRunning)
                    break;
                Tick?.Invoke(this, null);
            }
        }
        public void Stop()
        {
            if (tag == null)
                return;
            tag.IsRunning = false;
        }
    }
}
