namespace Webapi.Models {
    public class LoginResponse {
        public UserModel user { get; set; }
        public string token { get; set; }
    }
}