using GameLib.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.GameCore
{
    public interface IGameHandler
    {
        void OnDisplayUpdate(Pxl[,] grid);
        ConsoleKeyInfo ReadKey();
        string ReadLine();
        void WaitMs(int ms);
    }
}
