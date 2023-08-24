using Dapper;
using Dapper.Transaction;
using WebApi.DataTransfer;
using WebApi.Models;

namespace WebApi.DataAcesss;

public class UserDao {
    private readonly Database _database;

    public UserDao(Database database) {
        _database = database;
    }

    public async Task<IEnumerable<User>> GetUsers() {

        string query = @"
            SELECT
	            bu.id_personal AS IdPersonal,
	            CONCAT_WS(' ', p.nombre, p.apepaterno, p.apematerno) Name,
                bu.password AS Password,
                bu.fecha_alta AS FechaAlta,
                bu.ultimo_acceso AS UltimoAcceso,
                bu.estatus AS Estatus
            FROM innovacion.boxsh_usuarios bu
            INNER JOIN personal.personal p ON p.idpersonal = bu.id_personal;
        ";

        using var conn = _database.GetConnection();

        return await conn.QueryAsync<User>(query);
    }

    public async Task<User> GetUser(int IdPersonal) {

        string query = @"
            SELECT
	            bu.id_personal AS IdPersonal,
	            CONCAT_WS(' ', p.nombre, p.apepaterno, p.apematerno) Name
            FROM innovacion.boxsh_usuarios bu
            INNER JOIN personal.personal p ON p.idpersonal = bu.id_personal
            WHERE bu.id_personal = @IdPersonal;
        ";

        using var conn = _database.GetConnection();
        
        return await conn.QueryFirstOrDefaultAsync<User>(query, new { IdPersonal });
    }

    public async Task<int> CreateUser(UserDto user) {

        string query = @"
            INSERT INTO innovacion.boxsh_usuarios (
                id_personal,
                password,
                fecha_alta,
                estatus
            ) VALUES (
                @IdPersonal,
                @Password,
                NOW(),
                @Estatus
            );
        ";

        using var conn = _database.GetConnection();
        
        return await conn.ExecuteAsync(query, user);
    }

    public async Task<int> UpdateUser(UserDto user) {

        string query = @"
            UPDATE innovacion.boxsh_usuarios SET
                password = @Password,
                estatus = @Estatus
            WHERE id_personal = @IdPersonal;
        ";

        using var conn = _database.GetConnection();
        
        return await conn.ExecuteAsync(query, user);
    }

    public async Task<int> DeleteUser(int IdPersonal) {

        string query = @"
            DELETE FROM innovacion.boxsh_usuarios
            WHERE id_personal = @IdPersonal;
        ";

        using var conn = _database.GetConnection();
        
        return await conn.ExecuteAsync(query, new { IdPersonal });
    }
}