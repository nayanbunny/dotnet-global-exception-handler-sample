using Microsoft.AspNetCore.Mvc;
using DotNet.Global.Exceptions.Handler.Sample.Api.Models;
using DotNet.Global.Exceptions.Handler.Sample.Library.Constants;
using DotNet.Global.Exceptions.Handler.Sample.Library.Helpers;

namespace DotNet.Global.Exceptions.Handler.Sample.Api.Controllers;

/// <summary>
/// The Weather Forecast Controller to support Get Operation.
/// </summary>
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    /// <summary>
    /// The Summaries String Array.
    /// </summary>
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    /// <summary>
    /// The ILogger Field for Weather Forecast Controller Class Type.
    /// </summary>
    private readonly ILogger<WeatherForecastController> _logger;

    /// <summary>
    /// The Weather Forecast Controller Constructor
    /// </summary>
    /// <param name="logger">ILogger of Weather Forecast Controller Class Type</param>
    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        // Initializing _logger field.
        _logger = logger;
    }

    /// <summary>
    /// Get Operation of Weather Forecast
    /// </summary>
    /// <param name="exceptionType">The Exception Type</param>
    /// <returns>Weather Forecast Array</returns>
    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get([FromQuery] string? exceptionType = null)
    {

        if (exceptionType != null)
        {
            if (exceptionType.Equals(ServiceConstants.AppError, StringComparison.InvariantCultureIgnoreCase))
                throw new AppException("Input Data is incorrect.");
            else if (exceptionType.Equals(ServiceConstants.KeyNotFoundError, StringComparison.InvariantCultureIgnoreCase))
                throw new KeyNotFoundException("Data not found.");
            else
                throw new Exception();
        }

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
