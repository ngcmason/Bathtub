/// <summary>
/// This is the class each bot will actually derive from.
/// </summary>
public abstract class Bot
{
    public abstract BotOutput think(BotInput input);
}

/// <summary>
/// Contains all the information bots are given each time they are polled.
/// </summary>
public abstract class BotInput {}

/// <summary>
/// Contains all the information bots should return each time they are polled. 
/// </summary>
public abstract class BotOutput {}