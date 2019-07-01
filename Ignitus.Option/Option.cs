using System.Collections.Generic;

namespace Ignitus.Option
{
    public abstract class Option
    {
        public static Option Optionify<T>(T item)
        {
            if(item != null)
            {
                return new Some<T>(item);
            }

            return new None();
        }
    }

    public class Some<T> : Option
    {
        public T Value { get; private set; }

        public Some(T val)
        {
            Value = val;
        }

        public override bool Equals(object obj)
        {
            bool result = false;

            if (obj is Some<T>)
            {
                if ((obj as Some<T>).Value.Equals(Value))
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

    public class None : Option
    {
        public None(){ }

        public override bool Equals(object obj)
        {
            bool result = false;

            if (obj is None)
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
