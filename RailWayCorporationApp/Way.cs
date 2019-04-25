using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailWayCorporationApp
{
    public class Way : Entity
    {
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }

        public virtual Train Train { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
