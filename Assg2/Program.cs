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
string[] airlinesLines = File.ReadAllLines("airlines.csv");
for (int i = 1; i < airlinesLines.Length; i++) 
{
    string[] columns = airlinesLines[i].Split(',');
    string name = columns[0];
    string code = columns[1];

    // Create an airline and add it to the dictionary
    Airline airline = new Airline(code, name);
    airlines[code] = airline;
}

// Load Boarding Gates from the CSV file
string[] gatesLines = File.ReadAllLines("boardinggates.csv");
for (int i = 1; i < gatesLines.Length; i++) 
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

// Load Flights from the CSV file
string[] flightsLines = File.ReadAllLines("flights.csv");
for (int i = 1; i < flightsLines.Length; i++) 
{
    string[] columns = flightsLines[i].Split(',');
    string flightNumber = columns[0];
    string origin = columns[1];
    string destination = columns[2];
    DateTime expectedTime = DateTime.Parse(columns[3]);
    string requestCode = columns[4];

    Flight flight = null;

}
