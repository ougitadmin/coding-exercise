using Webapi.Libs.User.Endpoints;

namespace Webapi.Libs {
    public static class ServiceCollectionExtensions {
        public static void AddUserLogin(this IServiceCollection services, IConfiguration configuration) {
            var jwtSecret = configuration.GetSection("JwtSettings:Secret").Value;
            var keyId = configuration.GetSection("JwtSettings:KeyId").Value;

            if (string.IsNullOrEmpty(jwtSecret)) {
                throw new ArgumentNullException(nameof(jwtSecret), "JWT Secret cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(keyId)) {
                throw new ArgumentNullException(nameof(keyId), "KeyId cannot be null or empty.");
            }

            services.AddScoped<Login>(provider => new Login(
                provider.GetRequiredService<DatabaseContext>(),
                jwtSecret,
                keyId
            ));
        }
    }
}