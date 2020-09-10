using System;

namespace UDPTester
{
    public static class NetworkSpeedCalculator
    {
        public static double Mbps(int bits, TimeSpan period)
        {
            var seconds = period.TotalMilliseconds / 1000; // ms to seconds

            var packetsPerSecond = 1 / seconds;

            var totalBits = Convert.ToDouble(bits) * Convert.ToInt64(packetsPerSecond);

            var mbps = totalBits / 1000000;

            return mbps;
        }
    }
}