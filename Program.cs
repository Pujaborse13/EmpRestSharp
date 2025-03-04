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
        // Adding new Employee

        var client = new RestClient("http://localhost:3000");

        Console.WriteLine("Adding a new Employee");

        var request = new RestRequest("Employees", Method.Post);
        var jsonObj = new
        {
            Id = "10",
            Name = "pratibha",
            Salary = "66900"
        };


        request.AddJsonBody(jsonObj);

        var response = client.Execute(request);

        if (response.StatusCode == HttpStatusCode.Created)
        {
            Console.WriteLine($"Added Employee: {jsonObj.Name}, Salary: {jsonObj.Salary}");
        }
        else
        {
            Console.WriteLine($"Failed to add employee. Status: {response.StatusCode}");
        }



    }
}

