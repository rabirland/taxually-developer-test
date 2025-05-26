using Microsoft.AspNetCore.Mvc;
using Taxually.TechnicalTest.Dto;
using Taxually.TechnicalTest.Mapping;
using Taxually.TechnicalTest.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Taxually.TechnicalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VatRegistrationController : ControllerBase
    {
        private readonly ITaxuallyService _taxuallyService;

        public VatRegistrationController(ITaxuallyService taxuallyService)
        {
            _taxuallyService = taxuallyService ?? throw new ArgumentNullException(nameof(taxuallyService));
        }

        /// <summary>
        /// Registers a company for a VAT number in a given country
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] VatRegistrationRequestDto request)
        {
            var domainRequest = request.ToDomain();

            // NOTE: I would have made this into 3 different endpoints, but changing an API
            // of an existing product isn't always possible.
            try
            {
                switch (request.Country)
                {
                    case "GB":
                        // UK has an API to register for a VAT number
                        await _taxuallyService.RegisterUKVatNumber(domainRequest);
                        break;
                    case "FR":
                        await _taxuallyService.RegisterFrenchVatNumber(domainRequest);
                        break;
                    case "DE":
                        await _taxuallyService.RegisterGermanVatNumber(domainRequest);
                        break;
                    default:
                        throw new Exception("Country not supported");

                }
            }
            catch (Exception)
            {
                // NOTE: Would worth considering returning a useful error message, based on the exception
                // but not the raw exception message
                return BadRequest();
            }
            

            return Ok();
        }
    }
}
