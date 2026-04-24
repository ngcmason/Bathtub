using System.Dynamic;
using SFML.Graphics;
using SFML.System;

public class Game(RenderWindow window)
{
    // --------------------------------------------
    // Constants
    // --------------------------------------------
    /// <summary>
    /// Duration of each game step in milliseconds.
    /// </summary>
    public int STEP_TIME { get; } = 1000;
    /// <summary>
    /// Number of cells in each row of the game grid.
    /// </summary>
    protected static int NUM_COLUMNS { get; } = 16;
    /// <summary>
    /// Number of rows in the game grid.
    /// </summary>
    protected static int NUM_ROWS { get; } = 16;
    /// <summary>
    /// Minimum size of the vertical margin around the grid and banner in the window, measured in pixels.
    /// </summary>
    protected static float V_MARGIN = 20f;
    /// <summary>
    /// Minimum size of the horizontal margin around the grid in the window, measured in pixels.
    /// </summary>
    protected static float H_MARGIN = 20f;
    /// <summary>
    /// Minimum size of padding between each cell in the grid, measured in pixels.
    /// </summary>
    protected static float PADDING = 5f;
    /// <summary>
    /// This string determines the title displayed at the top of the window.
    /// </summary>
    protected static string TITLE = "Default Bathtub Game";
    /// <summary>
    /// Height of the banner above the grid, in pixels.
    /// </summary>
    protected static float BANNER_HEIGHT = 150f;
    /// <summary>
    /// Whether or not the grid should be moved down to make space for the banner.
    /// </summary>
    protected bool ShowBanner = false;
    /// <summary>
    /// This string determines which font file is loaded for the game's text.
    /// Make sure you are somehow actually adding the font to the output directory.
    /// The easiest way is by setting the file to automatically copy to the output directory
    /// in the .csproj file.
    /// </summary>
    protected static string fontName = "FreeMono.ttf";

    // --------------------------------------------
    // --------------------------------------------
    // --------------------------------------------

    protected virtual Cell[,] Cells { get; } = new Cell[NUM_COLUMNS, NUM_ROWS];
    protected RenderWindow Window { get; } = window;
    public static readonly Font Font = new(fontName);
    protected virtual Vector2f CellDimensions
    {
        get
        {
            float totalSpacingX = (NUM_COLUMNS - 1) * PADDING;
            float totalSpacingY = (NUM_ROWS - 1) * PADDING;

            float availableWidth = Window.Size.X - 2 * H_MARGIN;
            float availableHeight = Window.Size.Y - 2 * V_MARGIN - (ShowBanner ? BANNER_HEIGHT : 0);

            float cellWidth = (availableWidth - totalSpacingX) / NUM_COLUMNS;
            float cellHeight = (availableHeight - totalSpacingY) / NUM_ROWS;

            float cellSize = Math.Min(cellWidth, cellHeight);

            return new Vector2f(cellSize, cellSize);
        }
    }

    // --------------------------------------------
    // Methods each game should override
    // --------------------------------------------
    /// <summary>
    /// This method is called once at the beginning of 
    /// the game. It should ensure all the game's 
    /// state is properly set up to begin, including
    /// creating all cells and clearing the window.
    /// </summary>
    public virtual GameState IntializeGame()
    {
        ShowBanner = false;
        Window.Clear(Color.Black);
        Window.SetTitle(TITLE);
        for (int x = 0; x < NUM_COLUMNS; x++)
        {
            for (int y = 0; y < NUM_ROWS; y++)
            {
                Cells[x, y] = new();
            }
        }

        return new("", true);
    }

    /// <summary>
    /// This method is called once each time step while
    /// the gamestate is still ongoing. It should handle 
    /// all of the game's actual logic including polling 
    /// bots and assigning new states to cells.
    /// </summary>
    public virtual GameState LogicStep() { return new("", true); }

    /// <summary>
    /// This method is called every frame. By default, 
    /// it simply loops over each Cell and calls
    /// <see cref="Cell.Draw"/>.
    /// </summary>
    public virtual void DrawCells()
    {
        float cellSize = CellDimensions.X;

        float totalSpacingX = (NUM_COLUMNS - 1) * PADDING;
        float totalSpacingY = (NUM_ROWS - 1) * PADDING;

        float gridWidth = NUM_COLUMNS * cellSize + totalSpacingX;
        float gridHeight = NUM_ROWS * cellSize + totalSpacingY;

        float availableWidth = Window.Size.X - 2 * H_MARGIN;
        float availableHeight = Window.Size.Y - 2 * V_MARGIN - (ShowBanner ? BANNER_HEIGHT : 0);

        float offsetX = H_MARGIN + (availableWidth - gridWidth) / 2f;
        float offsetY = V_MARGIN + (ShowBanner ? BANNER_HEIGHT : 0) + (availableHeight - gridHeight) / 2f;

        for (int x = 0; x < NUM_COLUMNS; x++)
        {
            for (int y = 0; y < NUM_ROWS; y++)
            {
                float posX = offsetX + x * (cellSize + PADDING);
                float posY = offsetY + y * (cellSize + PADDING);

                Cells[x, y].Draw(
                    Window,
                    new Vector2f(posX, posY),
                    new Vector2f(cellSize, cellSize)
                );
            }
        }
    }

    /// <summary>
    /// This method is called when the game should no
    /// longer continue. It draws the provided text
    /// in the banner portion of the screen above the grid.
    /// </summary>
    /// <param name="content">The text to draw in the banner.</param>
    public virtual void DrawBanner(string content)
    {
        ShowBanner = true;
        float bannerWidth = Window.Size.X - 2 * H_MARGIN;
        float bannerHeight = BANNER_HEIGHT;

        float padding = 10f;

        uint fontSize = (uint)(bannerHeight * 0.6f);

        Text text = new(Font, content, fontSize)
        {
            FillColor = Color.White
        };

        FloatRect bounds = text.GetLocalBounds();

        if (bounds.Width > bannerWidth - 2 * padding)
        {
            float scale = (bannerWidth - 2 * padding) / bounds.Width;
            text.CharacterSize = (uint)(fontSize * scale);
            bounds = text.GetLocalBounds();
        }

        text.Origin = new Vector2f(
            bounds.Left + bounds.Width / 2f,
            bounds.Top + bounds.Height / 2f
        );

        text.Position = new Vector2f(
            H_MARGIN + bannerWidth / 2f,
            V_MARGIN + bannerHeight / 2f
        );

        Window.Draw(text);
    }

    // --------------------------------------------

}