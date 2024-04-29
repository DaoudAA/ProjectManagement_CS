using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Project
    {
        public Project()
        {
        }
        [Key]
        public int ID { get; set; }

        public int Code { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public ProjectManager Manager { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<Member> Members { get; set; }

        /*public void CreateProject()
        {
            // TODO implement here
        }

        public void UpdateProject()
        {
            // TODO implement here
        }

        public void DeleteProject()
        {
            // TODO implement here
        }

        public void ListProjects()
        {
            // TODO implement here
        }

        public void AddTaskToProject()
        {
            // TODO implement here
        }

        public void RemoveTaskFromProject()
        {
            // TODO implement here
        }

        public void AddMember()
        {
            // TODO implement here
        }

        public void RemoveMember()
        {
            // TODO implement here
        }*/
    }
}