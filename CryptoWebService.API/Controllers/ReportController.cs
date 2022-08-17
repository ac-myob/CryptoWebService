using CryptoWebService.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CryptoWebService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportController : Controller
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet]
    public async Task<ActionResult<double>> GetReport(DateTime startDate, DateTime endDate, int userId, int coinId)
    {
        return Ok(await _reportService.GetCoinProfitLossAsync(startDate, endDate, userId, coinId));
    }
}