using System.Collections.Generic;

namespace Ignitus.Option
{
    public abstract class Option<T>
    {
        protected T value;   
    }

    public class Some<T> : Option<T>
    {
        public Some(T val)
        {
            value = val;
        }

        public T GetValue()
        {
            return value;
        }

        public override bool Equals(object obj)
        {
            bool result = true;

            if (obj is Some<T>)
            {
                if (!(obj as Some<T>).GetValue().Equals(GetValue()))
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }

            return result;
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override string ToString()
        {
            return $"Some<{typeof(T).Name}>({value})";
        }
    }

    public class None<T> : Option<T>
    {
        public None()
        {
            value = default(T);
        }

        public override bool Equals(object obj)
        {
            bool result = true;

            if (!(obj is None<T>))
            {
                result = false;
            }

            return result;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public override string ToString()
        {
            return $"None<{typeof(T).Name}>";
        }        
    }
}
