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
        //Update Employee record by ID

        var client = new RestClient("http://localhost:3000");
        Console.WriteLine("Update Record");

        var request = new RestRequest($"Employees/{1}", Method.Put);

        var jsonObj = new
        {
            
            Name = "Puja",
            Salary = "90000",
        };

        request.AddJsonBody(jsonObj);

        var response =  client.Execute(request);

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            Console.WriteLine($"Updated Employee ID: 1,New Name :{jsonObj.Name}, New Salary: {jsonObj.Salary} ");
        }
        else
        {
            Console.WriteLine($"Failed to update employee salary. Status: {response.StatusCode}");
        }



    }
}

