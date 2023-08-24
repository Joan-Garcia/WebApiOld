namespace WebApi.Models;

public class User {
    public int IdPersonal { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public DateTime FechaAlta { get; set; }
    public DateTime UltimoAcceso { get; set; }
    public UserStatus Estatus { get; set; }
}

public enum UserStatus {
    Active = 1,
    Inactive = 0
}