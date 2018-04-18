using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Rendering.Displays
{
    [Serializable]
    public abstract class DisplayItemList<T> : Display
    {
        protected bool AllowScrolling { get; set; }

        private const int columnSpacing = 2;

        private T[] itemsArray;
        private string title;
        private bool hasTopBar;

        protected int selectedIndex = 0;
        private int viewOffset = 0;
        private DisplayItemListMode displayItemListMode;
        private int possibleItemsToRender;

        protected DisplayItemList(Display previousDisplay, T[] itemsArray, string title, bool hasTopBar, DisplayItemListMode displayItemListMode) : base(previousDisplay)
        {
            this.itemsArray = itemsArray;
            this.title = title;
            this.hasTopBar = hasTopBar;
            this.displayItemListMode = displayItemListMode;
            possibleItemsToRender = (int)Math.Ceiling(((double)Height - 6) / 2) - (hasTopBar ? 1 : 0);
            AllowScrolling = true;
        }

        public override Display Run()
        {
            if (GetListItems() == null)
            {
                return previousDisplay;
            }

            ConsoleKey read = ReadKey();
            if (read == ConsoleKey.UpArrow && AllowScrolling)
            {
                ScrollUp();
            }
            else if (read == ConsoleKey.DownArrow && AllowScrolling)
            {
                ScrollDown();
            }
            return RunItemList(read);
        }

        private Display ScrollUp()
        {
            if (displayItemListMode == DisplayItemListMode.ItemMode)
            {
                if (selectedIndex > 0)
                {
                    if (selectedIndex == -viewOffset)
                    {
                        viewOffset++;
                    }
                    selectedIndex--;
                }
            }
            else if (displayItemListMode == DisplayItemListMode.ScrollMode)
            {
                if (viewOffset < 0)
                {
                    viewOffset++;
                }
            }
            return this;
        }

        private Display ScrollDown()
        {
            if (displayItemListMode == DisplayItemListMode.ItemMode)
            {
                if (selectedIndex < GetListItems().Length - 1)
                {
                    selectedIndex++;
                    if (selectedIndex + viewOffset == possibleItemsToRender)
                    {
                        viewOffset--;
                    }
                }
            }
            else if (displayItemListMode == DisplayItemListMode.ScrollMode)
            {
                if (viewOffset > -(GetListItems().Length - possibleItemsToRender))
                {
                    viewOffset--;
                }
            }
            return this;
        }

        protected abstract Display RunItemList(ConsoleKey read);

        protected override void RenderDisplay()
        {
            if (GetListItems() == null)
            {
                return;
            }

            prefabs.RenderMenuBorder(title);
            if (hasTopBar)
            {
                DrawResource("menuBorderHorizontalLine", 0, 4);
            }
            DrawResource("menuBorderHorizontalLine", 0, Height - 3);
            RenderList();
            RenderItemList();
        }

        private void RenderList()
        {
            int offset = hasTopBar ? 5 : 3;

            int slotsToRender = (possibleItemsToRender + viewOffset >= GetListItems().Length) ? GetListItems().Length - viewOffset : possibleItemsToRender;

            StringBuilder columnTitles = new StringBuilder();

            foreach (ItemStringData itemStringData in ProvideTextForItem(default(T), -1))
            {
                AppendShortenStringWithSpacing(columnTitles, itemStringData.Title, itemStringData.Length);
            }

            int displayItemListModeOffset = 1;
            if (displayItemListMode == DisplayItemListMode.ItemMode)
            {
                displayItemListModeOffset = 2;
            }

            Write(columnTitles.ToString(), 1 + displayItemListModeOffset, 3, ConsoleColor.White);

            for (int i = 0; i < slotsToRender; i++)
            {
                int y = offset + (i * 2);
                int itemIndex = i - viewOffset;
                bool selected = itemIndex == selectedIndex;
                T item = GetListItems()[itemIndex];
                string itemText = GenerateItemText(item, itemIndex);

                if (displayItemListMode == DisplayItemListMode.ItemMode)
                {
                    Write($"{(selected ? ">" : " ")} {itemText}", 1, y);
                }
                else if (displayItemListMode == DisplayItemListMode.ScrollMode)
                {
                    Write($"{itemText}", 2, y);
                }
                RenderItemStringDecoration(item, itemIndex, selected, y);
            }
        }

        private string GenerateItemText(T item, int itemIndex)
        {
            StringBuilder itemText = new StringBuilder();

            foreach (ItemStringData itemStringData in ProvideTextForItem(item, itemIndex))
            {
                if (item != null)
                {
                    AppendShortenStringWithSpacing(itemText, itemStringData.Content, itemStringData.Length);
                }
                else
                {
                    AppendShortenStringWithSpacing(itemText, itemStringData.Empty, itemStringData.Length);
                }
            }
            return itemText.ToString();
        }

        private void AppendShortenStringWithSpacing(StringBuilder stringBuilder, string str, int length)
        {
            bool shouldStringBeShortened = length < str.Length;
            stringBuilder.Append(str.Substring(0, shouldStringBeShortened ? length - 1 : str.Length));
            stringBuilder.Append(shouldStringBeShortened ? "\u2026" : "");

            int renderedStringLength = shouldStringBeShortened ? length : str.Length;
            stringBuilder.Append(' ', (length - renderedStringLength) + columnSpacing);
        }

        protected abstract ItemStringData[] ProvideTextForItem(T item, int itemIndex);

        protected abstract void RenderItemList();

        protected abstract void RenderItemStringDecoration(T item, int index, bool selected, int y);

        protected virtual T[] GetListItems()
        {
            return itemsArray;
        }

        public class ItemStringData
        {
            public string Title { get; }
            public int Length { get; }
            public string Content { get; }
            public string Empty { get; }

            public ItemStringData(string title, int length, string content, string empty = "")
            {
                Title = title;
                Length = length;
                Content = content;
                Empty = empty;
            }
        }

        public enum DisplayItemListMode
        {
            ItemMode,
            ScrollMode
        }
    }
}
