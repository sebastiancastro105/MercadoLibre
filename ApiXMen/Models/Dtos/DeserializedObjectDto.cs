namespace ApiXMen.Models.Dtos
{
    public class DeserializedObjectDto
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public string[,] SerializedArray { get; set; }
    }
}
