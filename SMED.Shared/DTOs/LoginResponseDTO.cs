namespace SMED.Shared.DTOs
{
    public class LoginResponseDTO
    {
        public bool IsAuthenticated { get; set; }
        public string? Message { get; set; }
        public string? Token { get; set; }
        public UserDTO? User { get; set; }
    }
}
