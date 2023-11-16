namespace TeamRotaionActivity.Model
{
    public class ActivityWork
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public IList<Member> Members { get; set; }

        public RotationPeriod RotationPeriod { get; set; }

        public DateTime LastChangeActivity { get; set; }

        public ActivityWork(string name, string description)
        {
            Name = name;
            Description = description;
            Members = new List<Member>();
        }

    }
}
