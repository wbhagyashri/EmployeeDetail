using EmployeeAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeAccess
{
    public partial class UserInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClientScript.RegisterClientScriptBlock(Page.GetType(), "NOBACK", "<script type='text/javascript'>if(history.length>0)history.go(+1);</script>");
            }
        }

        protected void AddUserInfo(object sender, EventArgs e)
        {
            try
            {
                DBConnection dbConn = new DBConnection();
                EmpDetails empDetails = new EmpDetails();
                empDetails.EmpFirstName = empFirstName.Text;
                empDetails.EmpLastName = empLastName.Text;
                empDetails.EmpEmail = empEmail.Text;
                empDetails.EmpMobile = Session["mobileNo"].ToString();
                //if (!resEmpDet.IsAuthorized)
                //{
                    try
                    {
                        HttpClient client = new HttpClient();

                        client.BaseAddress = new Uri("http://localhost:52668//Employee");
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/pdf"));
                        string reqUrl = "api/AddEmployee";
                        var param = Newtonsoft.Json.JsonConvert.SerializeObject(empDetails);
                        HttpContent contentPost = new StringContent(param, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = client.PostAsync(reqUrl, contentPost).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            EmpDetails newEmpDet = response.Content.ReadAsAsync<EmpDetails>().Result;
                            if (newEmpDet.messageType == "success")
                            {
                                successMessage.InnerText = newEmpDet.message;
                                empDet.Visible = false;
                            }
                            else
                            {
                                errorMessage.InnerText = newEmpDet.message;
                                empDet.Visible = true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        errorMessage.InnerText = ex.Message;
                        Console.WriteLine(ex.Message);
                    }
                //}                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}