
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using ws = PeruTourism.ws_SendGridEmail;
using System.Text;
using System.Security.Cryptography;
using System.Web.Configuration;
using System.Collections.Specialized;
using PeruTourism.Repository.Data;
using PeruTourism.Models.PeruTourism;
using CustomLog;

namespace PeruTourism.Utility
{
    public class PeruTourismMail
    {

        
        private const string MailAccountFrom = "soporte@imaginasoftware.com";
        private string strSecureAppSettings;

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