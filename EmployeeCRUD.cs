using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace EmpRestSharp
{
    public class EmployeeCRUD
    {
        private RestClient client;

        public EmployeeCRUD()
        {
            client = new RestClient("http://localhost:3000");
        }

        // RunOperations calls the different CRUD methods sequentially.
        public async Task RunOperationsAsync()
        {
            Console.WriteLine("Getting employee list...");
            await GetEmployeeList();

            Console.WriteLine("---------------------------------------");    

            Console.WriteLine("Adding a new employee...");
            await AddNewEmployee("30", "Disha", "67000");
            Console.WriteLine("---------------------------------------");


            Console.WriteLine("Adding multiple employees...");
            await AddMultipleEmployees();
            Console.WriteLine("---------------------------------------");

            Console.WriteLine("Updating employee salary...");
            await UpdateEmployeeSalary("3", "85000");
            Console.WriteLine("---------------------------------------");


            Console.WriteLine("Deleting an employee...");
            await DeleteEmployee("a4df");

            Console.WriteLine("-------------- All Operation Done -------------------------");


        }


        //Getting EmployeeList
        private async Task GetEmployeeList()
        {
            var request = new RestRequest("Employees", Method.Get);
            var response = await client.ExecuteAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                List<Employee> employeeList = JsonConvert.DeserializeObject<List<Employee>>(response.Content);
                foreach (var emp in employeeList)
                {
                    Console.WriteLine($"Id: {emp.Id}, Name: {emp.Name}, Salary: {emp.Salary}");
                }
            }
            else
            {
                Console.WriteLine($"Failed to get employee list. Status: {response.StatusCode}");
            }
        }


        //Add new Employee
        private  async Task AddNewEmployee(string Id, string name, string salary)
        {
            var request = new RestRequest("Employees", Method.Post);
            var jsonObj = new
            {
                Id = Id,
                name = name,
                salary = salary
            };

            request.AddJsonBody(jsonObj);

            var response = await client.ExecuteAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                var employee = JsonConvert.DeserializeObject<Employee>(response.Content);
                Console.WriteLine($"Added Employee: {employee.Name}, Salary: {employee.Salary}");
            }
            else
            {
                Console.WriteLine($"Failed to add employee. Status: {response.StatusCode}");
            }
        }


        //Add Multiple Employee
        private  async Task AddMultipleEmployees()
        {
            var employees = new List<Employee>
            {
                new Employee { Name = "Dustin", Salary = "85536" },
                new Employee { Name = "Will", Salary = "120123" },
                new Employee { Name = "Eleven", Salary = "123456" }
            };

            foreach (var emp in employees)
            {
                var request = new RestRequest("Employees", Method.Post);
                var jsonObj = new
                {
                    name = emp.Name,
                    salary = emp.Salary
                };

                request.AddJsonBody(jsonObj);

                var response = await client.ExecuteAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    var addedEmp = JsonConvert.DeserializeObject<Employee>(response.Content);
                    Console.WriteLine($"Added Employee: {addedEmp.Name}, Salary: {addedEmp.Salary}");
                }
                else
                {
                    Console.WriteLine($"Failed to add employee {emp.Name}. Status: {response.StatusCode}");
                }
            }
        }


        
        //Update Employee Record
        private async Task UpdateEmployeeSalary(string id, string newSalary)
        {
            var request = new RestRequest($"Employees/{id}", Method.Put);
            var jsonObj = new
            {
                salary = newSalary
            };

            request.AddJsonBody(jsonObj);

            var response = await client.ExecuteAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var updatedEmp = JsonConvert.DeserializeObject<Employee>(response.Content);
                Console.WriteLine($"Updated Employee: {updatedEmp.Name}, New Salary: {updatedEmp.Salary}");
            }
            else
            {
                Console.WriteLine($"Failed to update employee salary. Status: {response.StatusCode}");
            }
        }

        //Delete Employee Record By ID
        private async Task DeleteEmployee(string id)
        {
            var request = new RestRequest($"Employees/{id}", Method.Delete);
            var response = await client.ExecuteAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"Successfully deleted employee with ID: {id}");
            }
            else
            {
                Console.WriteLine($"Failed to delete employee with ID: {id}. Status: {response.StatusCode}");
            }
        }



    }
 }



