using Xunit;
using Moq;
using MySql.Data.MySqlClient;
using Webapi.Libs.Jwt;
using Webapi.Models;
using Webapi.Schemas;
using Webapi.Libs.User.Endpoints;
using Microsoft.AspNetCore.Http.HttpResults;

public class LoginTests {
    private readonly Mock<DatabaseContext> _dbContextMock;
    private readonly Mock<JwtHelper> _jwtHelperMock;
    private readonly Mock<Login> _loginServiceMock;
    private readonly string _connectionString;
    private readonly JwtSettings _jwtSettings;

    public LoginTests() {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        _connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        _jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>() ?? new JwtSettings();

        _dbContextMock = new Mock<DatabaseContext>(_connectionString);
        _jwtHelperMock = new Mock<JwtHelper>(_jwtSettings.Secret, _jwtSettings.KeyId);
        _loginServiceMock = new Mock<Login>(_dbContextMock.Object, _jwtSettings.Secret, _jwtSettings.KeyId);
    }

    [Fact]
    public async Task LoginAsync_ShouldReturnOkResult_WhenUserIsValid() {
        var loginRequest = new LoginSchema("test@example.com", "password1234!");
        var user = new UserModel
            { Id = 1, Name = "Test User", Email = "test@example.com", Password = "password1234!" };
        
        _dbContextMock.Setup(db => db.GetConnection()).Returns(new MySqlConnection(_connectionString));
        _loginServiceMock.Setup(service => service.GetUserAsync(loginRequest.Email, loginRequest.Password))
            .ReturnsAsync(user);
        
        var result = await _loginServiceMock.Object.LoginAsync(loginRequest);
        var okResult = Assert.IsType<Ok<LoginResponse>>(result);
        var response = okResult.Value;

        Assert.NotNull(response);
        Assert.Equal(user.Id, response.user.Id);
        Assert.Equal(user.Name, response.user.Name);
        Assert.Equal(user.Email, response.user.Email);
    }

    [Fact]
    public async Task LoginAsync_ShouldReturnUnauthorizedResult_WhenUserIsInvalid() {
        var loginRequest = new LoginSchema("invalid@example.com", "wrongpassword");
        _dbContextMock.Setup(db => db.GetConnection()).Returns(new MySqlConnection(_connectionString));
        var result = await _loginServiceMock.Object.LoginAsync(loginRequest);
        var jsonResult = Assert.IsType<JsonHttpResult<ErrorResponse>>(result);
        var response = jsonResult.Value;

        Assert.NotNull(response);
        Assert.Equal("Not Authorized", response.error);
        Assert.Equal("401", response.errorCode);
    }
}