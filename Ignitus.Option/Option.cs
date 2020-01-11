namespace Ignitus.Option
{
    public abstract class Option<T> where T : notnull
    {
        public static Option<T> Optionify(T item)
        {
            if (item != null)
            {
                return new Some<T>(item);
            }

            return new None<T>();
        }

        public abstract bool IsSome();

        public abstract bool IsNone();

        public abstract T GetOrDefault(T value);
    }

    public class Some<T> : Option<T> where T : notnull
    {
        public T Value { get; private set; }

        public Some(T val)
        {
            Value = val;
        }

        public override bool IsSome()
        {
            return true;
        }

        public override bool IsNone()
        {
            return false;
        }

        public override T GetOrDefault(T value)
        {
            return Value;
        }

        public override bool Equals(object obj)
        {
            bool result = false;

            if (obj is Some<T> option)
            {
                if (option.Value.Equals(Value))
                {
                    result = true;
                }
            }

            return result;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return $"Some<{typeof(T).Name}>({Value})";
        }
    }

    public class None<T> : Option<T> where T : notnull
    {
        public None() { }


        public override bool IsNone()
        {
            return true;
        }

        public override bool IsSome()
        {
            return false;
        }

        public override T GetOrDefault(T value)
        {
            return value;
        }

        public override bool Equals(object obj)
        {
            bool result = false;

            if (obj is None<T>)
            {
                result = true;
            }

            return result;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public override string ToString()
        {
            return $"None";
        }
    }
}
