using System.ComponentModel.DataAnnotations;

namespace ApiXMen.Models
{
    public class DnaResult
    {
        [Key]
        public string Id { get; set; }
        public string DnaVerified { get; set; }
        public bool TestResult { get; set; }
    }
}
