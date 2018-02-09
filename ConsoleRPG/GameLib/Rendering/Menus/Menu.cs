using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Rendering.Menus
{
    public abstract class Menu
    {
        protected int width;
        protected int height;
        protected int x;
        protected int y;
        protected Pxl[,] grid;

        public Menu(int width, int height, int x, int y)
        {
            this.width = width;
            this.height = height;
            this.x = x;
            this.y = y;
            grid = new Pxl[width, height];
        }


        public Pxl[,] Render()
        {
            Array.Clear(grid, 0, grid.Length);
            RenderMenu();
            return grid;
        }

        public abstract void RenderMenu();
    }
}
