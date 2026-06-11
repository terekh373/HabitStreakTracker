using HabitStreakTracker.Models;
using HabitStreakTracker.Services;

namespace HabitStreakTracker
{
    public class Program
    {
        static void Main()
        {
            Console.WriteLine("Welcome to Habit Streak Tracker!");

            HabitRepositoryService service = new HabitRepositoryService();
            string habitName = string.Empty;

            while (true)
            {
                Console.WriteLine("------------------------------");
                Console.WriteLine("| Please choose an option:   |");
                Console.WriteLine("| 1. Add a new habit         |");
                Console.WriteLine("| 2. Remove a habit          |");
                Console.WriteLine("| 3. Get a habit             |");
                Console.WriteLine("| 4. Get all habits          |");
                Console.WriteLine("| 5. Find a habit            |");
                Console.WriteLine("| 6. Mark habit as done      |");
                Console.WriteLine("| 7. Show current streak     |");
                Console.WriteLine("| 0. Exit                    |");
                Console.WriteLine("------------------------------");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Enter a number: ");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Console.WriteLine(">    Enter the name of the habit: ");
                        habitName = Console.ReadLine();
                        Console.WriteLine(">    Enter the type of the habit (0 - Daily, 1 - Weekly, 2 - Monthly): ");
                        if (!int.TryParse(Console.ReadLine(), out int habitType))
                        {
                            Console.WriteLine("     Invalid input. Please enter a number.");
                            break;
                        }
                        else if (habitType < 0 || habitType > 2)
                        {
                            Console.WriteLine("     Invalid habit type.");
                            break;
                        }
                        Console.WriteLine(">    Enter the description of the habit: ");
                        string habitDescription = Console.ReadLine();

                        service.Add(new Habit(habitName, (HabitType)habitType, habitDescription));

                        break;
                    case 2:
                        Console.WriteLine(">    Enter the name of the habit: ");
                        habitName = Console.ReadLine() ?? string.Empty;
                        if (string.IsNullOrWhiteSpace(habitName))
                        {
                            Console.WriteLine("     Invalid habit name.");
                            break;
                        }
                        var habit2 = service.Remove(habitName);
                        if (habit2)
                        {
                            Console.WriteLine("     Habit removed successfully.");
                        }
                        else Console.WriteLine("     Habit not found.");
                        break;
                    case 3:
                        Console.WriteLine(">    Enter the name of the habit: ");
                        habitName = Console.ReadLine() ?? string.Empty;
                        if (string.IsNullOrWhiteSpace(habitName))
                        {
                            Console.WriteLine("     Invalid habit name.");
                            break;
                        }
                        var habit3 = service.Get(habitName);
                        if (habit3 != null)
                        {
                            Console.WriteLine(habit3);
                        }
                        else Console.WriteLine("     Habit not found.");
                        break;
                    case 4:
                        service.GetAll().ToList().ForEach(h => Console.WriteLine(h));
                        break;
                    case 5:
                        Console.WriteLine(">    Enter the search term: ");
                        string searchTerm = Console.ReadLine() ?? string.Empty;
                        if (string.IsNullOrWhiteSpace(searchTerm))
                        {
                            Console.WriteLine("     Invalid search term.");
                            break;
                        }
                        var foundHabits = service.Find(h => h.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
                        if (foundHabits.Count > 0)
                        {
                            foundHabits.ToList().ForEach(h => Console.WriteLine(h));
                        }
                        else Console.WriteLine("     No habits found matching the search term.");
                        break;
                    case 6:
                        Console.WriteLine(">    Enter the name of the habit: ");
                        habitName = Console.ReadLine() ?? string.Empty;
                        if (string.IsNullOrWhiteSpace(habitName))
                        {
                            Console.WriteLine("     Invalid habit name.");
                            break;
                        }
                        var habit = service.Get(habitName);
                        if (habit != null)
                        {
                            habit.MarkDoneToday();
                            Console.WriteLine("     Habit marked as done for today.");
                        }
                        else Console.WriteLine("     Habit not found.");
                        break;
                    case 7:
                        Console.WriteLine(">    Enter the name of the habit: ");
                        habitName = Console.ReadLine() ?? string.Empty;
                        if (string.IsNullOrWhiteSpace(habitName))
                        {
                            Console.WriteLine("     Invalid habit name.");
                            break;
                        }
                        int? streak = service.Get(habitName)?.CurrentStreak();
                        if (streak.HasValue)
                        {
                            Console.WriteLine($"     Current streak for '{habitName}': {streak.Value} days.");
                        }
                        else Console.WriteLine("     Habit not found.");
                        break;
                    case 0:
                        Console.WriteLine("It was nice to see you!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
