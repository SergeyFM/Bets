
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("api/[controller]")]
public class BetsController : ControllerBase {
    private readonly IBetsService _betsService;
    private readonly ILogger<BetsController> _logger;

    public BetsController(IBetsService betsService, ILogger<BetsController> logger) {
        _betsService = betsService;
        _logger = logger;
    }

    [HttpGet("events")]
    public IEnumerable<BetEvent> GetEvents() {
        return _betsService.GetAllEvents();
    }

    [HttpGet("events/{id}")]
    public BetEvent GetEvent(int id) {
        return _betsService.GetEventDetails(id);
    }

    [HttpPost("events/{eventId}/placebet")]
    public IActionResult PlaceBet(int eventId, int outcomeId, decimal amount) {
        _betsService.PlaceBet(eventId, outcomeId, amount);
        return Ok("Ставка сделана");
    }

    [HttpPost("bets/{betId}/cancel")]
    public IActionResult CancelBet(int betId) {
        _betsService.CancelBet(betId);
        return Ok("Ставка отменена");
    }
}
