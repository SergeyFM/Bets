
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("api/[controller]")]
public class WalletController : ControllerBase {
    private readonly IWalletService _walletService;
    private readonly ILogger<WalletController> _logger;

    public WalletController(IWalletService walletService, ILogger<WalletController> logger) {
        _walletService = walletService;
        _logger = logger;
    }

    [HttpGet("{userId}/balance")]
    public decimal GetBalance(int userId) {
        return _walletService.GetBalance(userId);
    }

    [HttpPost("{userId}/addfunds")]
    public IActionResult AddFunds(int userId, decimal amount) {
        _walletService.AddFunds(userId, amount);
        return Ok("Средства добавлены");
    }

    [HttpPost("{userId}/withdraw")]
    public IActionResult WithdrawFunds(int userId, decimal amount) {
        _walletService.WithdrawFunds(userId, amount);
        return Ok("Средства сняты");
    }
}
