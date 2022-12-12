using System;
using System.Collections.Generic;
using System.Linq;

namespace Collections
{
    public class SocialNetworkUser<TUser> : User, ISocialNetworkUser<TUser>
        where TUser : IUser
    {
        private IDictionary<string, ISet<TUser>> followed; 
        public SocialNetworkUser(string fullName, string username, uint? age) : base(fullName, username, age)
        {
            followed = new Dictionary<string, ISet<TUser>>();
        }

        public bool AddFollowedUser(string group, TUser user)
        {
            if(!followed.Keys.Contains(group)) followed.Add(group, new HashSet<TUser>());
            return   followed[group].Add(user);
        }

        public IList<TUser> FollowedUsers
        {
            get => followed.Values.SelectMany(x => x).ToList();
            
        }

        public ICollection<TUser> GetFollowedUsersInGroup(string group)
        {
           if(followed.ContainsKey(group)) return followed[group].ToList();
           else return new List<TUser>();
        }
    }
}
