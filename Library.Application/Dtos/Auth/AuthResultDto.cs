namespace Library.Application.Dtos.Auth
{
    public sealed record AuthResultDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
