using System.ComponentModel.DataAnnotations;

namespace SC.BL.Domain;

public class TicketResponse: IValidatableObject
{
    public int Id { get; set; }
    public string Text { get; set; }
    [Required]
    public DateTime Date { get; set; }
    public bool IsClientResponse { get; set; }
    
    [Required]
    public Ticket Ticket { get; set; }

    IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
    {
        // Make a list with the errors
        List<ValidationResult> errors = new List<ValidationResult>();
        
        // Check if the date is not before the Ticket.DateOpened
        if (Date <= Ticket.DateOpened)
        {
            errors.Add(new ValidationResult("Can't be before the date the ticket is created!", new String[] {"Date", "Ticket.DateOpened"}));
        }

        return errors;
    }
}