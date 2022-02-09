using GSF.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace ZeppelinWebsite.Helper
{
    public class EmailVerification
    {

        public static void SendVerficationLink(string userEmail, string verificationLink)
        {

            var fromMail = new MailAddress(FromEmail.FromEmailAddress, displayName: "Zeppelin");
            var toEmail = new MailAddress(userEmail);
            var fromEmailPassword = FromEmail.FromEmailPassword;
            string subject = "Activate account";
            //string body = "<br/> Please click o the following link in order to activate your account" + "<br/><a href='" + verificationLink + "'/>";
            string body = "<br/><br/> Please click o the following link in order to activate your account " + "<br/><br/><a href='" + verificationLink + "'>" + verificationLink + "</a>";
            var smtp = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                UseDefaultCredentials = false,
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                
                Credentials = new NetworkCredential(userName: fromMail.Address, fromEmailPassword)
            };



            using (var message = new MailMessage(fromMail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true


            })
            {
               
                smtp.Send(message);
            }
        } 
    



    } 



}



  
