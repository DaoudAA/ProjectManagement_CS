using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Task
    {

        public Task()
        {
        }
        [Key]
        public int ID { get; set; }
        public string UserId { get; set; }  

        public int Code { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public virtual Project Project { get; set; }
        /*public void CreateTask()
        {
            // TODO implement here
        }

        public void UpdateTask()
        {
            // TODO implement here
        }

        public void DeleteTask()
        {
            // TODO implement here
        }

        public void ListTasks()
        {
            // TODO implement here
        }
        */
    }

}