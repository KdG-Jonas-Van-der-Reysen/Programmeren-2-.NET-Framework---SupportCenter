using System.Collections.Generic;
using System.Linq;
using SC.BL.Domain;

namespace SC.DAL;

public class TicketRepositoryHC : ITicketRepository
{
    private static readonly List<Ticket> _tickets = new List<Ticket>();
    private static readonly List<TicketResponse> _responses = new List<TicketResponse>();

    // CREATE
    public Ticket CreateTicket(Ticket ticket)
    {
        ticket.TicketNumber = _tickets.Max(t => t.TicketNumber) + 1;
        _tickets.Add(ticket);
        return ticket;
    }
    
    // READ
    public IEnumerable<Ticket> ReadTickets()
    {
        return _tickets;
    }

    public Ticket ReadTicket(int ticketNumber)
    {
        return _tickets.Find(t => t.TicketNumber == ticketNumber);
    }
    
    // UPDATE
    public void UpdateTicket(Ticket ticket)
    {
        // Do nothing! All data lives in memory, so everything references the same objects!!
    }
    
    // DELETE
    public void DeleteTicket(int ticketNumber)
    {
        _responses.RemoveAll(r => r.Ticket.TicketNumber == ticketNumber);
        _tickets.Remove(ReadTicket(ticketNumber));
    }
    
    // TICKET RESPONSES
    // CREATE
    public TicketResponse CreateTicketResponse(TicketResponse response)
    {
        response.Id = _responses.Max(r => r.Id) + 1;
        _responses.Add(response);
        return response;
    }
    // READ
    public IEnumerable<TicketResponse> ReadTicketResponsesOfTicket(int ticketNumber)
    {
        return _tickets.Find(t => t.TicketNumber == ticketNumber).Responses;
    }
    
    
    
    
    
    // Dummy data
    // Static constructor
    static TicketRepositoryHC()
    {
        Seed();
    }

    private static void Seed()
    {
        // Create first ticket with three responses
        Ticket t1 = new Ticket()
        {
            TicketNumber = _tickets.Count + 1, // Ok, no ticket will be deleted
            AccountId = 1,
            Text = "Ik kan mij niet aanmelden op de webmail",
            DateOpened = new DateTime(2022, 9, 9, 13, 5, 59),
            State = TicketState.Open,
            Responses = new List<TicketResponse>()
        };
        _tickets.Add(t1);
        TicketResponse t1r1 = new TicketResponse()
        {
            Id = _responses.Count + 1,
            Ticket = t1,
            Text = "Account was geblokkeerd",
            Date = new DateTime(2022, 9, 9, 13, 24, 48),
            IsClientResponse = false
        };
        t1.Responses.Add(t1r1);
        _responses.Add(t1r1);

        TicketResponse t1r2 = new TicketResponse()
        {
            Id = _responses.Count + 1,
            Ticket = t1,
            Text = "Account terug in orde en nieuw paswoord ingesteld",
            Date = new DateTime(2022, 9, 9, 13, 29, 11),
            IsClientResponse = false
        };
        t1.Responses.Add(t1r2);
        _responses.Add(t1r2);
        TicketResponse t1r3 = new TicketResponse()
        {
            Id = _responses.Count + 1,
            Ticket = t1,
            Text = "Aanmelden gelukt en paswoord gewijzigd",
            Date = new DateTime(2022, 9, 10, 7, 22, 36),
            IsClientResponse = true
        };
        t1.Responses.Add(t1r3);
        _responses.Add(t1r3);
        t1.State = TicketState.Closed;

        // Create second ticket with one response
        Ticket t2 = new Ticket()
        {
            TicketNumber = _tickets.Count + 1,
            AccountId = 1,
            Text = "Geen internetverbinding",
            DateOpened = new DateTime(2022, 11, 5, 9, 45, 13),
            State = TicketState.Open,
            Responses = new List<TicketResponse>()
        };
        _tickets.Add(t2);
        TicketResponse t2r1 = new TicketResponse()
        {
            Id = _responses.Count + 1,
            Ticket = t2,
            Text = "Controleer of de kabel goed is aangesloten",
            Date = new DateTime(2022, 11, 5, 11, 25, 42),
            IsClientResponse = false
        };
        t2.Responses.Add(t2r1);
        _responses.Add(t2r1);
        t2.State = TicketState.Answered;

        // Create hardware ticket without responses
        HardwareTicket ht1 = new HardwareTicket()
        {
            TicketNumber = _tickets.Count + 1,
            AccountId = 2,
            Text = "Blue screen!",
            DateOpened = new DateTime(2022, 12, 14, 19, 5, 2),
            State = TicketState.Open,
            DeviceName = "PC-123456"
        };
        _tickets.Add(ht1);
    }
}