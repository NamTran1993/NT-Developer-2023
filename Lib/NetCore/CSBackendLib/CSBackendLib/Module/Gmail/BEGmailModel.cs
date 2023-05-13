using System.Net.Mail;

namespace BEBackendLib.Module.Gmail
{
    public class BEGmailModel
    {
        public string Host { get; set; } = "smtp.gmail.com";
        public int Port { get; set; } = 587;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool EnableSsl { get; set; } = true;
        public bool IsBodyHtml { get; set; } = false;
        public int TimeOut { get; set; } = 60000; // (milliseconds).
        public SmtpDeliveryMethod DeliveryMethod { get; set; } = SmtpDeliveryMethod.Network;
        public bool UseDefaultCredentials { get; set; } = false;

        // ---
        public string[]? ToEmails { get; set; } = null;
        public string[]? ToCcEmails { get; set; } = null;
        public string[]? ToBccEmails { get; set; } = null;

        // ----
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public Attachment[]? Attachments { get; set; } = null;
    }
}
