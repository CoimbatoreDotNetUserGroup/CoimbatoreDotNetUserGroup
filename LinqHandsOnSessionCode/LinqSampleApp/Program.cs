using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqSampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            FunctionsInLinq();

            Console.Read();
        }

        public static void BasicEntitySample()
        {
            var employees = new SampleData().GetEmployees();

            var empdata = employees.Where(e => e.Department == "Sales");
        }

        public static void FirstVsFirstOrDefault()
        {
            var employees = new SampleData().GetEmployees();

            ////throws error
            //var empdata = employees.Where(e => e.EmployeeId == 200).First();


            var empdata1 = employees.Where(e => e.EmployeeId == 200).FirstOrDefault();

            if (empdata1 == null)
            {
                Console.WriteLine("Employee not found");
            }

            Console.Read();
        }

        public static void AnonymousTypeProjection()
        {
            var employees = new SampleData().GetEmployees();

            var empdata = from e in employees
                          select new { e.EmployeeName, e.Department };


            //do some code

        }

        public static void GroupedEmployees()
        {
            var employees = new SampleData().GetEmployees();

            var departmentsAndItsEmps = from e in employees
                                        group e by e.Department into groupedData
                                        select new { Department = groupedData.Key, Employees = groupedData };

            foreach (var oneDeptAndItsEmployees in departmentsAndItsEmps)
            {
                Console.WriteLine("-------------------------------------------------------------------------");
                Console.WriteLine("Department: " + oneDeptAndItsEmployees.Department);
                foreach (var empsInDept in oneDeptAndItsEmployees.Employees)
                {
                    Console.WriteLine("Employee Name: " + empsInDept.EmployeeName);

                }
            }

            Console.Read();
        }

        public static void IndexedQueries()
        {
            var employees = new SampleData().GetEmployees();

            //var selectedEmps = employees.Where(emp => emp.Department == "Sales");
            var selectedEmps = employees.Where((emp, index) => ((index != 0) ? true : false));

            foreach (var emp in selectedEmps)
            {
                Console.WriteLine("Employee Name: " + emp.EmployeeName);

            }
            Console.Read();

        }



        public static void DeferredQueryExecution()
        {
            var employees = new SampleData().GetEmployees();

            float increment = 0;

            increment = 1;//wont apply

            var empdata = from e in employees
                          select new { e.EmployeeName, e.Salary, NewSalary = (e.Salary + increment) };

            increment = 2000;//this will apply

            foreach (var emp in empdata)
            {
                Console.WriteLine("-------------------------------------------------------------------------");
                Console.WriteLine("Employee Name: " + emp.EmployeeName);
                Console.WriteLine("Employee Old Salary: " + emp.Salary);
                Console.WriteLine("Employee New Salary: " + emp.NewSalary);
            }

            Console.Read();
        }


        public static void FunctionsInLinq()
        {
            var employees = new SampleData().GetEmployees();

            var empdata = from e in employees
                          select new { e.EmployeeId, e.EmployeeName, e.Salary, Tax = CalculateTax(e) };

            foreach (var emp in empdata)
            {
                Console.WriteLine("-------------------------------------------------------------------------");
                Console.WriteLine("Employee Name: " + emp.EmployeeName);
                Console.WriteLine("Employee Salary: " + emp.Salary);
                Console.WriteLine("Employee Tax: " + emp.Tax);
            }
            Console.Read();

        }

        private static float CalculateTax(Employee e)
        {
            //do some complex tax calculations, but now nothing is done
            return e.Salary * 10 / 100;
        }

    }

    class SampleData
    {
        public IList<Employee> GetEmployees()
        {
            var emps = new List<Employee>();
            emps.Add(new Employee() { EmployeeId = 1, EmployeeName = "Arnold Schwarzenegger", Salary = 63000, Department = "Sales" });
            emps.Add(new Employee() { EmployeeId = 2, EmployeeName = "Liam Neeson", Salary = 30000, Department = "Support" });
            emps.Add(new Employee() { EmployeeId = 3, EmployeeName = "Bruce Willis", Salary = 20000, Department = "Sales" });
            emps.Add(new Employee() { EmployeeId = 4, EmployeeName = "Denzel Washington", Salary = 73000, Department = "Marketing" });
            emps.Add(new Employee() { EmployeeId = 5, EmployeeName = "Ed Harris", Salary = 61000, Department = "Sales" });
            emps.Add(new Employee() { EmployeeId = 6, EmployeeName = "William Dafoe", Salary = 86000, Department = "Support" });
            emps.Add(new Employee() { EmployeeId = 7, EmployeeName = "Michael Douglas", Salary = 97000, Department = "Marketing" });
            emps.Add(new Employee() { EmployeeId = 8, EmployeeName = "Will Ferrell", Salary = 105000, Department = "Support" });
            emps.Add(new Employee() { EmployeeId = 9, EmployeeName = "Sylvester Stallone", Salary = 189000, Department = "Marketing" });
            emps.Add(new Employee() { EmployeeId = 10, EmployeeName = "Ben Stiller", Salary = 78000, Department = "Sales" });
            return emps;
        }
    }

    class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public float Salary { get; set; }
    }
}
