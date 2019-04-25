using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailWayCorporationApp
{
    public class Ticket : Entity
    {
        public virtual Way Way { get; set; }

        public virtual Place Place { get; set; }

        public virtual User User { get; set; }
    }
}
