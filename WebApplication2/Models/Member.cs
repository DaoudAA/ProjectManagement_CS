using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace WebApplication2.Models
{
    public class Member
    {

        public Member()
        {
        }
        public Member(string id)
        {
            UserID = id;
        }
        public Member(string id,string uname)
        {
            UserID = id;
            Name = uname;
        }
        public override int GetHashCode()
        {
            return UserID.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Member))
            {
                return false;
            }

            Member otherMember = (Member)obj;

            return this.UserID == otherMember.UserID;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment

        public int ID { get; set; }
        public string UserID { get; set; }
        public int Code { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }
        public virtual ICollection<Project> Projects { get; set; }

    }

}
