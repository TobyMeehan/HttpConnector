using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using TobyMeehan.Http;

namespace Tester
{
    class Program
    {
        public static HttpClient Client { get; set; } = new HttpClient();

        const string BaseUri = "http://dummy.restapiexample.com/api/v1";

        static async Task Main(string[] args)
        {

            while (true)
            {
                switch (Console.ReadLine())
                {
                    case "list":
                        await ListEmployees();
                        break;
                    case "single":
                        await GetEmployee();
                        break;
                    case "create":
                        await CreateEmployee();
                        break;
                    case "update":
                        await UpdateEmployee();
                        break;
                    case "delete":
                        await DeleteEmployee();
                        break;
                    case "exit":
                        Environment.Exit(0);
                        break;
                }
            }
        }

        static void DisplayEmployee(Employee employee)
        {
            Console.WriteLine();
            Console.WriteLine($"Name: {employee.Name}");
            Console.WriteLine($"Age: {employee.Age}");
            Console.WriteLine($"Salary: {employee.Salary}");
        }

        static void DisplayEmployee(GetEmployeeResponse employee)
        {
            Console.WriteLine();
            Console.WriteLine($"Name: {employee.Employee_Name}");
            Console.WriteLine($"Age: {employee.Employee_Age}");
            Console.WriteLine($"Salary: {employee.Employee_Salary}");
        }

        static async Task ListEmployees()
        {
            await Client.Get($"{BaseUri}/employees")
                .OnOK<DummyResponse<List<GetEmployeeResponse>>>((result) =>
                {
                    foreach (var employee in result.Data)
                    {
                        DisplayEmployee(employee);
                    }
                })
                .OnBadRequest<DummyResponse<object>>((result, statusCode, reasonPhrase) =>
                {
                    Console.WriteLine(result.Message);
                })
                .SendAsync();
        }

        static async Task GetEmployee()
        {
            Console.Write("Enter employee ID: ");

            int id = int.Parse(Console.ReadLine());

            await Client.Get($"{BaseUri}/employee/{id}")
                .OnOK<DummyResponse<GetEmployeeResponse>>((result) =>
                {
                    DisplayEmployee(result.Data);
                })
                .OnBadRequest<DummyResponse<object>>((result, statusCode, reasonPhrase) =>
                {
                    Console.WriteLine(result.Message);
                })
                .SendAsync();
        }

        static async Task CreateEmployee()
        {
            Employee employee = new Employee();

            Console.Write("Enter employee name: ");
            employee.Name = Console.ReadLine();

            Console.Write("Enter employee salary: ");
            employee.Salary = int.Parse(Console.ReadLine());

            Console.Write("Enter employee age: ");
            employee.Age = int.Parse(Console.ReadLine());

            await Client.Post($"{BaseUri}/create", employee)
                .OnOK<DummyResponse<object>>((result) =>
                {
                    Console.WriteLine("Successfully created employee.");
                })
                .OnBadRequest<DummyResponse<string>>((result, statusCode, reasonPhrase) => 
                {
                    Console.WriteLine(result.Data);
                })
                .SendAsync();
        }

        static async Task UpdateEmployee()
        {
            Employee employee = new Employee();

            Console.Write("Enter employee ID: ");
            employee.Id = int.Parse(Console.ReadLine());

            Console.Write("Enter employee's new name: ");
            employee.Name = Console.ReadLine();

            Console.Write("Enter employee's new salary: ");
            employee.Salary = int.Parse(Console.ReadLine());

            Console.Write("Enter employee's new age: ");
            employee.Age = int.Parse(Console.ReadLine());

            await Client.Put($"{BaseUri}/update/{employee.Id}", employee)
                .OnOK<DummyResponse<Employee>>((result) =>
                {
                    Console.WriteLine("Updated employee:");
                    DisplayEmployee(employee);
                })
                .OnBadRequest<DummyResponse<object>>((result, statusCode, reasonPhrase) =>
                {
                    Console.WriteLine(result.Message);
                })
                .SendAsync();
        }

        static async Task DeleteEmployee()
        {
            Console.Write("Enter employee ID: ");

            int id = int.Parse(Console.ReadLine());

            await Client.Delete($"{BaseUri}/delete/{id}")
                .Always<DummyResponse<DeleteEmployeeResponse>>((result, statusCode) =>
                {
                    Console.WriteLine(result.Message);
                })
                .SendAsync();
        }
    }
}
