using MySql.Data.MySqlClient;
using Webapi.Libs.Jwt;
using Webapi.Models;
using Webapi.Schemas;

namespace Webapi.Libs.User.Endpoints {
    public class Login(DatabaseContext dbContext, string jwtSecret, string keyId) {
        private const string Query = """
                                     SELECT id, name, email, password
                                       FROM Users
                                      WHERE email = @Email
                                        AND password = @Password
                                     """;
        
        private readonly JwtHelper _jwtHelper = new (jwtSecret, keyId);

        public async Task<IResult> LoginAsync(LoginSchema loginRequest) {
            var user = await GetUserAsync(loginRequest.Email, loginRequest.Password);
            if (user != null) {
                var token = _jwtHelper.GenerateJwtToken(user);
                var response = new LoginResponse { user = user, token = token};
                return Results.Ok(response);
            }
            var errorResponse = new ErrorResponse { error = "Not Authorized", errorCode = "401" };
            return Results.Json(errorResponse, statusCode: StatusCodes.Status401Unauthorized);
        }

        public virtual async Task<UserModel?> GetUserAsync(string email, string password) {
            await using var connection = dbContext.GetConnection();
            await connection.OpenAsync();
            
            await using var command = new MySqlCommand(Query, connection);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@Password", password);

            await using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync()) {
                return new UserModel {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2),
                    Password = reader.GetString(3)
                };
            }

            return null;
        }
    }
}
