using SFML.Graphics;
using SFML.System;

public class Cell
{
    public virtual Color BGColor { get; set; } = new Color(64, 64, 64);
    public virtual string Text { get; set; } = "";
    public virtual Color TextColor { get; set; } = Color.White;
    public void Draw(RenderWindow window, Vector2f position, Vector2f dimensions)
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
        uint fontSize = (uint)(dimensions.Y * 0.3f); // tweak factor as needed

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