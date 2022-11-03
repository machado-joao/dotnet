using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Activity.Utils
{
    public class Functions
    {
        public static string HashText(string text, string hashType)
        {
            HashAlgorithm algorithm = HashAlgorithm.Create(hashType);
            if (algorithm == null)
            {
                throw new ArgumentException("This hash type doesn\'t exist.", "hashType");
            }
            byte[] hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(text));
            return Convert.ToBase64String(hash);
        }

        public static string SendEmail(string receiver, string subject, string body)
        {
            try
            {
                MailAddress from = new MailAddress("<email@gmail.com>");
                MailAddress to = new MailAddress(receiver);

                MailMessage message = new MailMessage(from, to);
                message.IsBodyHtml = true;
                message.Subject = subject;
                message.Body = body;
                message.Priority = MailPriority.Normal;

                SmtpClient client = new SmtpClient();
                client.Send(message);
                return "success|Email sent!";
            }
            catch
            {
                return "error|Error sending the email!";
            }
        }

        public static string Encode(string text)
        {
            byte[] base64String = new byte[text.Length];
            base64String = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(base64String);
        }

        public static string Decode(string text)
        {
            var encode = new UTF8Encoding();
            var utf8Decoder = encode.GetDecoder();
            byte[] stringValue = Convert.FromBase64String(text);
            int count = utf8Decoder.GetCharCount(stringValue, 0, stringValue.Length);
            char[] charDecode = new char[count];
            utf8Decoder.GetChars(stringValue, 0, stringValue.Length, charDecode, 0);
            return new String(charDecode);
        }
    }
}
