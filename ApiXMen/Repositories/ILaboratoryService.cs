namespace ApiXMen.Repositories
{
    public interface ILaboratoryService
    {
        ValueTask<bool> DnaCheck(string dnaArray);
       
    }
}
