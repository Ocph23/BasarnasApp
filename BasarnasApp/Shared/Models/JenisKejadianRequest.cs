namespace BasarnasApp.Shared.Models
{
    public class JenisKejadianRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description{ get; set; }
        public ICollection<InstansiRequest> Instansis {get; set;} = new List<InstansiRequest>();
    }
}
