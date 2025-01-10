using System.Collections.Generic;
using System.Linq;

namespace JDoddsNAIT.ThemedUI
{
    public class NavigatorStack<T>
    {
        public int Index { get; set; }
        private T[] _stack;

        public NavigatorStack(int size)
        {
            _stack = new T[size];
        }

        public void Push(T item)
        {
            _stack = Refresh().ToArray();
            for (int i = _stack.Length - 2; i >= 0; i--)
            {
                _stack[i + 1] = _stack[i];
            }
            _stack[0] = item;
        }

        private IEnumerable<T> Refresh()
        {
            for (int i = 0; i < _stack.Length; i++)
            {
                int idx = i + Index;
                yield return idx < _stack.Length
                    ? _stack[idx]
                    : default;
            }
            Index = 0;
        }
    }
}
