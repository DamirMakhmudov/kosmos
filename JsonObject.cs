namespace kosmos
{
    public class JsonObject
    {
        public string mode { set; get; }
        public jUser User { get; set; }
    }

    public class jUser
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string patronymic { get; set; }
        public string password { get; set; }
        public string phone{ get; set; }
        public string email{ get; set; }
        public string sysname{ get; set; }
    }
}
