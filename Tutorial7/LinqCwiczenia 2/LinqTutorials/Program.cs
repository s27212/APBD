using System;

namespace LinqTutorials
{
    class Program
    {
        static void Main(string[] args)
        {
            // Task 1
            Console.WriteLine("Task 1: Backend programmers");
            var task1Result = LinqTasks.Task1();
            foreach (var emp in task1Result)
            {
                Console.WriteLine(emp);
            }

            // Task 2
            Console.WriteLine("\nTask 2: Frontend programmers with Salary > 1000 ordered by Ename descending");
            var task2Result = LinqTasks.Task2();
            foreach (var emp in task2Result)
            {
                Console.WriteLine(emp);
            }

            // Task 3
            Console.WriteLine("\nTask 3: Max salary");
            var task3Result = LinqTasks.Task3();
            Console.WriteLine($"Max Salary: {task3Result}");

            // Task 4
            Console.WriteLine("\nTask 4: Employees with max salary");
            var task4Result = LinqTasks.Task4();
            foreach (var emp in task4Result)
            {
                Console.WriteLine(emp);
            }

            // Task 5
            Console.WriteLine("\nTask 5: Employee names and jobs");
            dynamic task5Result = LinqTasks.Task5();
            foreach (var item in task5Result)
            {
                Console.WriteLine($"Surname: {item.Surname}, Job: {item.Job}");
            }

            // Task 6
            Console.WriteLine("\nTask 6: Joined Employee and Department details");
            dynamic task6Result = LinqTasks.Task6();
            foreach (var item in task6Result)
            {
                Console.WriteLine($"Emp: {item.Ename}, Job: {item.Job}, Dept: {item.Dname}");
            }

            // Task 7
            Console.WriteLine("\nTask 7: Job and Employee Count");
            dynamic task7Result = LinqTasks.Task7();
            foreach (var item in task7Result)
            {
                Console.WriteLine($"Job: {item.Job}, Count: {item.Count}");
            }

            // Task 8
            Console.WriteLine("\nTask 8: Any Backend programmer exists?");
            var task8Result = LinqTasks.Task8();
            Console.WriteLine($"Result: {task8Result}");

            // Task 9
            Console.WriteLine("\nTask 9: Latest Frontend programmer");
            var task9Result = LinqTasks.Task9();
            if (task9Result != null)
            {
                Console.WriteLine(task9Result);
            }

            // Task 10
            Console.WriteLine("\nTask 10: Employees and a No value entry");
            dynamic task10Result = LinqTasks.Task10();
            foreach (var item in task10Result)
            {
                Console.WriteLine($"Ename: {item.Ename}, Job: {item.Job}, HireDate: {item.HireDate}");
            }

            // Task 11
            Console.WriteLine("\nTask 11: Departments with more than 1 employee");
            dynamic task11Result = LinqTasks.Task11();
            foreach (var item in task11Result)
            {
                Console.WriteLine($"Name: {item.Name}, NumberOfEmployees: {item.numOfEmployees}");
            }

            // Task 12
            Console.WriteLine("\nTask 12: Employees with subordinates");
            var task12Result = LinqTasks.Task12();
            foreach (var emp in task12Result)
            {
                Console.WriteLine(emp);
            }

            // Task 13
            Console.WriteLine("\nTask 13: Number appearing an odd number of times");
            int[] array = { 1, 1, 1, 1, 1, 1, 10, 1, 1, 1, 1 };
            var task13Result = LinqTasks.Task13(array);
            Console.WriteLine($"Result: {task13Result}");

            // Task 14
            Console.WriteLine("\nTask 14: Departments with exactly 5 or no employees");
            var task14Result = LinqTasks.Task14();
            foreach (var dept in task14Result)
            {
                Console.WriteLine($"Dept: {dept.Dname}");
            }

            // Task 15
            Console.WriteLine("\nTask 15: Jobs with more than 2 employees containing 'A'");
            dynamic task15Result = LinqTasks.Task15();
            foreach (var item in task15Result)
            {
                Console.WriteLine($"Job: {item.Job}, NumberOfEmployees: {item.NumberOfEmployees}");
            }

            // Task 16
            Console.WriteLine("\nTask 16: Cartesian product of Emps and Depts");
            dynamic task16Result = LinqTasks.Task16();
            foreach (var item in task16Result)
            {
                Console.WriteLine($"Emp: {item.emp.Ename}, Dept: {item.dept.Dname}");
            }
        }
    }
}
