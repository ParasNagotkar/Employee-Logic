using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;

namespace Employee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationdbContext _context;

        public EmployeeController(ApplicationdbContext context)
        {
            _context = context;
        }


        static string baseDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
        string filePath = Path.Combine(baseDirectory, "employee.json");


        [HttpGet("GetEmployee")]
        public List<Dictionary<string, string>> GetEmployee(string type,int id)
        {
            List<Dictionary<string, string>> result = new List<Dictionary<string, string>>();
            try
            {
                SqlParameter[] parameter;

                switch (type)
                {
                    case "R":
                        parameter = new[]
                        {
                        new SqlParameter("@type", type)
                        };
                        break;
                    case "E":
                        parameter = new[]
                        {
                        new SqlParameter("@type", type),
                        new SqlParameter("@employeeID", id)
                        };
                       break;
                    default:
                        throw new ArgumentException("Invalid type");
                }
                 result = _context.ExecuteStoredProcedure("CRUD_Employee", parameter);
            }
            catch (Exception ex)
            {
                List<Dictionary<string, string>> error = new List<Dictionary<string, string>>
                {
                new Dictionary<string, string> { { "error", "message :"+ex }, { "status", "400" } }
                };

                return error;
            }
            return result;

        }



        [HttpPost("InsertUpdateEmployee")]
        public List<Dictionary<string, string>> InsertEmployee(Employeecs employeecs)
        {
            List<Dictionary<string, string>> result = new List<Dictionary<string, string>>();
            try
            {
                SqlParameter[] parameter;
                if (employeecs.id == 0)
                {
                     parameter = new[] {
                    new SqlParameter("@name", employeecs.name ),
                    new SqlParameter("@address", employeecs.address ),
                    new SqlParameter("@designation", employeecs.designation ),
                    new SqlParameter("@MobileNumber", employeecs.MobileNumber ),
                    new SqlParameter("@email", employeecs.Email ),
                    new SqlParameter("@dob", employeecs.DOB ),
                    new SqlParameter("@WorkExperience", employeecs.WorkExperience ),
                    new SqlParameter("@desc", employeecs.description ),
                    new SqlParameter("@Type", 'I' )
                    };
                }
                else
                {
                     parameter = new[] {
                    new SqlParameter("@employeeID", employeecs.id ),
                    new SqlParameter("@name", employeecs.name ),
                    new SqlParameter("@address", employeecs.address ),
                    new SqlParameter("@MobileNumber", employeecs.MobileNumber ),
                    new SqlParameter("@email", employeecs.Email ),
                    new SqlParameter("@designation", employeecs.designation ),
                    new SqlParameter("@dob", employeecs.DOB ),
                    new SqlParameter("@WorkExperience", employeecs.WorkExperience ),
                    new SqlParameter("@desc", employeecs.description ),
                    new SqlParameter("@Type", 'U' )
                    };

                }

                result = _context.ExecuteStoredProcedure("CRUD_Employee", parameter);
            }
            catch (Exception ex)
            {
                List<Dictionary<string, string>> error = new List<Dictionary<string, string>>
                {
                new Dictionary<string, string> { { "error", "message :"+ex }, { "status", "400" } }
                };

                return error;
            }
            return result;

        }



        [HttpDelete("DeleteEmployee")]
        public List<Dictionary<string, string>> DeleteEmployee(int id)
        {
            List<Dictionary<string, string>> result = new List<Dictionary<string, string>>();
            try
            {
                SqlParameter[] parameter;

                    parameter = new[] {
                    new SqlParameter("@employeeID",id),
                    new SqlParameter("@Type", 'D' )
                    };
                
                
                result = _context.ExecuteStoredProcedure("CRUD_Employee", parameter);
            }
            catch (Exception ex)
            {
                List<Dictionary<string, string>> error = new List<Dictionary<string, string>>
                {
                new Dictionary<string, string> { { "error", "message :"+ex }, { "status", "400" } }
                };

                return error;
            }
            return result;

        }
    }

}
