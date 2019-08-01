
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

        public const string strServer = "smtp.gmail.com";

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





    }
}