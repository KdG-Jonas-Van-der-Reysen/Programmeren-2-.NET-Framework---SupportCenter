using SC.BL.Domain;
using SC.UI.CA.ExtensionMethods;
using System.ComponentModel.DataAnnotations;
namespace SC.UI.CA;
using SC.BL;
class ConsoleUi
{
    private readonly ITicketManager _mgr;
    private bool _quit = false;
    
    public ConsoleUi(ITicketManager ticketManager)
    {
        _mgr = ticketManager;
    }    

    public void Run()
    {
        while (!_quit)
        {
            try
            {
                ShowMenu();
            }
            catch (ValidationException validationException)
            {
                Console.WriteLine(validationException.Message);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Er heeft zich een fout voorgedaan! Probeer opnieuw!");
            }
        }
    }


    private void ShowMenu()
    {
        Console.WriteLine("=================================");
        Console.WriteLine("=== HELPDESK - SUPPORT CENTER ===");
        Console.WriteLine("=================================");
        Console.WriteLine("1) Toon alle tickets");
        Console.WriteLine("2) Details van een ticket");
        Console.WriteLine("3) Toon de antwoorden van een ticket");
        Console.WriteLine("4) Maak een nieuw ticket");
        Console.WriteLine("5) Geef een antwoord op een ticket");
        Console.WriteLine("6) Verwijder een ticket");
        Console.WriteLine("0) Afsluiten");
        DetectMenuAction();
    }

    private void DetectMenuAction()
    {
        bool isInvalidAction;
        do
        {
            isInvalidAction = false;
            Console.Write("Keuze: ");
            string input = Console.ReadLine();
            if (Int32.TryParse(input, out int action))
            {
                switch (action)
                {
                    case 1:
                        ShowAllTickets();
                        break;
                    case 2:
                        ActionShowTicketDetails();
                        break;
                    case 3:
                        ActionShowTicketResponses();
                        break;
                    case 4:
                        ActionCreateTicket(); 
                        break;
                    case 5:
                        ActionAddResponseToTicket();
                        break;
                    case 6:
                        ActionDeleteTicket();
                        break;
                    case 0:
                        _quit = true; return;
                    default:
                        Console.WriteLine("Geen geldige keuze!");
                        isInvalidAction = true;
                        break;
                }
            } else
            {
                Console.WriteLine("Geen geldige keuze!");
                isInvalidAction = true;
            }
        } while (isInvalidAction);
    }

    private void ShowAllTickets()
    {
        foreach (Ticket ticket in _mgr.GetTickets())
        {
            Console.WriteLine(ticket.GetInfo());
        }
    }
    
    private void ActionShowTicketDetails()
    {
        Console.Write("Ticketnummer: ");
        int ticketNumber = Int32.Parse(Console.ReadLine());
        Ticket t = _mgr.GetTicket(ticketNumber);
        ShowTicketDetails(t);
    }
    
    private void ShowTicketDetails(Ticket ticket)
    {
        Console.WriteLine("{0,-15}: {1}", "Ticket", ticket.TicketNumber);
        Console.WriteLine("{0,-15}: {1}", "Gebruiker", ticket.AccountId);
        Console.WriteLine("{0,-15}: {1}", "Datum", ticket.DateOpened.ToString("dd/MM/yyyy"));
        Console.WriteLine("{0,-15}: {1}", "Status", ticket.State);
        if (ticket is HardwareTicket)
            Console.WriteLine("{0,-15}: {1}", "Toestel", ((HardwareTicket)ticket).DeviceName);
        Console.WriteLine("{0,-15}: {1}", "Vraag/probleem", ticket.Text);
    }
    
    private void ActionShowTicketResponses()
    {
        Console.Write("Ticketnummer: ");
        int ticketNumber = Int32.Parse(Console.ReadLine());
        IEnumerable<TicketResponse> responses = _mgr.GetTicketResponses(ticketNumber);
        if (responses != null) ShowTicketResponses(responses);
    }
    private void ShowTicketResponses(IEnumerable<TicketResponse> responses)
    {
        foreach (TicketResponse r in responses)
            Console.WriteLine(r.GetInfo());
    }

    private void ActionCreateTicket()
    {
        string device = "";
        Console.Write("Is het een hardware probleem (j/n)? ");
        bool isHardwareProblem = (Console.ReadLine().ToLower() == "j");
        if (isHardwareProblem)
        {
            Console.Write("Naam van het toestel: ");
            device = Console.ReadLine();
        }
        Console.Write("Gebruikersnummer: ");
        int accountNumber = Int32.Parse(Console.ReadLine());
        Console.Write("Probleem: ");
        string problem = Console.ReadLine();
        if (!isHardwareProblem)
            _mgr.AddTicket(accountNumber, problem);
        else
            _mgr.AddTicket(accountNumber, device, problem);
    }
    
    private void ActionAddResponseToTicket()
    {
        Console.Write("Ticketnummer: ");
        int ticketNumber = Int32.Parse(Console.ReadLine());
        Console.Write("Antwoord: ");
        string response = Console.ReadLine();
        _mgr.AddTicketResponse(ticketNumber, response, false);
    }
    
    private void ActionDeleteTicket()
    {
        Console.Write("Ticketnummer: ");
        int ticketNumber = Int32.Parse(Console.ReadLine());
        _mgr.RemoveTicket(ticketNumber);
        Console.WriteLine("Ticket met nummer '" + ticketNumber + "' is verwijderd!");
    }
    
    
}