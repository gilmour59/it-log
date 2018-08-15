using System;

namespace IT_Log.Model
{
    public class it_log
    {
        public int id { get; internal set; }
        public string Name { get; internal set; }
        public string Office { get; internal set; }
        public DateTime? Date { get; internal set; }
        public TimeSpan? Time { get; internal set; }
        public string Service_Request { get; internal set; }
        public int IT_Personnel_id { get; internal set; }
    }
}
