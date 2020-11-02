using EmployeeAccess;
using EmployeeAccess.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace EmpDetailsSystem.Controllers
{
    public class EmployeeController : ApiController
    {
        [HttpGet]
        [Route("api/CheckEmpExists")]
        public HttpResponseMessage CheckEmpExists(string EmpMobile)
        {
            EmpDetails resEmpDet = null;
            try
            {
                DBConnection dbConn = new DBConnection();
                resEmpDet = dbConn.GetUserDetails(EmpMobile);
                if (resEmpDet.IsAuthorized)
                {
                    return new HttpResponseMessage()
                    {
                        Content = new StringContent(JsonConvert.SerializeObject("Success"), Encoding.UTF8, "application/json")
                    };
                }
                else
                {
                    return new HttpResponseMessage()
                    {
                        Content = new StringContent(JsonConvert.SerializeObject("Error"), Encoding.UTF8, "application/json")
                    };
                }
            }
            catch
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
        }

        [HttpPost]
        [Route("api/AddEmployee")]
        public IHttpActionResult AddEmployee(EmpDetails empDetails)
        {
            EmpDetails resEmpDet = empDetails;
            try
            {
                DBConnection dbConn = new DBConnection();
                string status = dbConn.AddEmployeeDetails(empDetails);
                if (status == "Successfully Uploaded")
                {
                    resEmpDet.messageType = "success";
                    resEmpDet.message = "Successfully added user's info";
                    return Ok(resEmpDet);
                }
            }
            catch (Exception ex)
            {
                resEmpDet = new EmpDetails();
                resEmpDet.messageType = "error";
                resEmpDet.message = ex.Message;
                return Ok(resEmpDet);
            }
            return Ok(resEmpDet);
        }
    }
}
