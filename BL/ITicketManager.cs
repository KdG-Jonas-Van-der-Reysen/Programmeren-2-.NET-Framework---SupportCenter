namespace SC.BL;
using SC.BL.Domain;
public interface ITicketManager
{
    // ADD
    Ticket AddTicket(int accountId, string question);
    Ticket AddTicket(int accountId, string device, string problem);
    
    // GET
    IEnumerable<Ticket> GetTickets();
    Ticket GetTicket(int ticketNumber);
    
    // CHANGE
    void ChangeTicket(Ticket ticket);
    
    // REMOVE
    void RemoveTicket(int ticketNumber);
    
    // EXTRAS
    IEnumerable<TicketResponse> GetTicketResponses(int ticketNumber);
    TicketResponse AddTicketResponse(int ticketNumber, string response, bool isClientResponse);
}