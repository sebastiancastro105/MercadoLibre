using ApiXMen.Models;
using ApiXMen.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiXMen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DnaTestController : ControllerBase
    {
        private readonly ILaboratoryService Laboratory;
        private readonly ISqlService Context;

        public DnaTestController(ILaboratoryService laboratory, ISqlService context)
        {
            Laboratory = laboratory;
            Context = context;
        }

        [Route("mutant")]
        [HttpPost]
        public async ValueTask<StatusCodeResult> IsMutant(string dna)
        {
            try
            {
                bool resultTest = await Laboratory.DnaCheck(dna);

                if (resultTest)
                    return await ValueTask.FromResult(StatusCode(200));
                else
                    return await ValueTask.FromResult(StatusCode(403));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Route("stat")]
        [HttpGet]
        public async ValueTask<string> GetData()
        {
            float countMutantDna = 0;
            float CountHumanDna = 0;
            float ratio = 0.0F;

            List<DnaResult> dnaResult = await Context.GetAllDnaResult();

            foreach (var item in dnaResult)
            {
                if (item.TestResult)
                    countMutantDna++;
                else
                    CountHumanDna++;
            }

            ratio = countMutantDna / CountHumanDna;
            var request = new { count_mutant_dna = (int)countMutantDna, count_human_dna = (int)CountHumanDna, ratio = MathF.Round(ratio,1)};

            string serializeObject = JsonConvert.SerializeObject(request);
            return await ValueTask.FromResult(serializeObject);
        }
    }
}
