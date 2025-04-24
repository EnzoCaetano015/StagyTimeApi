namespace StagyTimeApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Senha { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdTipoUser { get; set; }
    }
}
