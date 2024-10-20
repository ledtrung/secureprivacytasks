using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace SecurePrivacy.Task1.API.Resources.Consent;

[ApiController]
[Route("consent")]
public class ConsentController : ControllerBase
{
    private readonly ILogger<ConsentController> _logger;
    private readonly ConsentService _consentService;

    public ConsentController(ConsentService consentService, ILogger<ConsentController> logger)
    {
        _consentService = consentService;
        _logger = logger;
    }

    [HttpGet]
    [Route("types")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ConsentType>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get()
    {
        try
        {
            var consentTypes = await _consentService.GetConsentTypes();
            return Ok(consentTypes);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(500, "Contact support");
        }
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Consent))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get([FromRoute] string id)
    {
        try
        {
            var consent = await _consentService.GetConsent(id);
            if (consent is null)
                return NotFound();
                
            return Ok(consent);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, ex.Message);
            return NotFound();
        }
        catch (NullReferenceException ex)
        {
            _logger.LogError(ex, ex.Message);
            return NotFound();
        }
        catch (Exception ex)
        { 
            _logger.LogError(ex, ex.Message);
            return StatusCode(500, "Contact support");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Add([FromBody] Consent consent)
    {
        try
        {
            await _consentService.AddOrUpdateConsent(consent);
            
            return Created();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(500, "Contact support");
        }
    }
}
