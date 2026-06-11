namespace HabitStreakTracker.Models
{
    public enum HabitType
	{
		Daily,
		Weekly,
		Monthly
    }

    public class Habit
    {
        private readonly HashSet<DateOnly> _dates = new();
        public IReadOnlyCollection<DateOnly> Dates => _dates;

        private string _name;
        public string Name
        {
            get { return _name; }
            set => _name = string.IsNullOrWhiteSpace(value) ? "Unnamed Habit" : value;
        }

        public HabitType Type { get; set; }

        public string? Description { get; set; }


        public Habit(string name, HabitType type, string? description)
        {
            Name = name;
            Type = type;
            Description = description;
        }


        override public string ToString() =>
            $"{Name} ({Type}) — стрік: {CurrentStreak()}, найдовший: {LongestStreak()}, " +
            $"за 7 днів: {CompletionRateLast7Days():F0}%";

        public void MarkDoneToday() => _dates.Add(DateOnly.FromDateTime(DateTime.Today));
        public int CurrentStreak()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);

            var day = _dates.Contains(today) ? today : today.AddDays(-1);

            int streak = 0;
            while (_dates.Contains(day)) { streak++; day = day.AddDays(-1); }
            return streak;
        }

        public int LongestStreak()
        {
            if (_dates.Count == 0) return 0;
            var days = _dates.Distinct().OrderBy(d => d).ToList();
            int longest = 1, current = 1;
            for (int i = 1; i < days.Count; i++)
            {
                if (days[i] == days[i - 1].AddDays(1)) current++;
                else current = 1;
                longest = Math.Max(longest, current);
            }
            return longest;
        }

        public double CompletionRateLast7Days()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            int done = Enumerable.Range(0, 7).Count(i => _dates.Contains(today.AddDays(-i)));
            return done / 7.0 * 100;
        }
    }
}
