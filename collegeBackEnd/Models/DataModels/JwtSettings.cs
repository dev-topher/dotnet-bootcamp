namespace collegeBackEnd.Models.DataModels
{
    public class JwtSettings
    {
        public bool ValidateIssuerSingingKey { get; set; }
        public string IssuerSingingKey {  set; get; } = string.Empty;
        public bool ValidateIssuer { get; set; } = true;
        public string? ValidIssuer {  set; get; }
        public bool ValidateAudience { get; set; } = true;
        public string? ValidAudience {  set; get; }
        public bool RequireExpirationTime { get; set; } 
        public bool ValidateLifetime { get; set; } = true;

    }
}
