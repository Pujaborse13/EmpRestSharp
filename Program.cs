using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
namespace EmpRestSharp;

class Program
{
    private static RestClient client;

    static void Main(string[] args)
    {
        var client = new RestClient("http://localhost:3000");
        
        // Create a GET request
        var request = new RestRequest("Employees", Method.Get);

        // Execute the request
        var response = client.Execute(request);

        // Check response status
        if (response.StatusCode == HttpStatusCode.OK)
        {
            Console.WriteLine("Data retrieved successfully!");

            // Deserialize the response content
            List<Employee> employeeList = JsonConvert.DeserializeObject<List<Employee>>(response.Content);

            // Print the employee details
            if (employeeList != null)
            {
                foreach (var emp in employeeList)
                {
                    Console.WriteLine($"Id: {emp.Id}, Name: {emp.Name}, Salary: {emp.Salary}");
                }
            }
            else
            {
                Console.WriteLine("No employees found or failed to deserialize.");
            }
        }
        else
        {
            Console.WriteLine($"Error: {response.StatusCode}, Message: {response.Content}");
        }
        








    }
}

