using System;

namespace Lab_7
{
    public abstract class Human : IHasName
    {
        public string Name { get; protected set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public bool IsMale { get; protected set; }

        protected Human(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        protected Human(string name, bool isMale) : this(name)
        {
            IsMale = isMale;
        }
    }
}
