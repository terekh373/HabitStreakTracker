using HabitStreakTracker.Models;
using HabitStreakTracker.Repositories;

namespace HabitStreakTracker
{
    public class Program
    {
        static void Main()
        {
            Console.WriteLine("Welcome to Habit Streak Tracker!");

            InMemoryHabitRepository service = new InMemoryHabitRepository();

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
                Console.WriteLine("| 8. Show longest streak     |");
                Console.WriteLine("| 9. CompletionRateLast7Days |");
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
                        if (!TryReadHabitName(out var name1)) break;
                        Console.WriteLine(">    Enter the type (0 - Daily, 1 - Weekly, 2 - Monthly): ");
                        if (!int.TryParse(Console.ReadLine(), out int habitType) || habitType < 0 || habitType > 2)
                        {
                            Console.WriteLine("     Invalid habit type.");
                            break;
                        }
                        Console.WriteLine(">    Enter the description: ");
                        string? habitDescription = Console.ReadLine();
                        service.Add(new Habit(name1, (HabitType)habitType, habitDescription));
                        break;
                    case 2:
                        if (!TryReadHabitName(out var name2)) break;
                        var habit2 = service.Remove(name2);
                        if (habit2)
                        {
                            Console.WriteLine("     Habit removed successfully.");
                        }
                        else Console.WriteLine("     Habit not found.");
                        break;
                    case 3:
                        if (!TryReadHabitName(out var name)) break;
                        var habit3 = service.Get(name);
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
                        if (!TryReadHabitName(out var name6)) break;
                        var habit = service.Get(name6);
                        if (habit != null)
                        {
                            habit.MarkDoneToday();
                            Console.WriteLine("     Habit marked as done for today.");
                        }
                        else Console.WriteLine("     Habit not found.");
                        break;
                    case 7:
                        if (!TryReadHabitName(out var name7)) break;
                        int? streak = service.Get(name7)?.CurrentStreak();
                        if (streak.HasValue)
                        {
                            Console.WriteLine($"     Current streak for '{name7}': {streak.Value} days.");
                        }
                        else Console.WriteLine("     Habit not found.");
                        break;
                    case 8:
                        if (!TryReadHabitName(out var name8)) break;
                        var habit8 = service.Get(name8);
                        if (habit8 != null)
                            Console.WriteLine($"     Longest streak for '{name8}': {habit8.LongestStreak()} days.");
                        else Console.WriteLine("     Habit not found.");
                        break;
                    case 9:
                        if (!TryReadHabitName(out var name9)) break;
                        var habit9 = service.Get(name9);
                        if (habit9 != null)
                            Console.WriteLine($"     Completion (7 days) for '{name9}': {habit9.CompletionRateLast7Days():F0}%");
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

        static bool TryReadHabitName(out string name)
        {
            Console.WriteLine(">    Enter the name of the habit: ");
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("     Invalid habit name.");
                name = string.Empty;
                return false;
            }
            name = input.Trim();
            return true;
        }
    }
}
