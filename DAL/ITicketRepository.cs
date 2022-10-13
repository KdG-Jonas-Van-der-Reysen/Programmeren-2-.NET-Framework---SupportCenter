using SC.BL.Domain;
namespace SC.DAL;

public interface ITicketRepository
{
    // CREATE
    public Ticket CreateTicket(Ticket ticket);
    
    // READ
    public IEnumerable<Ticket> ReadTickets();
    Ticket ReadTicket(int ticketNumber);
    
    // UPDATE
    void UpdateTicket(Ticket ticket);
    
    // DELETE
    void DeleteTicket(int ticketNumber);
    
    // EXTRAS
    TicketResponse CreateTicketResponse(TicketResponse response);
    IEnumerable<TicketResponse> ReadTicketResponsesOfTicket(int ticketNumber);
    
}