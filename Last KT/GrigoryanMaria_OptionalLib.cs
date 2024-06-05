using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrigoryanMaria_OptionalLib
{
    public class IncorrectOptionalAccessException : InvalidOperationException
    {
        public IncorrectOptionalAccessException() { }
        public IncorrectOptionalAccessException(string message) : base(message) { }
        public IncorrectOptionalAccessException(string message, Exception innerException) : base(message, innerException) { }
    }

    public interface IOptional<T> where T : struct
    {
        T Value { get; set; }
        void SetValue(T? value);
        T GetValueOrDefault();
        bool Empty { get; }
    }

    public class Optional<T> : IOptional<T> where T : struct
    {
        private T? _value;

        public Optional()
        {
            _value = null;
        }

        public Optional(T value)
        {
            _value = value;
        }

        public T Value
        {
            get
            {
                if (Empty)
                {
                    throw new IncorrectOptionalAccessException("Параметр не имеет значения.");
                }

                return (T)_value;
            }
            set
            {
                _value = value;
            }
        }

        public void SetValue(T? value)
        {
            if (value.HasValue)
            {
                _value = value.Value;
            }
            else
            {
                _value = null;
            }
        }

        public T GetValueOrDefault()
        {
            return Empty ? default(T) : (T)_value;
        }

        public bool Empty => !_value.HasValue;

        public override string ToString()
        {
            return Empty ? "empty" : Value.ToString();
        }
    }

    public class ExtendedOptional<T> : Optional<T> where T : struct
    {
        public event Action<T> OnOptionalFilled;
        public event Action OnOptionalEmptied;

        public ExtendedOptional() : base() { }
        public ExtendedOptional(T value) : base(value) { }

        public new T Value
        {
            get { return base.Value; }
            set
            {
                base.Value = value;
                if (!Empty)
                {
                    OnOptionalFilled?.Invoke(value);
                }
                else
                {
                    OnOptionalEmptied?.Invoke();
                }
            }
        }
    }
}
