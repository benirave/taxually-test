using Microsoft.AspNetCore.Mvc;
using Taxually.TechnicalTest.Factories;
using Taxually.TechnicalTest.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Taxually.TechnicalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VatRegistrationController : ControllerBase
    {
        private readonly IVatRegistrationFactory _vatRegistrationFactory;

        public VatRegistrationController(IVatRegistrationFactory vatRegistrationFactory)
        {
            _vatRegistrationFactory = vatRegistrationFactory;
        }

        /// <summary>
        /// Registers a company for a VAT number in a given country
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] VatRegistrationRequest request)
        {
            var vatRegistrationService = _vatRegistrationFactory.GetVatRegistrationService(request.Country);

            await vatRegistrationService.RegisterCompany(request);
            
            return Ok();
        }
    }
}
