namespace SchoolWebApplication.DTOs
{
    public class RefreshDto
    {
        public string AccessToken { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
    }
}