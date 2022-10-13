using SC.BL.Domain;
namespace SC.UI.CA.ExtensionMethods;
internal static class TicketResponseExtensions
{
    internal static string GetInfo(this TicketResponse r)
    {
        return String.Format("{0:dd/MM/yyyy} {1}{2}", r.Date, r.Text
            , r.IsClientResponse ? " (client)" : "");
    }
}