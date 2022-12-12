using System;

namespace Collections
{
    public class User : IUser
    {
        public User(string fullName, string username, uint? age)
        {
            if(!(fullName == null) && !(username == null))
            {
                Age = age;
                FullName = fullName;
                Username = username;
            }
        }
        
        public uint? Age { get; }
        
        public string FullName { get; }
        
        public string Username { get; }

        public bool IsAgeDefined => Age.HasValue;

        public override bool Equals(object obj)
        {
            return obj is User user &&
                   Age == user.Age &&
                   FullName == user.FullName &&
                   Username == user.Username;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Age, FullName, Username);
        }

        public override string ToString() => $"{nameof(User)}({nameof(Age)}: {Age}, {nameof(FullName)}: {FullName}, {nameof(Username)}: {Username})";
    }
}
