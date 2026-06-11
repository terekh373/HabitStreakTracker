using HabitStreakTracker.Models;

namespace HabitStreakTracker.Services
{
    public class HabitRepositoryService : IHabitRepository
    {
        private List<Habit> _habits = new List<Habit>();


        public void Add(Habit habit)
        {
            _habits.Add(habit);
        }

        public IReadOnlyList<Habit> Find(Func<Habit, bool> predicate)
        {
            return _habits.Where(predicate).ToList();
        }

        public Habit? Get(string name)
        {
            return _habits.FirstOrDefault(h => h.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public IReadOnlyList<Habit> GetAll()
        {
            return _habits.ToList();
        }

        public bool Remove(string name)
        {
            var habit = Get(name);
            if (habit != null)
            {
                _habits.Remove(habit);
                return true;
            }
            return false;
        }
    }
}
