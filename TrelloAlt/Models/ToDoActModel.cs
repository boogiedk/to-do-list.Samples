using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrelloAlt.Models
{
    public class ToDoActModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}