using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PuzzleBoobleClone.GameElements
{
    public interface BoundsObserver
    {
        void OnOneRowRemoved(Bounds bound);
    }
}
