
using PokerPlanning.Services;

namespace PokerPlanning.BackgroundServices
{
    public sealed class JanitorService : BackgroundService
    {
        private readonly IRoomService _roomService;
        public JanitorService(IRoomService roomService)
        {
            _roomService = roomService;
        }


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            return Task.CompletedTask;
        }
    }
}
