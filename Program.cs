using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace console_todo_list
{
    internal class Program
    {
        public class Task
        {
            public string Name { get; set; }
            public bool Completed { get; set; }

            public Task(string name)
            {
                Name = name;
                Completed = false;
            }

            public override string ToString()
            {
                return $"{Name}: {(Completed ? "Completed" : "Pending..")}";
            }
        }

        public class TaskList
        {
            static void Main(string[] args)
            {
                List<Task> tasks = new List<Task>();
                string filename = "task.txt";
                LoadTasks(tasks, filename);

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("\nTask List");
                    Console.WriteLine("1. Add Task");
                    Console.WriteLine("2. Mark Task as complete");
                    Console.WriteLine("3. View Tasks");
                    Console.WriteLine("4. Save Tasks to file");
                    Console.WriteLine("5. Load Tasks from file");
                    Console.WriteLine("6. Remove a task");
                    Console.WriteLine("7. Exit the apllication");

                    Console.Write("\nEnter your choice: ");
                    string choice = Console.ReadLine();

                    Console.Clear();

                    switch (choice)
                    {
                        case "1":
                            AddTask(tasks);
                            break;
                        case "2":
                            MarkTaskCompleted(tasks);
                            break;
                        case "3":
                            ViewTasks(tasks); 
                            break;
                        case "4":
                            SaveTasks(tasks, filename); 
                            break;
                        case "5":
                            LoadTasks(tasks, filename);
                            break;
                        case "6":
                            RemoveTask(tasks);
                            break;
                        case "7":
                            return;
                        default:
                            Console.WriteLine("Invalid choice");
                            break;
                    }

                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }

            static void AddTask(List<Task> tasks)
            {
                Console.Write("Enter task name: ");
                string name = Console.ReadLine();
            }
        }       

    }
}
