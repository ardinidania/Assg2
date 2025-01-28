using Assg2;
using static System.Runtime.InteropServices.JavaScript.JSType;

//==========================================================
// Student Number : S10270562D
// Student Name : Ardini Dania
// Partner Number: S10266783H
// Partner Name : Gerick Yi
//==========================================================

Dictionary<string, Airline> airlineDictionary = new Dictionary<string, Airline>();
Dictionary<string, BoardingGate> boardingGates = new Dictionary<string, BoardingGate>();
Dictionary<string, Flight> flights = new Dictionary<string, Flight>();

// Load Airlines, Boarding Gates, and Flights
LoadAirlines(airlineDictionary);
LoadBoardingGates(boardingGates);
LoadFlights(flights, airlineDictionary);

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
        ListFlightsInformation(flights, airlineDictionary);
    }
    else if (option == "2")
    {
        ListBoardingGates(boardingGates);
    }
    else if (option == "3")
    {
        AssignBoardingGate(flights, boardingGates);
    }
    else if (option == "4")
    {
        CreateFlight(flights);
    }
    else if (option == "5")
    {
        DisplayFlightDetailsFromAirline(airlineDictionary, boardingGates);
    }
    else if (option == "6")
    {
        ListFlightsForAirline(airlineDictionary);
        ModifyFlightDetails(flights,airlineDictionary);
    }
    else if (option == "7")
    {
        DisplayFlightSchedule(flights);
    }
    else
    {
        Console.WriteLine("Invalid option, please try again.");
    }

    Console.WriteLine();
}
    

    static void DisplayMenu()
{
    Console.WriteLine("");
    Console.WriteLine("==================================================\r\nWelcome to Changi Airport Terminal 5\r\n==================================================\r\n1. List All Flights\r\n2. List Boarding Gates\r\n3. Assign a Boarding Gate to a Flight\r\n4. Create Flight\r\n5. Display Airline Flights\r\n6. Modify Flight Details\r\n7. Display Flight Schedule\r\n0. Exit\r\nPlease select your option:");
}

static void LoadAirlines(Dictionary<string, Airline> airlines)
{
    Console.WriteLine("Loading Airlines...");
    string[] airlinesLines = File.ReadAllLines("airlines.csv");
    for (int i = 1; i < airlinesLines.Length; i++)
    {
        string[] columns = airlinesLines[i].Split(',');
        string name = columns[0];
        string code = columns[1];

        Airline airline = new Airline(code, name);
        airlines[code] = airline;
    }
    Console.WriteLine($"{airlines.Count} Airlines Loaded!");
}

static void LoadBoardingGates(Dictionary<string, BoardingGate> boardingGates)
{
    Console.WriteLine("Loading Boarding Gates...");
    string[] gatesLines = File.ReadAllLines("boardinggates.csv");
    for (int i = 1; i < gatesLines.Length; i++)
    {
        string[] columns = gatesLines[i].Split(',');
        string gateName = columns[0];
        bool supportsDDJB = bool.Parse(columns[1]);
        bool supportsCFFT = bool.Parse(columns[2]);
        bool supportsLWTT = bool.Parse(columns[3]);

        BoardingGate gate = new BoardingGate(gateName, supportsCFFT, supportsDDJB, supportsLWTT);
        boardingGates[gateName] = gate;
    }
    Console.WriteLine($"{boardingGates.Count} Boarding Gates Loaded!");
}

static void LoadFlights(Dictionary<string, Flight> flights, Dictionary<string, Airline> airlineDictionary)
{
    Console.WriteLine("Loading Flights...");
    string[] flightsLines = File.ReadAllLines("flights.csv");
    for (int i = 1; i < flightsLines.Length; i++)
    {
        string[] columns = flightsLines[i].Split(',');
        string flightNumber = columns[0];
        string origin = columns[1];
        string destination = columns[2];
        DateTime expectedTime = DateTime.Parse(columns[3]);
        string requestCode = columns[4];

        Flight newFlight;

        if (requestCode == "DDJB")
        {
            newFlight = new DDJBFlight(flightNumber, origin, destination, expectedTime);
        }
        else if (requestCode == "CFFT")
        {
            newFlight = new CFFTFlight(flightNumber, origin, destination, expectedTime);
        }
        else if (requestCode == "LWTT")
        {
            newFlight = new LWTTFlight(flightNumber, origin, destination, expectedTime);
        }
        else
        {
            newFlight = new NormalFlight(flightNumber, origin, destination, expectedTime);
        }

        flights[flightNumber] = newFlight;

        
        string airlineCode = flightNumber.Substring(0, 2); 
        if (airlineDictionary.ContainsKey(airlineCode))
        {
            Airline airline = airlineDictionary[airlineCode];
            airline.AddFlight(newFlight); 
        }
    }
    Console.WriteLine($"{flights.Count} Flights Loaded!");
}



static void ListFlightsInformation(Dictionary<string, Flight> flights, Dictionary<string, Airline> airlineDictionary)
{
    Console.WriteLine("==================================================");
    Console.WriteLine("List of Flights for Changi Airport Terminal 5");
    Console.WriteLine("==================================================");
    Console.WriteLine("{0,-16} {1,-20} {2,-20} {3,-25} {4,-10}", "Flight Number", "Airline Name", "Origin", "Destination", "Expected Departure/Arrival Time");

    foreach (var flight in flights.Values)
    {
        string airlineCode = flight.FlightNumber.Substring(0, 2);  
        string airlineName = airlineDictionary.ContainsKey(airlineCode) ? airlineDictionary[airlineCode].Name : "Unknown Airline";
        string formattedTime = flight.ExpectedTime.ToString("dd/MM/yyyy hh:mm:ss tt");

        Console.WriteLine("{0,-16} {1,-20} {2,-20} {3,-25} {4,-10}",
            flight.FlightNumber, airlineName, flight.Origin, flight.Destination, formattedTime);
    }

    Console.WriteLine("==================================================");
}


static void ListBoardingGates(Dictionary<string, BoardingGate> boardingGates)
{
    Console.WriteLine("==================================================\r\nList of Boarding Gates for Changi Airport Terminal 5\r\n==================================================");
    Console.WriteLine("{0,-12} {1,-8} {2,-8} {3,-8}", "Gate Name", "DDJB", "CFFT", "LWTT");
    foreach (var gate in boardingGates.Values)
    {
        Console.WriteLine("{0,-12} {1,-8} {2,-8} {3,-8}",
            gate.GateName, gate.SupportsDDJB ? "True" : "False", gate.SupportsCFFT ? "True" : "False", gate.SupportsLWTT ? "True" : "False");
    }
}

static void AssignBoardingGate(Dictionary<string, Flight> flights, Dictionary<string, BoardingGate> boardingGates)
{
    Console.WriteLine("==================================================");
    Console.WriteLine("Assigning a Boarding Gate to a Flight");
    Console.WriteLine("==================================================");

    Console.WriteLine("Enter Flight Number:");
    string flightNumber = Console.ReadLine().ToUpper();

    if (flights.ContainsKey(flightNumber))
    {
        Flight selectedFlight = flights[flightNumber];
        Console.WriteLine($"Available Boarding Gates:");
        ListBoardingGates(boardingGates);

        Console.WriteLine("Enter Boarding Gate Name:");
        string gateName = Console.ReadLine().ToUpper();

        if (boardingGates.ContainsKey(gateName))
        {
            BoardingGate selectedGate = boardingGates[gateName];

            if (selectedGate.Flight == null)
            {
                selectedGate.Flight = selectedFlight;
                Console.WriteLine($"Flight {flightNumber} has been assigned to gate {gateName}.");
            }
            else
            {
                Console.WriteLine($"Gate {gateName} is already assigned to another flight.");
            }
        }
        else
        {
            Console.WriteLine("Invalid gate name.");
        }
    }
    else
    {
        Console.WriteLine("Invalid flight number.");
    }
}

static void CreateFlight(Dictionary<string, Flight> flights)
{

    Console.WriteLine("Enter Flight Number:");
    string flightNumber = Console.ReadLine().ToUpper();

    if (!flights.ContainsKey(flightNumber))
    {
        Console.WriteLine("Enter Origin:");
        string origin = Console.ReadLine();
        Console.WriteLine("Enter Destination:");
        string destination = Console.ReadLine();
        Console.WriteLine("Enter Expected Departure Time (YYYY-MM-DD HH:MM):");
        string timeInput = Console.ReadLine();

        if (DateTime.TryParse(timeInput, out DateTime expectedTime))
        {
            Console.WriteLine("Enter Special Request Code (NORM, DDJB, CFFT, LWTT):");
            string requestCode = Console.ReadLine().ToUpper();

            Flight newFlight;

            if (requestCode == "DDJB")
            {
                newFlight = new DDJBFlight(flightNumber, origin, destination, expectedTime);
            }
            else if (requestCode == "CFFT")
            {
                newFlight = new CFFTFlight(flightNumber, origin, destination, expectedTime);
            }
            else if (requestCode == "LWTT")
            {
                newFlight = new LWTTFlight(flightNumber, origin, destination, expectedTime);
            }
            else
            {
                newFlight = new NormalFlight(flightNumber, origin, destination, expectedTime);
            }

            flights[flightNumber] = newFlight;


            Console.WriteLine($"Flight {flightNumber} successfully created!");
        }
        else
        {
            Console.WriteLine("Invalid date format.");
        }
    }
    else
    {
        Console.WriteLine("Flight number already exists.");
    }
}

static void DisplayFlightDetailsFromAirline(Dictionary<string, Airline> airlines, Dictionary<string, BoardingGate> boardingGates)
{
    Console.WriteLine("==================================================");
    Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
    Console.WriteLine("==================================================");

    if (airlines.Count == 0)
    {
        Console.WriteLine("No airlines available.");
        return;
    }

    foreach (var airline in airlines.Values)
    {
        Console.WriteLine($"{airline.Code,-10} {airline.Name}");
    }

    Console.Write("\nEnter Airline Code: ");
    string airlineCode = Console.ReadLine().ToUpper();

    if (airlines.ContainsKey(airlineCode))
    {
        Airline selectedAirline = airlines[airlineCode];

        Console.WriteLine("\n=============================================");
        Console.WriteLine($"List of Flights for {selectedAirline.Name}");
        Console.WriteLine("=============================================");
        Console.WriteLine("{0,-15} {1,-15} {2,-15} {3,-25}", "Flight Number", "Origin", "Destination", "Expected Time");

        if (selectedAirline.Flights.Count == 0)
        {
            Console.WriteLine("No flights available for this airline.");
        }
        else
        {
            foreach (var flight in selectedAirline.Flights.Values)
            {
                Console.WriteLine("{0,-15} {1,-15} {2,-15} {3,-25:dd/MM/yyyy hh:mm tt}",
                    flight.FlightNumber, flight.Origin, flight.Destination, flight.ExpectedTime);
            }

            Console.Write("\nEnter Flight Number: ");
            string flightNumber = Console.ReadLine().ToUpper();

            if (selectedAirline.Flights.ContainsKey(flightNumber))
            {
                Flight selectedFlight = selectedAirline.Flights[flightNumber];

                Console.WriteLine("\n=============================================");
                Console.WriteLine($"Flight Details for {selectedFlight.FlightNumber}");
                Console.WriteLine("=============================================");
                Console.WriteLine($"Flight Number: {selectedFlight.FlightNumber}");
                Console.WriteLine($"Airline Name: {selectedAirline.Name}");
                Console.WriteLine($"Origin: {selectedFlight.Origin}");
                Console.WriteLine($"Destination: {selectedFlight.Destination}");
                Console.WriteLine($"Expected Departure Time: {selectedFlight.ExpectedTime:dd/MM/yyyy hh:mm tt}");

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
    }
    else
    {
        Console.WriteLine("Invalid airline code.");
    }
}



// For the Modify Section
static void ListFlightsForAirline(Dictionary<string, Airline> airlines)
{
    Console.WriteLine("==================================================");
    Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
    Console.WriteLine("==================================================");

    if (airlines.Count == 0)
    {
        Console.WriteLine("No airlines available.");
        return;
    }

    foreach (var airline in airlines.Values)
    {
        Console.WriteLine($"{airline.Code,-10} {airline.Name}");
    }

    Console.Write("\nEnter Airline Code: ");
    string airlineCode = Console.ReadLine().ToUpper();

    if (airlines.ContainsKey(airlineCode))
    {
        Airline selectedAirline = airlines[airlineCode];

        Console.WriteLine("\n=============================================");
        Console.WriteLine($"List of Flights for {selectedAirline.Name}");
        Console.WriteLine("=============================================");
        Console.WriteLine("{0,-15} {1,-15} {2,-15} {3,-25}", "Flight Number", "Origin", "Destination", "Expected Time");

        if (selectedAirline.Flights.Count == 0)
        {
            Console.WriteLine("No flights available for this airline.");
        }
        else
        {
            foreach (var flight in selectedAirline.Flights.Values)
            {
                Console.WriteLine("{0,-15} {1,-15} {2,-15} {3,-25:dd/MM/yyyy hh:mm tt}",
                    flight.FlightNumber, flight.Origin, flight.Destination, flight.ExpectedTime);
            }
        }
    }
    else
    {
        Console.WriteLine("Invalid airline code.");
    }
}


static void ModifyFlightDetails(Dictionary<string, Flight> flights, Dictionary<string, Airline> airlines)
{

    Console.WriteLine("==================================================");
    Console.WriteLine("Modify Flight Details");
    Console.WriteLine("==================================================");

    Console.WriteLine("Enter Flight Number:");
    string flightNumber = Console.ReadLine().ToUpper();

    if (flights.ContainsKey(flightNumber))
    {
        Flight selectedFlight = flights[flightNumber];
        Airline flightAirline = airlines[selectedFlight.FlightNumber.Substring(0, 2)]; 

        Console.WriteLine("What would you like to modify?");
        Console.WriteLine("1. Modify Basic Information");
        Console.WriteLine("2. Modify Status");
        Console.WriteLine("3. Modify Special Request Code");
        Console.WriteLine("4. Modify Boarding Gate");
        Console.Write("Enter your choice: ");
        string modifyOption = Console.ReadLine();

        if (modifyOption == "1")
        {
            Console.WriteLine("1. Modify Origin");
            Console.WriteLine("2. Modify Destination");
            Console.WriteLine("3. Modify Expected Time");
            Console.Write("Enter your choice: ");
            string basicOption = Console.ReadLine();

            if (basicOption == "1")
            {
                Console.Write("Enter new Origin: ");
                selectedFlight.Origin = Console.ReadLine();
            }
            else if (basicOption == "2")
            {
                Console.Write("Enter new Destination: ");
                selectedFlight.Destination = Console.ReadLine();
            }
            else if (basicOption == "3")
            {
                Console.Write("Enter new Expected Departure/Arrival Time (dd/mm/yyyy hh:mm): ");
                string timeInput = Console.ReadLine();
                if (DateTime.TryParseExact(timeInput, "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime newTime))
                {
                    selectedFlight.ExpectedTime = newTime;
                }
                else
                {
                    Console.WriteLine("Invalid time format.");
                }
            }
        }

      
        Console.WriteLine("Flight updated!");
        Console.WriteLine("Flight Number: " + selectedFlight.FlightNumber);
        Console.WriteLine("Airline Name: " + flightAirline.Name);  
        Console.WriteLine("Origin: " + selectedFlight.Origin);
        Console.WriteLine("Destination: " + selectedFlight.Destination);
        Console.WriteLine("Expected Departure/Arrival Time: " + selectedFlight.ExpectedTime.ToString("dd/MM/yyyy h:mm tt"));
        Console.WriteLine("Status: " + selectedFlight.Status);
        Console.WriteLine("Special Request Code: " + selectedFlight.SpecialRequestCode);  
        Console.WriteLine("Boarding Gate: " + selectedFlight.BoardingGate);  
    }
    else
    {
        Console.WriteLine("Flight not found.");
    }
}


static void DisplayFlightSchedule(Dictionary<string, Flight> flights)
{
    Console.WriteLine("==================================================");
    Console.WriteLine("Flight Schedule for Changi Airport Terminal 5");
    Console.WriteLine("==================================================");

    foreach (var flight in flights.Values.OrderBy(f => f.ExpectedTime))
    {
        Console.WriteLine($"{flight.FlightNumber,-16} {flight.Origin,-20} {flight.Destination,-20} {flight.ExpectedTime,-20} {flight.Status}");
    }
}




