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

        //Delete Record by Id

        var client = new RestClient("http://localhost:3000");

        Console.WriteLine("Deleting an employee...");
        var request = new RestRequest("Employees/10", Method.Delete);
        var response = client.Execute(request);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            Console.WriteLine($"Successfully deleted employee with ID: 10");
        }
        else
        {
            Console.WriteLine($"Failed to delete employee. Status: {response.StatusCode}");
        }


    }
}

