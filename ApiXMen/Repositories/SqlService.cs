using ApiXMen.Models;

namespace ApiXMen.Repositories
{
    public class SqlService : ISqlService
    {
        private readonly XMenContext Context;

        public SqlService(XMenContext context)
        {
            Context = context;
        }
        public async ValueTask SaveDnaResult(DnaResult dnaResult)
        {
            Context.DnaResults.Add(dnaResult);
            await Context.SaveChangesAsync();
        }
        public async ValueTask<List<DnaResult>> GetAllDnaResult()
        {
            return await ValueTask.FromResult(Context.DnaResults.ToList());
        }
    }
}
