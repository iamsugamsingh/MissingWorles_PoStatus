using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Data;

namespace Worles_Po_Status
{
    public partial class AutoMailSend : System.Web.UI.Page
    {
        public string Pass, FromEmailid, HostAdd, msg;

        protected void Page_Load(object sender, EventArgs e)
        {            
            if (Session["Last10DaysMissingPoData"] != null)
            {
                DataTable Last10DaysMissingPoData=Session["Last10DaysMissingPoData"] as DataTable;

                string toMail = "it2@anugroup.net, technical@worles.com";
                string cc="";
                string bcc="";
                string Subj="Missing Worles PO Reminder";
                string Message = "Dear Sir, <br/><br/>These are the Missing Worles POs, that still not put in our system by production department from last 10 days. <br/><br/>";
                string[] path = new string[] { "" };

                for (int i = 0; i < Last10DaysMissingPoData.Rows.Count; i++)
                {
                    Message += Last10DaysMissingPoData.Rows[i][1].ToString()+", ";
                }

                Email_With_CCandBCC(toMail, cc, bcc, Subj, Message /*,path*/);

                Session.Remove("Last10DaysMissingPoData");
            }
        }

        public void Email_With_CCandBCC(String ToEmail, string cc, string bcc, string Subj, string Message /*,string[] path*/)
        {
            try
            {
                //Reading sender Email credential from web.config file
                HostAdd = ConfigurationManager.AppSettings["Host"].ToString();
                FromEmailid = ConfigurationManager.AppSettings["FromMail"].ToString();
                Pass = ConfigurationManager.AppSettings["Password"].ToString();

                //creating the object of MailMessage
                MailMessage mailMessage = new MailMessage();

                mailMessage.From = new MailAddress(FromEmailid); //From Email Id
                mailMessage.Subject = Subj; //Subject of Email
                mailMessage.Body = Message; //body or message of Email
                mailMessage.IsBodyHtml = true;

                //mailMessage.To.Add(new MailAddress(ToEmail)); //reciver's TO Email Id
                //string[] CCId = cc.Split(',');
                //foreach (string CCEmail in CCId)
                //{
                //    mailMessage.CC.Add(new MailAddress(cc)); //Adding Multiple CC email Id
                //}
                ////mailMessage.CC.Add(new MailAddress(cc)); //Adding CC email Id

                //string[] bccid = bcc.Split(',');

                //foreach (string bccEmailId in bccid)
                //{
                //    mailMessage.Bcc.Add(new MailAddress(bcc)); //Adding Multiple BCC email Id
                //}
                foreach (var address in ToEmail.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                {
                    mailMessage.To.Add(address);
                }

                foreach (var address in cc.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                {
                    mailMessage.CC.Add(address);
                }

                foreach (var address in bcc.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                {
                    mailMessage.Bcc.Add(address);
                }

                //foreach (var attachfile in path)
                //{
                //    //mailMessage.Bcc.Add(new MailAddress(bcc)); //Adding BCC email Id
                //    mailMessage.Attachments.Add(new Attachment("W:\\Software\\Dummy\\OC_pdf\\" + attachfile));
                //}

                SmtpClient smtp = new SmtpClient(); // creating object of smptpclient
                smtp.Host = HostAdd; //host of emailaddress for example smtp.gmail.com etc

                //network and security related credentials

                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential();
                NetworkCred.UserName = mailMessage.From.Address;
                NetworkCred.Password = Pass;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mailMessage);                
                
                Response.Write("Email Send Succesfully!...");                
            }
            catch (Exception ex)
            {
                Response.Write("Can't Send Mail..... "+ ex.Message); 
            }
            
        }
    }
}