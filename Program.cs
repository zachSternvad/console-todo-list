using System;
using System.Collections.Generic;
using System.IO;
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
                    Console.WriteLine("7. Exit the aplication");

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

                tasks.Add(new Task(name));
                Console.WriteLine("Task added.");
            }

            static void MarkTaskCompleted(List<Task> tasks)
            {
                if (tasks.Count == 0)
                {
                    Console.WriteLine("No tasks available.");
                    return;
                }

                for (int i = 0; i < tasks.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {tasks[i]}");
                }

                Console.Write("Enter task number to mark as completed: ");
                if (int.TryParse(Console.ReadLine(), out int taskNumber) && taskNumber > 0 && taskNumber <= tasks.Count)
                {
                    tasks[taskNumber - 1].Completed = true;
                    Console.WriteLine("Task marked as completed.");
                }
                else
                {
                    Console.WriteLine("Invalid task number.");
                }
            }

            static void ViewTasks(List<Task> tasks)
            {
                if (tasks.Count == 0)
                {
                    Console.WriteLine("No tasks available.");
                    return;
                }

                var sortedTasks = tasks
                    .OrderBy(task => task.Completed)
                    .ToList();

                Console.WriteLine($"{"Task Name",-20}{"Status"}");
                Console.WriteLine("-----------------------------------------");


                foreach (var task in sortedTasks)
                {
                    Console.Write($"{task.Name,-20}");

                    if (task.Completed)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\t\tCompleted");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\t\tPending..");
                        Console.ResetColor();
                    }
                }
            }

            static void SaveTasks(List<Task> tasks, string filename)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(filename))
                    {
                        foreach (var task in tasks)
                        {
                            writer.WriteLine($"{task.Name},{task.Completed}");
                        }
                    }
                    Console.WriteLine("Task saved.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving tasks: {ex:Message}");
                }
            }

            static void LoadTasks(List<Task> tasks, string filename)
            {
                tasks.Clear();
                try
                {
                    if (File.Exists(filename))
                    {
                        using (StreamReader reader = new StreamReader(filename))
                        {
                            string line;
                            while ((line = reader.ReadLine()) != null)
                            {
                                string[] parts = line.Split(',');
                                if (parts.Length == 2)
                                {
                                    tasks.Add(new Task(parts[0]) { Completed = bool.Parse(parts[1]) });
                                }
                            }
                        }
                        Console.WriteLine("Tasks loaded.");
                    }
                    else
                    {
                        Console.WriteLine("No saved tasks found.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading tasks: {ex.Message}");
                }
            }

            static void RemoveTask(List<Task> tasks)
            {
                if (tasks.Count == 0)
                {
                    Console.WriteLine("No tasks available to remove.");
                    return;
                }

                for (int i = 0; i < tasks.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {tasks[i].Name} - Completed: {tasks[i].Completed}");
                }

                Console.Write("Enter the task number to remove: ");
                if (int.TryParse(Console.ReadLine(), out int taskNumber) && taskNumber > 0 && taskNumber <= tasks.Count)
                {
                    tasks.RemoveAt(taskNumber - 1);
                    Console.WriteLine("Task removed.");
                }
                else
                {
                    Console.WriteLine("Invalid task number.");
                }
            }
        }
    }
}

