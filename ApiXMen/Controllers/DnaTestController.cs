using ApiXMen.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ApiXMen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DnaTestController : ControllerBase
    {
        private readonly ILaboratoryService Laboratory;

        public DnaTestController(ILaboratoryService laboratory)
        {
            Laboratory = laboratory;
        }
        [Route("mutant")]
        [HttpPost]
        public async ValueTask<StatusCodeResult> IsMutant(string dna)
        {
            bool resultTest = await Laboratory.DnaCheck(dna);

            if (resultTest)
                return await ValueTask.FromResult(StatusCode(200));
            else
                return await ValueTask.FromResult(StatusCode(403));

        }

        [Route("start")]
        [HttpPost]
        public async ValueTask<StatusCodeResult> GetData(string dna)
        {
            bool resultTest = await Laboratory.DnaCheck(dna);

            if (resultTest)
                return await ValueTask.FromResult(StatusCode(200));
            else
                return await ValueTask.FromResult(StatusCode(403));

        }
    }
}
