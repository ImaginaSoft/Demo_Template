
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

namespace PeruTourism.Utility
{
    public class PeruTourismMail
    {

        //public const string strServer = "smtp.gmail.com";
        //private const string MailErrorSubject = "ERROR EN LA APLICACION";
        private const string MailAccountFrom = "soporte@imaginasoftware.com";
        //private const string MailErrorAccountTo = "bdavid2290@gmail.com";
        //private const string MailErrorTemplate = @"<h1> {0} </h1>
	       //                             TargetSite:   <b>{1}</b>  <hr />
	       //                             Source:       <b>{2}</b>  <hr />
        //                                    StackTrace:   <b>{3}</b>  <hr />
	       //                             DateTime:     <b>{5}</b>";
        /*Para Implementar
         
              new PeruTourismMail().EnviarCorreo("Error CompraHotel FileAccessDeniedFault", "webmaster@gruponuevomundo.com.pe", "FileAccessDeniedFault: <br>" + ex.Message);*/

        public void EnviarCorreo(string pStrAsunto, string pStrMailTo, string pStrBody)
        {
            try
            {

                var fromAddress = new MailAddress("soporte@imaginasoftware.com", "From Name");
                
                var toAddress = new MailAddress(pStrMailTo, "To Name");
                 string fromPassword = "soporteImagina123";
                 string subject = pStrAsunto;
                 string body = pStrBody;

                var smtp = new SmtpClient
                {
                    Host = "kem10730.inkahosting.com.pe",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }


            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void EnviarCorreo_GG(string pStrAsunto, string pStrMailToCliente,string pStrEmailVendedor, string pStrBody)
        {
            try
            {
                var client = new SmtpClient();
                var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(pStrMailToCliente, pStrMailToCliente);
                mailMessage.To.Add(pStrEmailVendedor);
                mailMessage.CC.Add(pStrMailToCliente);
                mailMessage.Subject = pStrAsunto;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = pStrBody;
                mailMessage.Priority = MailPriority.High;

                //mailMessage.Body = string.Format(MailErrorTemplate, ex.Message, ex.TargetSite, ex.Source, ex.StackTrace, DateTime.Now.ToString("MMMM dd, yyyy HH:mm tt"));
                client.EnableSsl = true;
                client.Send(mailMessage);
                mailMessage.Dispose();
            }
            catch (Exception ex) {

                throw;
            }
           
        }



    }
}