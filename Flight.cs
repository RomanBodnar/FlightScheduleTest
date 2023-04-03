namespace FlightScheduleTest;

public class Flight
{
    public Flight(int flight, string from, string to, int day)
    {
        this.FlightNumber = flight;
        this.Departure = from;
        this.Arrival = to;
        this.Day = day;
    }
    public int FlightNumber { get;set; }

    public string Departure { get; set; }

    public string Arrival { get; set; }

    public int Day { get; set; }
}