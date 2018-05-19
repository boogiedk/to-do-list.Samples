using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrelloAlt.Models
{
    public class ActListModel
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public virtual ApplicationUser User { get; set; }
        public IEnumerable<ToDoActModel> ActList { get; set; }
    }
}