using EmployeeAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeAccess
{
    public partial class ValidateOTP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClientScript.RegisterClientScriptBlock(Page.GetType(), "NOBACK", "<script type='text/javascript'>if(history.length>0)history.go(+1);</script>");
            }
        }

        protected void VerifyOTP(object sender, EventArgs e)
        {
            try
            {
                string sessOtp = Session["otpstr"].ToString();
                string userOtp = otpText.Text;
                if (sessOtp == userOtp)
                {
                    try
                    {
                        HttpClient client = new HttpClient();
                        string MobileNo = Session["mobileNo"].ToString();
                        client.BaseAddress = new Uri("http://localhost:52668//Employee");
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/pdf"));
                        string reqUrl = "api/CheckEmpExists?EmpMobile=" + MobileNo;
                        HttpResponseMessage response = client.GetAsync(reqUrl).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            string data = response.Content.ReadAsStringAsync().Result;
                            string messageType = (new JavaScriptSerializer()).Deserialize<string>(data);
                            if (messageType == "Error")
                            {
                                errorMessage.InnerHtml = "Employee not registered.  Please <a href='UserInfo.aspx'>signup!</a>";
                                verfyDiv.Visible = false;
                            }
                            else
                            {
                                successMessage.InnerText = "Employee already registered!";
                                errorMessage.InnerText = "";
                                verfyDiv.Visible = false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        errorMessage.InnerText = ex.Message;
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    errorMessage.InnerText = "OTP does not match. Please try again!";
                }
            }
            catch (Exception ex)
            {
                errorMessage.InnerText = ex.Message;
            }
        }
    }
}