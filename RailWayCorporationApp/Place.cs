namespace RailWayCorporationApp
{
    public class Place : Entity
    {
        public bool IsRent { get; set; } = false;

        public virtual Carriage Carriage { get; set; }
    }
}