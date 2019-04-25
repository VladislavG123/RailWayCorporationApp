using System.Collections.Generic;

namespace RailWayCorporationApp
{
    public class Train : Entity
    {
        public string TrainNumber { get; set; }
        public virtual ICollection<Carriage> Carriages { get; set; }
    }
}