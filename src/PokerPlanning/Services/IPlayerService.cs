
namespace PokerPlanning.Services
{
    public interface IPlayerService
    {
        EventHandler? PlayerNameChanged { get; set; }

        Task<string> GetUserName();
        Task SetUserName(string userName);
    }
}