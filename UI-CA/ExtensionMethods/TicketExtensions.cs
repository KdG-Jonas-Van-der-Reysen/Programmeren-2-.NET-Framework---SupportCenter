using SC.BL.Domain;
namespace SC.UI.CA.ExtensionMethods;

internal static class TicketExtensions
{
    internal static string GetInfo(this Ticket t)
    {
        return String.Format("[{0}] {1} ({2} antwoorden)"
            , t.TicketNumber, t.Text
            , t.Responses == null ? 0 : t.Responses.Count);
    }
}