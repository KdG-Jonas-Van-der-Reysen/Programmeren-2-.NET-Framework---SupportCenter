namespace SC.BL.Domain;
using System.ComponentModel.DataAnnotations;
public class Ticket
{
    public int TicketNumber { get; set; }
    public int AccountId { get; set; }
    
    [Required]
    [StringLength(100, ErrorMessage = "Er zijn maximaal 100 tekens toegestaan")]
    public string Text { get; set; }
    public DateTime DateOpened { get; set; }
    public TicketState State { get; set; }
    public ICollection<TicketResponse> Responses { get; set; }
}

