using System.Collections.Generic;

namespace RailWayCorporationApp
{
    public class Carriage : Entity
    {
        public Train Train { get; set; }
        public virtual ICollection<Place> Places { get; set; } 
    }
}