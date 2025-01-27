using Assg2;
using static System.Runtime.InteropServices.JavaScript.JSType;

//==========================================================
// Student Number : S10270562D
// Student Name : Ardini Dania
// Partner Number: S10266783H
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

static void DisplayMenu()
{
    Console.WriteLine("==================================================\r\nWelcome to Changi Airport Terminal 5\r\n==================================================\r\n1. List All Flights\r\n2. List Boarding Gates\r\n3. Assign a Boarding Gate to a Flight\r\n4. Create Flight\r\n5. Display Airline Flights\r\n6. Modify Flight Details\r\n7. Display Flight Schedule\r\n0. Exit\r\nPlease select your option:");

}


// Main program loop
while (true)
{
    DisplayMenu(); 
    Console.Write("Enter your option: ");
    string option = Console.ReadLine();

    if (option == "0")
    {
        Console.WriteLine("Goodbye!");
        break;
    }
    else if (option == "1")
    {
        Console.WriteLine("==================================================\r\nList of Flights for Changi Airport Terminal 5\r\n==================================================");
        ListFlightsInformation(flights);
        
    }
    else if (option == "2")
    {
        Console.WriteLine("==================================================\r\nList of Boarding Gates for Changi Airport Terminal 5\r\n==================================================");
        ListBoardingGates(boardingGates);
    }
    else if (option == "3")
    {
        Console.WriteLine("Assigning a boarding gate to a flight...");
        
    }
    else if (option == "4")
    {
        Console.WriteLine("Creating a new flight...");
        
    }
    else if (option == "5")
    {
        Console.WriteLine("=============================================\r\nList of Airlines for Changi Airport Terminal 5\r\n=============================================");
        DisplayFlightDetailsFromAirline(airlines, boardingGates);
    }
    else if (option == "6")
    {
        Console.WriteLine("Modifying flight details...");
        
    }
    else if (option == "7")
    {
        Console.WriteLine("Displaying flight schedule...");
        
    }
    else
    {
        Console.WriteLine("Invalid option, please try again.");
    }

    Console.WriteLine(); 
}
static void PopulateFlightsList(string filePath, List<Flight> flights)
{
    string[] lines = File.ReadAllLines(filePath);

    for (int i = 1; i < lines.Length; i++)
    {
        string[] columns = lines[i].Split(',');

        string flightNumber = columns[0];
        string origin = columns[1];
        string destination = columns[2];
        DateTime expectedTime = DateTime.Parse(columns[3]);
        string specialRequestCode = columns.Length > 4 ? columns[4] : null;
    }
}
static void ListFlightsInformation(List<Flight> flights)
{
    Console.WriteLine("{0,-16} {1,-20} {2,-20} {3,-20} {4,-10}",
        "Flight Number", "Origin", "Destination", "Expected Time", "Status");

    foreach (var flight in flights)
    {
        Console.WriteLine("{0,-16} {1,-20} {2,-20} {3,-20} {4,-10}",
            flight.FlightNumber,
            flight.Origin,
            flight.Destination,
            flight.ExpectedTime,
            flight.Status);
    }
}
static void ListBoardingGates(Dictionary<string, BoardingGate> boardingGates)
{
    Console.WriteLine("{0,-12} {1,-8} {2,-8} {3,-8}", "Gate Name", "DDJB", "CFFT", "LWTT");
    foreach (var gate in boardingGates)
    {
        Console.WriteLine("{0,-12} {1,-8} {2,-8} {3,-8}",
            gate.Value.GateName,
            gate.Value.SupportsDDJB ? "True" : "False",
            gate.Value.SupportsCFFT ? "True" : "False",
            gate.Value.SupportsLWTT ? "True" : "False");
    }
}

static void DisplayFlightDetailsFromAirline(Dictionary<string, Airline> airlines, Dictionary<string, BoardingGate> boardingGates)
{
    // List all airlines
    foreach (var airline in airlines.Values)
    {
        Console.WriteLine($"{airline.Code,-10} {airline.Name}");
    }

    // Prompt user to enter the airline code
    Console.Write("\nEnter Airline Code: ");
    string airlineCode = Console.ReadLine().ToUpper();

    // Check if the airline code exists
    if (airlines.ContainsKey(airlineCode))
    {
        Airline selectedAirline = airlines[airlineCode];

        // Display the list of flights for the selected airline
        Console.WriteLine("\n=============================================");
        Console.WriteLine($"List of Flights for {selectedAirline.Name}");
        Console.WriteLine("=============================================");
        Console.WriteLine("{0,-15} {1,-15} {2,-15} {3,-25}", "Flight Number", "Origin", "Destination", "Expected Time");

        foreach (var flight in selectedAirline.Flights.Values)
        {
            Console.WriteLine("{0,-15} {1,-15} {2,-15} {3,-25:dd/MM/yyyy hh:mm tt}",
                flight.FlightNumber, flight.Origin, flight.Destination, flight.ExpectedTime);
        }

        // Prompt user to select a flight number
        Console.Write("\nEnter Flight Number: ");
        string flightNumber = Console.ReadLine().ToUpper();

        // Check if the flight exists
        if (selectedAirline.Flights.ContainsKey(flightNumber))
        {
            Flight selectedFlight = selectedAirline.Flights[flightNumber];

            // Display the flight details
            Console.WriteLine("\n=============================================");
            Console.WriteLine($"Flight Details for {selectedFlight.FlightNumber}");
            Console.WriteLine("=============================================");
            Console.WriteLine($"Flight Number: {selectedFlight.FlightNumber}");
            Console.WriteLine($"Airline Name: {selectedAirline.Name}");
            Console.WriteLine($"Origin: {selectedFlight.Origin}");
            Console.WriteLine($"Destination: {selectedFlight.Destination}");
            Console.WriteLine($"Expected Departure Time: {selectedFlight.ExpectedTime:dd/MM/yyyy hh:mm tt}");

            // Special Request Code 
            if (selectedFlight is CFFTTFlight cfftt)
            {
                Console.WriteLine("Special Request Code: CFFT");
                Console.WriteLine($"Request Fee: {cfftt.RequestFee:C}");
            }
            else if (selectedFlight is DDJBFlight ddjb)
            {
                Console.WriteLine("Special Request Code: DDJB");
                Console.WriteLine($"Request Fee: {ddjb.RequestFee:C}");
            }
            else if (selectedFlight is LWTTFlight lwtt)
            {
                Console.WriteLine("Special Request Code: LWTT");
                Console.WriteLine($"Request Fee: {lwtt.RequestFee:C}");
            }
            else if (selectedFlight is NORMFlight)
            {
                Console.WriteLine("Special Request Code: NORM");
            }

            // Boarding Gate 
            BoardingGate assignedGate = null;
            foreach (var gate in boardingGates.Values)
            {
                if (gate.Flight == selectedFlight)
                {
                    assignedGate = gate;
                    break;
                }
            }

            if (assignedGate != null)
            {
                Console.WriteLine($"Boarding Gate: {assignedGate.GateName}");
            }
            else
            {
                Console.WriteLine("Boarding Gate: Not Assigned");
            }
        }
        else
        {
            Console.WriteLine("Invalid flight number.");
        }
    }
    else
    {
        Console.WriteLine("Invalid airline code.");
    }
}


