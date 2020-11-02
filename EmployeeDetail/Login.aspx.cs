using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeAccess
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClientScript.RegisterClientScriptBlock(Page.GetType(), "NOBACK", "<script type='text/javascript'>if(history.length>0)history.go(+1);</script>");
            }
        }

        protected void SendOTP(object sender, EventArgs e)
        {
            try
            {
                string otpstr = GenerateOTP();
                sendSms(mobielNo.Text, otpstr);
                Session["otpstr"] = otpstr;
                Session["mobileNo"] = mobielNo.Text;
                Response.Redirect("ValidateOTP.aspx");
            }
            catch(Exception ex)
            {
                errorMessage.InnerText = "Error while sending OTP";
            }
        }

        private string GenerateOTP()
        {
            string characters = "1234567890";
            int length = 6;
            string otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }
            return otp;
        }

        private void sendSms(string mobileNo, string OTP)
        {
            try
            {
                string strUrl = "http://control.msg91.com/api/sendotp.php?otp_length=4&authkey=346074AJVQCfwXrm7M5f9ea7f9P1&message=&sender=TEST&mobile=" + mobileNo + "&otp=" + OTP + "";
                WebRequest request = HttpWebRequest.Create(strUrl);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream s = (Stream)response.GetResponseStream();
                StreamReader readStream = new StreamReader(s);
                string dataString = readStream.ReadToEnd();
                response.Close();
                s.Close();
                readStream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        }
    }