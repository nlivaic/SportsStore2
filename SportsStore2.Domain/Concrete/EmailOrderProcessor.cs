using SportsStore2.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportsStore2.Domain.Entities;
using System.Net.Mail;
using System.Text;
using System.Net;

namespace SportsStore2.Domain.Concrete {
    public class EmailSettings {
        public string MailToAddress = "nenad.livaic@gmail.com";
        public string MailFromAddress = "SportsStore@gmail.com";
        public bool UseSsl = true;
        public string Username = "";
        public string Password = "";
        public string ServerName = "smtp.example.com";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string FileLocation = @"C:\Users\nenad\Desktop\delete_me\SportsStoreEmail";
    }

    public class EmailOrderProcessor : IOrderProcessor {
        private EmailSettings emailSettings;

        public EmailOrderProcessor(EmailSettings emailSettings) {
            this.emailSettings = emailSettings;
        }

        private MailMessage CreateEmailMessage(Cart cart, ShippingDetails shippingDetails) {
            MailMessage message = new MailMessage();
            message.From = new MailAddress("order_processor@sportsstore.com");
            message.To.Add("shipping@sportsstore.com");
            message.Subject = "New order";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Items and quantity:");
            foreach (var item in cart.Items) {
                sb.AppendLine(String.Format("{0}, pieces: {1}.", item.Item.Name, item.Quantity));
            }
            sb.AppendLine("Shipping Address:");
            sb.AppendLine(String.Format("{0} {1}", shippingDetails.FirstName, shippingDetails.LastName));
            sb.AppendLine(shippingDetails.AddressLine1);
            if (!String.IsNullOrEmpty(shippingDetails.AddressLine2)) {
                sb.AppendLine(shippingDetails.AddressLine2);
            }
            if (!String.IsNullOrEmpty(shippingDetails.AddressLine3)) {
                sb.AppendLine(shippingDetails.AddressLine3);
            }
            message.Body = sb.ToString();
            return message;
        }

        public void ProcessOrder(Cart cart, ShippingDetails shippingDetails) {
            SmtpClient client = new SmtpClient();
            client.PickupDirectoryLocation = @"C:\Users\nenad\Desktop\delete_me\SportsStoreEmail";
            client.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);
            if (emailSettings.WriteAsFile) {
                client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
            }
            client.Send(CreateEmailMessage(cart, shippingDetails));
        }
    }
}