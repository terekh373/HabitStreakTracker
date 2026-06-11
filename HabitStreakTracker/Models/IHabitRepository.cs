namespace HabitStreakTracker.Models
{
    public interface IHabitRepository
    {
        void Add(Habit habit);
        bool Remove(string name);
        Habit? Get(string name);
        IReadOnlyList<Habit> GetAll();
        IReadOnlyList<Habit> Find(Func<Habit, bool> predicate);
    }
}
