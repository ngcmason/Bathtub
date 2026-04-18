using SFML.Graphics;
using SFML.System;

public class Cell
{
    /// <summary>
    /// By default, this is the color of the cell itself.
    /// </summary>
    public virtual Color BGColor { get; set; } = new Color(64, 64, 64);
    /// <summary>
    /// By default, this is the text to display on top of the cell.
    /// </summary>
    public virtual string Text { get; set; } = "";
    /// <summary>
    /// By default, this is the color of the cell's text.
    /// </summary>
    public virtual Color TextColor { get; set; } = Color.White;
    /// <summary>
    /// By default, this is the value to multiply the cell's height by to
    /// determine the cell's text's font size.
    /// </summary>
    public virtual float TextScaleFactor { get; set;} = 0.3f;

    /// <summary>
    /// Renders the cell to the screen. By default, it will draw a rectangle with the given
    /// dimensions and in the cell's background color with the cell's text on top in the cell's text color.
    /// </summary>
    /// <param name="window">The window the game is being rendered in.</param>
    /// <param name="position">Where (in screen coordinates) to position the cell.</param>
    /// <param name="dimensions">The dimensions of the cell (in pixels). Doesn't have to be square.</param>
    public virtual void Draw(RenderWindow window, Vector2f position, Vector2f dimensions)
    {
        RectangleShape rect = new(dimensions)
        {
            Position = position,
            FillColor = BGColor
        };

        window.Draw(rect);

        // Skip if no text
        if (string.IsNullOrEmpty(Text))
            return;

        // Scale font size based on cell size
        uint fontSize = (uint)(dimensions.Y * TextScaleFactor);

        Text text = new(Game.Font, Text, fontSize)
        {
            FillColor = TextColor
        };

        // Center text inside the cell
        FloatRect bounds = text.GetLocalBounds();

        text.Position = new Vector2f(
            position.X + (dimensions.X - bounds.Width) / 2f - bounds.Left,
            position.Y + (dimensions.Y - bounds.Height) / 2f - bounds.Top
        );

        window.Draw(text);
    }
}