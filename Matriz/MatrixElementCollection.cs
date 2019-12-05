using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections.Concurrent;

namespace Matriz
{
    public class MatrixElementCollection : ConcurrentDictionary<int, MatrixElement>
    {

        

        public bool IsLocked(int key)
        {
            if (!TryGetValue(key, out MatrixElement elem))
                return true;

            return elem.IsLocked;
        }

        public void LockAllDirections(int X, int Y)
        {
            foreach (var item in this.Where(e => e.Value.X == X || e.Value.Y == Y))
            {
                item.Value.Lock();
            }
        }

        public void UnlockAllDirections(int X, int Y)
        {
            foreach (var item in this.Where(e => e.Value.X == X || e.Value.Y == Y))
            {
                item.Value.Unlock();
            }
        }

    }
}
