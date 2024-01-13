
using PokerPlanning.Services;

namespace PokerPlanning.BackgroundServices
{
    public sealed class JanitorService : BackgroundService
    {
        private readonly IRoomService _roomService;
        private readonly ILogger<JanitorService> _logger;
        private readonly TimeSpan _janitorInterval = TimeSpan.FromMinutes(1);
        public JanitorService(
            IRoomService roomService,
            ILogger<JanitorService> logger)
        {
            _roomService = roomService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Janitor is cleaning...");
                    
                    var cleanedRooms = await _roomService.CleanRooms();
                    _logger.LogInformation($"Janitor cleaned {cleanedRooms} rooms.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Janitor encountered an error while cleaning rooms: {ex.Message}. StackTrace: {ex.StackTrace}");
                }
                finally
                {
                    await Task.Delay(_janitorInterval, stoppingToken);
                }

            }
        }
    }
}
