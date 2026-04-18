using SFML.Window;
using SFML.Graphics;
using SFML.System;

Clock clock = new();
RenderWindow window = new RenderWindow(new VideoMode(new SFML.System.Vector2u(800, 600)), "");
window.Closed += (s, e) => window.Close();
window.Resized += (sender, e) =>
{
    var view = new View(new FloatRect(new Vector2f(0, 0), ((Vector2f)e.Size)));
    window.SetView(view);
};

// --------------------------------------------
// Replace the constructor here with an instance
// of the specific game that is being run
// --------------------------------------------
Game GameInstance = new(window);
// --------------------------------------------

GameState state = GameInstance.IntializeGame();

while (window.IsOpen)
{
    if (clock.ElapsedTime.AsMilliseconds() >= GameInstance.STEP_TIME && state.ShouldContinue)
    {
        clock.Restart();
        state = GameInstance.LogicStep();   
    }

    if (!state.ShouldContinue)
    {
        GameInstance.ShowBanner = true;
        GameInstance.DrawBanner(state.Text);
    }

    GameInstance.DrawCells();

    window.Display();
    window.DispatchEvents();
}