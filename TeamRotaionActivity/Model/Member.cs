using System;

namespace TeamRotaionActivity.Model
{
    public class Member
    {
        public Guid Id { get; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public Member(Guid id, string name, string lastName)
        {
            Id = id;
            Name = name;
            LastName = lastName;
        }

        public static Member CreateNew(string name, string LastName)
        {
            return new Member(Guid.NewGuid(), name, LastName);
        }

    }
}
