using System.Net;
using System.Net.Mail;

namespace BEBackendLib.Module.Gmail
{
    public class BEGmail : IDisposable
    {
        private BEGmailModel? _gmailModel = null;
        private SmtpClient? _smtpClient = null;
        private MailMessage? _message = null;

        public BEGmail(BEGmailModel gmailModel)
        {
            _gmailModel = gmailModel;
        }


        public void InitGmail()
        {
            try
            {
                if (_gmailModel is null)
                    throw new Exception("Gmail Model is null, please check again!");

                if (string.IsNullOrEmpty(_gmailModel.Host))
                    throw new Exception("Host is nullorEmpty, please check again!");

                if (string.IsNullOrEmpty(_gmailModel.Email))
                    throw new Exception("Email is nullorEmpty, please check again!");

                if (string.IsNullOrEmpty(_gmailModel.Password))
                    throw new Exception("Password is nullorEmpty, please check again!");

                _smtpClient = new SmtpClient(_gmailModel.Host)
                {
                    Port = _gmailModel.Port,
                    Credentials = new NetworkCredential(_gmailModel.Email, _gmailModel.Password),
                    EnableSsl = _gmailModel.EnableSsl,
                    Timeout = _gmailModel.TimeOut,
                    DeliveryMethod = _gmailModel.DeliveryMethod,
                    UseDefaultCredentials = _gmailModel.UseDefaultCredentials
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Send()
        {
            bool res = false;
            try
            {
                if (_smtpClient is null)
                    throw new Exception("SMTP Client is null, please call InitGmail()!");

                if (string.IsNullOrEmpty(_gmailModel?.Email))
                    throw new Exception("Email is nullorEmpty, please check again!");

                if (_gmailModel?.ToEmails is null || _gmailModel?.ToEmails.Length == 0)
                    throw new Exception("To Email is null or length zero, please check again!");

                if (string.IsNullOrEmpty(_gmailModel?.Subject))
                    throw new Exception("Subject is nullorEmpty, please check again!");

                _message = new MailMessage();
                _message.From = new MailAddress(_gmailModel.Email);

                foreach (var toEmail in _gmailModel.ToEmails)
                    _message.To.Add(new MailAddress(toEmail));

                if (_gmailModel.ToCcEmails is not null && _gmailModel.ToCcEmails.Length > 0)
                {
                    foreach (var toCcEmail in _gmailModel.ToCcEmails)
                        _message.CC.Add(new MailAddress(toCcEmail));
                }

                if (_gmailModel.ToBccEmails is not null && _gmailModel.ToBccEmails.Length > 0)
                {
                    foreach (var toBccEmail in _gmailModel.ToBccEmails)
                        _message.Bcc.Add(new MailAddress(toBccEmail));
                }

                _message.Subject = _gmailModel.Subject;
                _message.Body = _gmailModel.Body;

                if (_gmailModel?.Attachments is not null && _gmailModel?.Attachments.Length > 0)
                {
                    foreach (var att in _gmailModel.Attachments)
                        _message.Attachments.Add(att);
                }

                _smtpClient.Send(_message);
                res = true;
            }
            catch (Exception)
            {
                throw;
            }
            return res;
        }

        public void Dispose()
        {
            try
            {
                _smtpClient = null;
                _message = null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
