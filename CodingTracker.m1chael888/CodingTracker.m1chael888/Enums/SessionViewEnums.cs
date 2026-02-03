using System.ComponentModel;

namespace CodingTracker.m1chael888.Enums
{
    public static class SessionViewEnums
    {
        public enum SessionType
        {
            [Description("Time new session via stopwatch")]
            New,
            [Description("Manually enter past session")]
            Past
        }
    }
}
