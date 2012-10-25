using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PuzzleBoobleClone.GameElements
{
    public interface HangingBallsObserver
    {
        void OnPlayerWins();

        void OnPlayerLoses();
    }
}
