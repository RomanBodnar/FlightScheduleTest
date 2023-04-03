namespace FlightScheduleTest;
 
public class ScheduledFlight
{
    private int capacity;

    public ScheduledFlight(int planeCapacity, Flight flight)
    {
        this.capacity = planeCapacity;
        this.Orders = new List<Order>(planeCapacity);
        this.Flight = flight;
    }
    
    public Flight Flight { get; set; }

    public List<Order> Orders { get; }

    public bool IsFullyBooked => this.capacity == this.Orders.Count;

}