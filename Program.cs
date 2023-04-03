// See https://aka.ms/new-console-template for more information

using FlightScheduleTest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

const string Montreal = "YUL";
const string Toronto = "YYZ";
const string Calgary = "YYC";
const string Vancouver = "YVR";
const int defaultPlaneCapacity = 20;


var schedules = LoadSchedule().ToList();
var orders = LoadOrders().ToList();

var availableFlights = schedules.Select(x => new ScheduledFlight(defaultPlaneCapacity, x)).ToList();

var queue = new Queue<Order>(orders);
var scheduledFlights = new List<ScheduledFlight>();
while(queue.Any())
{
    var order = queue.Dequeue();

    var flight = GetNextAvailableFlight(order.Destination);
    if(flight is null)
    {
        break;
    }

    flight.Orders.Add(order);
}

foreach(var flight in availableFlights)
{
    foreach(var order in flight.Orders)
    {
        Console.WriteLine($"order: {order.OrderNumber}, flightNumber: {flight.Flight.FlightNumber}, departure: {flight.Flight.Departure}, arrival: {flight.Flight.Arrival}, day: {flight.Flight.Day}");
    }
}
foreach(var order in queue)
{
    Console.WriteLine($"order: {order.OrderNumber}, flightNumber: not scheduled");
}

ScheduledFlight GetNextAvailableFlight(string destination)
{
    return availableFlights.FirstOrDefault(x => x.Flight.Arrival == destination && !x.IsFullyBooked);
}

IEnumerable<Flight> LoadSchedule()
{
    yield return new Flight(1, Montreal, Toronto, 1);
    yield return new Flight(2, Montreal, Calgary, 1);
    yield return new Flight(3, Montreal, Vancouver, 1);

    yield return new Flight(4, Montreal, Toronto, 2);
    yield return new Flight(5, Montreal, Calgary, 2);
    yield return new Flight(6, Montreal, Vancouver, 2);
}

IEnumerable<Order> LoadOrders()
{
    var rawData = File.ReadAllLines("coding-assigment-orders.json");
    var json = JObject.Parse(string.Join("\n", rawData));
    var definition = new { destination = "" };
    var parsedOrders = new List<Order>();
    foreach(var property in json.Properties())
    {
        var id = property.Name;
        var value = (JObject) property.Value;
        var deserialized = JsonConvert.DeserializeAnonymousType(value.ToString(), definition);
        var order = new Order { OrderNumber = id, Destination = deserialized.destination };
        parsedOrders.Add(order);
    }
    return parsedOrders;
}
