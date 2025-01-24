using Assg2;

//==========================================================
// Student Number : S10270562D
// Student Name : Ardini Dania
// Partner Name : Gerick Yi
//==========================================================

// Create collections to hold airlines, boarding gates, and flights
Dictionary<string, Airline> airlines = new Dictionary<string, Airline>();
Dictionary<string, BoardingGate> boardingGates = new Dictionary<string, BoardingGate>();
Dictionary<string, Flight> flights = new Dictionary<string, Flight>();

// Load Airlines from the CSV file
Console.WriteLine("Loading Airlines...");
string[] airlinesLines = File.ReadAllLines("airlines.csv");
for (int i = 1; i < airlinesLines.Length; i++) // Skip the header line
{
    string[] columns = airlinesLines[i].Split(',');
    string name = columns[0];
    string code = columns[1];

    // Create an airline and add it to the dictionary
    Airline airline = new Airline(code, name);
    airlines[code] = airline;
}
Console.WriteLine($"{airlines.Count} Airlines Loaded!");

// Load Boarding Gates from the CSV file
Console.WriteLine("Loading Boarding Gates...");
string[] gatesLines = File.ReadAllLines("boardinggates.csv");
for (int i = 1; i < gatesLines.Length; i++) // Skip the header line
{
    string[] columns = gatesLines[i].Split(',');
    string gateName = columns[0];
    bool supportsDDJB = bool.Parse(columns[1]);
    bool supportsCFFT = bool.Parse(columns[2]);
    bool supportsLWTT = bool.Parse(columns[3]);

    // Create a boarding gate and add it to the dictionary
    BoardingGate gate = new BoardingGate(gateName, supportsCFFT, supportsDDJB, supportsLWTT);
    boardingGates[gateName] = gate;
}
Console.WriteLine($"{boardingGates.Count} Boarding Gates Loaded!");

// Load Flights from the CSV file
Console.WriteLine("Loading Flights...");
string[] flightsLines = File.ReadAllLines("flights.csv");
for (int i = 1; i < flightsLines.Length; i++) // Skip the header line
{
    string[] columns = flightsLines[i].Split(',');
    string flightNumber = columns[0];
    string origin = columns[1];
    string destination = columns[2];
    DateTime expectedTime = DateTime.Parse(columns[3]);
    string requestCode = columns[4];
}
int flightCount = flightsLines.Length - 1;
Console.WriteLine($"{flightCount} Flights Loaded!");