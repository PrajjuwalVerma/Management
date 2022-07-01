using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Management.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Management.Controllers
{
    [Route("api/[controller]")]
    public class ManagementController : ControllerBase
    {
        private readonly ManagementContext managementContext;
        public ManagementController(ManagementContext _managementContext)
        {
            managementContext = _managementContext;
        }
        [HttpPost]
        [Route("InsertManagerRecord")]
        public bool InsertManagerRecord(Manager manager)
        {
            try
            {
                managementContext.Managers.Add(manager);
                managementContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        [HttpPost]
        [Route("InsertEmployeeRecord")]
        public bool InsertEmployeeRecord(Employee employee)
        {
            try
            {
                managementContext.Employees.Add(employee);
                managementContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        [HttpGet]
        [Route("GetAllEmployeeRecord")]
        public async Task<List<EmployeeManagerData>> GetAllEmployeeRecord()
        {
            return await (from e in managementContext.Employees
                          join m in managementContext.Managers
                          on e.ManagerId equals m.ManagerId
                          select new EmployeeManagerData
                          {
                              EmployeeName = e.EmployeeName,
                              ManagerName = m.ManagerName
                          }).ToListAsync();
        }

        [HttpGet]
        [Route("GetAllManagerRecord")]
        public async Task<List<EmployeeManagerData>> GetAllManagerRecord()
        {
            return await (from m in managementContext.Managers
                          select new EmployeeManagerData
                          {
                              ManagerName = m.ManagerName,
                              ManagerSalary = m.ManagerSalary
                          }).ToListAsync();
        }

        [HttpGet]
        [Route("GetTotalEmployeesUnderManager")]
        public async Task<List<EmployeeManagerData>> GetTotalEmployeesUnderManager()
        {
            return await (from e in managementContext.Employees
                          join m in managementContext.Managers
                          on e.ManagerId equals m.ManagerId
                          select new EmployeeManagerData
                          {
                              ManagerName = m.ManagerName,
                              ManagerTotalEmployees = managementContext.Employees.Select(x => new { x.EmployeeId, x.ManagerId }).Where(y => y.ManagerId == m.ManagerId).Count()
                          }).Distinct().ToListAsync();
        }

        [HttpGet]
        [Route("GetNthHighestSalary")]
        public EmployeeManagerData GetNthHighestSalary(int nCount)
        {
            var highestSalaryData = new EmployeeManagerData();
            var employee = managementContext.Employees.OrderByDescending(x => x.EmployeeSalary).Select(y => new { y.EmployeeSalary, y.ManagerId }).Take(nCount).Skip(nCount - 1).FirstOrDefault();
            highestSalaryData.EmployeeSalary = employee.EmployeeSalary;
            highestSalaryData.ManagerName = managementContext.Managers.First(x => x.ManagerId == employee.ManagerId).ManagerName;
            return highestSalaryData;
        }
    }
}