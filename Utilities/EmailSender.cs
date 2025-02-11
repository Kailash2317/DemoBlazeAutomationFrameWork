using System;
using System.Net;
using System.Net.Mail;

namespace Demo_Project.Utilities
{
    public static class EmailSender
    {
        // Method to send an email with an attachment
        public static void SendEmailWithAttachment(string recipientEmail, string subject, string body, string attachmentFilePath)
        {
            try
            {
                // Sender email credentials
                string senderEmail = "tester.prajapat@gmail.com"; // Your email
                string senderPassword = "unvk xrgx lqra ordf"; // Your email password (or app password for Gmail)

                // Set up the SMTP client (using Gmail's SMTP server)
                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
                    smtpClient.EnableSsl = true;

                    // Create the email message
                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress(senderEmail);
                        mailMessage.To.Add(recipientEmail);  // Recipient's email address
                        mailMessage.Subject = subject;      // Subject of the email
                        mailMessage.Body = body;            // Body of the email

                        // Attach the report
                        if (!string.IsNullOrEmpty(attachmentFilePath))
                        {
                            Attachment attachment = new Attachment(attachmentFilePath);
                            mailMessage.Attachments.Add(attachment);
                        }

                        // Send the email
                        smtpClient.Send(mailMessage);
                    }
                }

                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to send email. Error: " + ex.Message);
            }
        }
    }
}
