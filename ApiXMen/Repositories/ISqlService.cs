using ApiXMen.Models;

namespace ApiXMen.Repositories
{
    public interface ISqlService
    {
        ValueTask SaveDnaResult(DnaResult dnaResult);
        ValueTask<List<DnaResult>> GetAllDnaResult();
    }
}
