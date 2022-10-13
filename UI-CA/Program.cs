using SC.UI.CA;
using SC.BL;
using SC.DAL;

// Data access layer
ITicketRepository repo = new TicketRepositoryHC();
// Business layer
ITicketManager mgr = new TicketManager(repo);

// UI layer
ConsoleUi consoleUi = new ConsoleUi(mgr);
consoleUi.Run(); 