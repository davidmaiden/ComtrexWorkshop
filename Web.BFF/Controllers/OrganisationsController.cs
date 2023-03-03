using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json;
using Web.BFF.Models;

namespace Web.BFF.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class OrganisationsController : ControllerBase
    {
        private const string companyList = 
            "[" +
            "{\"Id\": \"4e964689-b257-4a10-b543-0581cfdb9046\",\"Name\": \"Golden Oaks Inns\"}," +
            "{\"Id\": \"b336c63f-e7f8-42ae-be38-0f538115fa22\",\"Name\": \"Pub & Dining\"}," +
            "{\"Id\": \"a3b66a3a-bf6c-4d74-9bc3-13ebd5d1eb9f\",\"Name\": \"Belhaven\"}," +
            "{\"Id\": \"3a56a5f8-c6a3-49b7-b807-24be6d1f0784\",\"Name\": \"Locals Urban\"}," +
            "{\"Id\": \"3e41b8e1-cf77-4cb2-9252-4081d2aed3c0\",\"Name\": \"Old English Inns\"}," +
            "{\"Id\": \"60abd49e-9386-4812-8a11-547acfe2e56f\",\"Name\": \"Greene King Staff System\"}," +
            "{\"Id\": \"221d1780-b7a2-4090-aebc-5a6ce170defa\",\"Name\": \"Chef & Brewer Tables\"}," +
            "{\"Id\": \"b3d95f0b-5f2a-415d-9b6b-5e35ac9342ea\",\"Name\": \"Eating Inn\"}," +
            "{\"Id\": \"9060d648-f927-4e93-80d4-5e9de6942d6b\",\"Name\": \"MainstreamHS\"}," +
            "{\"Id\": \"6764089d-59b9-4841-ba72-62915419386d\",\"Name\": \"Chef & Brewer\"}," +
            "{\"Id\": \"89b88e55-6ed6-4935-8d68-643cfc6e571d\",\"Name\": \"M&E\"}," +
            "{\"Id\": \"a4dda22c-8dc7-4ef8-a027-65ae96c6d442\",\"Name\": \"Pub & Grill\"}," +
            "{\"Id\": \"bd07794d-8278-4af9-9945-6bfd72ea4675\",\"Name\": \"Farmhouse Inns\"}," +
            "{\"Id\": \"ba73cad9-c71c-4595-8a51-87ac44c22722\",\"Name\": \"Crafted Pubs \"}," +
            "{\"Id\": \"730ab791-4116-4107-928a-b2ee3ad8c0e8\",\"Name\": \"Pub & Carvery\"}," +
            "{\"Id\": \"892739f3-ad06-4f6d-a6d4-b8ecb103589f\",\"Name\": \"Metropolitan\"}," +
            "{\"Id\": \"33c3cd06-145b-49b3-b3ca-c4af4a26ae53\",\"Name\": \"Hungry Horse\"}," +
            "{\"Id\": \"c878227b-08e0-4468-8a5d-dbafbcff10d4\",\"Name\": \"Flame Grill\"}," +
            "{\"Id\": \"db53839d-4c33-461a-be94-df23ec981d1e\",\"Name\": \"Fayre and Square\"}," +
            "{\"Id\": \"a7c65597-ee4a-4d53-9216-e4bf71427eea\",\"Name\": \"Locals Value\"}," +
            "{\"Id\": \"e64c7eb9-f766-491e-85bc-e928c4a183a5\",\"Name\": \"Premium\"}," +
            "{\"Id\": \"d8cd7476-d5e4-41cb-82b3-f1d51d171082\",\"Name\": \"Loch Fyne Restaurants Ltd\"}," +
            "{\"Id\": \"92b0d1f6-436d-4954-8574-f494176d3fe1\",\"Name\": \"Hive\"}," +
            "{\"Id\": \"d35069a5-a3e1-4b95-8f0d-fd4f129b2ffa\",\"Name\": \"Flaming Grill\"}" +
            "]";

        [HttpGet("{orgUnitId}/children/values")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrganisationUnitValue>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<OrganisationUnitValue>> GetOrganizationUnits(Guid orgUnitId, [FromQuery] string? type, int? pageSize, int? pageNumber)
        {
            if (orgUnitId.Equals(Guid.Empty))
                return BadRequest();

            if (!orgUnitId.Equals(Guid.Parse("1be4ef1c-921c-4075-a10f-9b74cb292e81")))
                return NotFound();

            IEnumerable<OrganisationUnitValue> list = new List<OrganisationUnitValue>();

            if (type?.ToLower() == "company")
            {
                list = JsonSerializer.Deserialize<IEnumerable<OrganisationUnitValue>>(companyList);
            }

            if (type?.ToLower() == "site")
            {
                list = JsonSerializer.Deserialize<IEnumerable<OrganisationUnitValue>>(companyList);

            }

            return Ok(list);
        }

        [HttpGet("{orgUnitId}/children/devices")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<DevicesLocation>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<DevicesLocation>> GetDevices(Guid orgUnitId, [FromQuery] string? type, int? pageSize, int? pageNumber)
        {
            if (orgUnitId.Equals(Guid.Empty))
                return BadRequest();

            if (!orgUnitId.Equals(Guid.Parse("1be4ef1c-921c-4075-a10f-9b74cb292e81")))
                return NotFound();

            IEnumerable<DevicesLocation> list = new List<DevicesLocation>();


            return Ok(list);
        }

        [HttpPost("{orgUnitId}/pairingcodes")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public ActionResult<string> CreatePairingCode(Guid orgUnitId)
        {

            return Ok();
        }

        [HttpDelete("{orgUnitId}/devices/{deviceId}")]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public ActionResult<string> DeletePairingCode(Guid orgUnitId, string pairingCode)
        {

            return NoContent();
        }

        [HttpPatch("{orgUnitId}/licenses/{licenseId}/revoke")]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public ActionResult<string> Revokelicense(Guid orgUnitId, string licenseId)
        {

            return NoContent();
        }

        [Authorize(Policy = "ZonalAdminPolicy")]
        [HttpGet("tenants")]
        public ActionResult<IEnumerable<OrganisationUnitValue>> GetTenants()
        {
            IEnumerable<OrganisationUnitValue> list = new List<OrganisationUnitValue>();

            return Ok(list);
        }

    }
}
