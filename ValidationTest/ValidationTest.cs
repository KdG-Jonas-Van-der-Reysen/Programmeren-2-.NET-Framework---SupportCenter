using System.ComponentModel.DataAnnotations;
using SC.BL.Domain;


Ticket t1 = new Ticket() {
    TicketNumber = 1, AccountId = 1, Text = "",
    State = TicketState.Open, DateOpened = DateTime.Now
};

var t1Errors = new List<ValidationResult>();
Validator.TryValidateObject(t1, new ValidationContext(t1), t1Errors
    , true);


Ticket t2 = new HardwareTicket() {
    TicketNumber = 2, AccountId = 1, DeviceName = "LPT-9876", Text = "text",
    State = TicketState.Open, DateOpened = DateTime.Now
};

var t2Errors = new List<ValidationResult>();
Validator.TryValidateObject(t2, new ValidationContext(t2), t2Errors
    , true);

TicketResponse tr = new TicketResponse() {
    Id = 1, Text = "response", IsClientResponse = true, Date = new DateTime(2017, 1, 1),
    Ticket = new Ticket() {
        TicketNumber = 3, AccountId = 1, Text = "text", State = TicketState.Open, DateOpened = new DateTime(2018, 1, 1)
    }
};
var trErrors = new List<ValidationResult>();
Validator.TryValidateObject(tr, new ValidationContext(tr), trErrors, true);

Console.WriteLine("Goodbye");
// ... (test-code is coming here)
Environment.Exit(0);