using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace PeruTourism.Utility
{
    public class PeruTourismMail
    {

        public const string strServer = "";

        /*Para Implementar
         
              new PeruTourismMail().EnviarCorreo("Error CompraHotel FileAccessDeniedFault", "webmaster@gruponuevomundo.com.pe", "FileAccessDeniedFault: <br>" + ex.Message);*/

        public void EnviarCorreo(String pStrAsunto, String pStrMailTo, String pStrBody)
        {
            try
            {
                MailAddress objFrom = new MailAddress("errores-webmaster@inboxplace.com");
                MailMessage objMailMessage = new MailMessage();
                if (pStrMailTo == null)
                {
                    objMailMessage.To.Add("webmaster@inboxplace.com");
                }
                else
                {
                    objMailMessage.To.Add(pStrMailTo);
                }
                objMailMessage.From = objFrom;
                objMailMessage.Subject = pStrAsunto;
                objMailMessage.Body = pStrBody;
                objMailMessage.IsBodyHtml = true;

                SmtpClient objSMPTClient = new SmtpClient();
                objSMPTClient.Host = strServer;
                objSMPTClient.Send(objMailMessage);
            }
            catch (Exception ex)
            {
            }
        }





    }
}