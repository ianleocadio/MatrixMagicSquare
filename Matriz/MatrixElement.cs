using System;
using System.Collections.Generic;
using System.Text;

namespace Matriz
{
    public class MatrixElement
    {

        public int X { get; set; }
        public int Y { get; set; }

        public int Value { get; set; }

        public bool IsLocked { get; private set; }


        public static MatrixElement Create(int Value, int X, int Y, bool IsLocked)
        {
            return new MatrixElement
            {
                Value = Value,
                X = X,
                Y = Y,
                IsLocked = IsLocked
            };
        }

        public void Lock()
        {
            this.IsLocked = true;
        }

        public void Unlock()
        {
            this.IsLocked = false;
        }

        public override string ToString()
        {
            return $"[{X},{Y}] -> {IsLocked}";
        }
    }
}
