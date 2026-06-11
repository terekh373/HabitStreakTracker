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
        private readonly List<DateOnly> _dates = new();
        public IReadOnlyList<DateOnly> Dates => _dates;

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
            _dates = new List<DateOnly>();
        }


        override public string ToString() => $"{Name} ({Type}) - {CurrentStreak()} day streak, Dates marked as done: {string.Join(", ", Dates)}";


        public void MarkDoneToday()
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            if (!_dates.Contains(today)) _dates.Add(today);
        }
        public int CurrentStreak() => _dates.Count;
    }
}
