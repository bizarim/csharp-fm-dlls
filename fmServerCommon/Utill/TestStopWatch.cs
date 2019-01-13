using System;
using System.Diagnostics;
using fmLibrary;

namespace fmServerCommon
{
    public class TestStopWatch
    {
        public string name = "";
        Stopwatch sw = new Stopwatch();
        TimeSpan ts;

        public void Start()
        {
            sw.Reset();
            sw.Start();
        }

        public void Stop()
        {
            sw.Stop();
            ts = sw.Elapsed;
            Logger.Debug("{0} Time:{1}", name, ts.TotalSeconds);
        }
    }
}
