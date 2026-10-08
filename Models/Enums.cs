namespace BlazorSprout.Models;

/// <summary>Garden "forecast" shown in the weather banner (Home screen).</summary>
public enum WeatherCondition
{
    Sunny,   // under budget
    Cloudy,  // close to the cap
    Stormy   // over budget
}

/// <summary>How a category plant looks (Garden screen).</summary>
public enum PlantHealth
{
    Thriving,
    Growing,
    Wilting
}

/// <summary>Visual stage of a savings-goal tree (Goal Trees feature).</summary>
public enum TreeStage
{
    Seed,    // nothing saved yet
    Sapling, // under 40 %
    Young,   // under 80 %
    Mature,  // under 100 %
    Bloomed  // 100 % funded - gold sparkle
}
