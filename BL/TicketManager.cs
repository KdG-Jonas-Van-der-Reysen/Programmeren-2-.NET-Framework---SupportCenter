using SC.BL.Domain;
using SC.DAL;

namespace SC.BL;

public class TicketManager : ITicketManager
{
    // PROPERTIES
    private readonly ITicketRepository _repo;

    // CONSTRUCTOR
    public TicketManager(ITicketRepository ticketRepository)
    {
        _repo = ticketRepository;
    }

    // ADD
    // Method to create a ticket object
    public Ticket AddTicket(int accountId, string question)
    {
        Ticket t = new Ticket()
        {
            AccountId = accountId,
            Text = question,
            DateOpened = DateTime.Now,
            State = TicketState.Open,
        };
        return this.AddTicket(t);
    }

    // Method to create a HardwareTicket object
    public Ticket AddTicket(int accountId, string device, string question)
    {
        Ticket t = new HardwareTicket()
        {
            AccountId = accountId,
            DeviceName = device,
            Text = question,
            DateOpened = DateTime.Now,
            State = TicketState.Open,
        };
        return this.AddTicket(t);
    }

    // Method to add the ticket to the database
    private Ticket AddTicket(Ticket ticket)
    {
        return _repo.CreateTicket(ticket);
    }

    // GET
    public IEnumerable<Ticket> GetTickets()
    {
        return _repo.ReadTickets();
    }

    public Ticket GetTicket(int ticketNumber)
    {
        return _repo.ReadTicket(ticketNumber);
    }

    // CHANGE
    public void ChangeTicket(Ticket ticket)
    {
        _repo.UpdateTicket(ticket);
    }

    // REMOVE
    public void RemoveTicket(int ticketNumber)
    {
        _repo.DeleteTicket(ticketNumber);
    }

    // EXTRAS
    public TicketResponse AddTicketResponse(int ticketNumber, string response, bool isClientResponse)
    {
        Ticket ticketToAddResponseTo = this.GetTicket(ticketNumber);
        if (ticketToAddResponseTo != null)
        {
            // Create response
            TicketResponse newTicketResponse = new TicketResponse();
            newTicketResponse.Date = DateTime.Now;
            newTicketResponse.Text = response;
            newTicketResponse.IsClientResponse = isClientResponse;
            newTicketResponse.Ticket = ticketToAddResponseTo;
            
            // Add response to existing responses of ticket
            var responses = this.GetTicketResponses(ticketNumber);
            if (responses != null)
                ticketToAddResponseTo.Responses = responses.ToList();
            else
                ticketToAddResponseTo.Responses = new List<TicketResponse>();
            ticketToAddResponseTo.Responses.Add(newTicketResponse);

            // Change state of ticket
            if (isClientResponse)
                ticketToAddResponseTo.State = TicketState.ClientAnswer;
            else
                ticketToAddResponseTo.State = TicketState.Answered;
            
            // Save changes to repository
            _repo.CreateTicketResponse(newTicketResponse);
            _repo.UpdateTicket(ticketToAddResponseTo);
            return newTicketResponse;
        }
        else
        {
            throw new ArgumentException("Ticket with number '" + ticketNumber + "' not found!");

        }    
    }
    
    public IEnumerable<TicketResponse> GetTicketResponses(int ticketNumber)
    {
        return _repo.ReadTicketResponsesOfTicket(ticketNumber);
    }
}