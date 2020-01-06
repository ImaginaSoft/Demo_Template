
using System;
using System.Net.Mail;
using ws = PeruTourism.ws_SendGridEmail;
using System.Text;
using System.Security.Cryptography;
using System.Web.Configuration;
using PeruTourism.Models.PeruTourism;
using CustomLog;

namespace PeruTourism.Utility
{
    public class PeruTourismMail
    {

        
        private const string MailAccountFrom = "soporte@imaginasoftware.com";

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

        private static string Encriptar(string cadena)
        {
            using (System.Security.Cryptography.MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(cadena));

                StringBuilder sBuilder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                return sBuilder.ToString();
            }
        }

        public void EnviarCorreoSendGrid(string pNombreEmisor,string pCorreoEmisor,string pDestinatarios,string pAsunto,string pCuerpo) {

            var send = new ws.wsMails();
            var resultado = new RespuestaEmail();


            try {
                // *************************************************************************
                // Para envio de correos sin adjunto
                // *************************************************************************

                var enviarSinAdjunto = send.EnviarCorreo(
                    new ws.Autenticacion
                    {
                        InWebId = ws.TipoWeb.Mozart,
                        StUsuario = Encriptar(WebConfigurationManager.AppSettings["key_usuarioTk"].ToString()),
                        StClave = Encriptar(WebConfigurationManager.AppSettings["key_claveTk"].ToString())
                    },
                    new ws.Correo
                    {
                        NombreEmisor = pNombreEmisor,
                        CorreoEmisor = pCorreoEmisor,
                        Destinatarios = pDestinatarios,
                        Asunto = pAsunto,
                        CuerpoHtml = pCuerpo
                    });
                        // *************************************************************************

                        //resultado.Tipo = enviarSinAdjunto.
                        resultado.Valor = enviarSinAdjunto.Valor;
            }
            catch (Exception ex) {

                resultado.Tipo = TipoRespuesta.Error;
                resultado.Valor = ex.Message;

                Bitacora.Current.Error<PeruTourismMail>(ex, new { TipoRespuesta.Error });

            }
        
        }

    }
}