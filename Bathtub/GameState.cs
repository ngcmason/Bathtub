using SFML.Graphics;

public struct GameState(string text, bool shouldContinue)
{
    public string Text { get; } = text;
    public bool ShouldContinue { get; } = shouldContinue;
}