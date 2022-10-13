namespace SC.BL.Domain;

public class TicketResponse
{
    public int Id { get; set; }
    public string Text { get; set; }
    public DateTime Date { get; set; }
    public bool IsClientResponse { get; set; }
    public Ticket Ticket { get; set; }
}