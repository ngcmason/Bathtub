using SFML.Graphics;

public struct GameState(string text, bool shouldContinue)
{
    /// <summary>
    /// If the game state should not continue, this text will be displayed in the banner above the grid.
    /// </summary>
    public string Text { get; } = text;
    /// <summary>
    /// Whether or not this gamestate should trigger another time step.
    /// If not, the game will display the provided text above the grid 
    /// in the banner and stop calling <see cref="Game.LogicStep"/>.
    /// </summary>
    public bool ShouldContinue { get; } = shouldContinue;
}