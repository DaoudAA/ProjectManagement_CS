using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace WebApplication2.Models
{
    public class Member
    {

        public Member()
        {
        }
        [Key]
        public int ID { get; set; }

        public int Code { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

    }

}
