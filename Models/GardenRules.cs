namespace BlazorSprout.Models;

/// <summary>Thresholds that turn spending ratios into plants and weather.</summary>
public static class GardenRules
{
    public const decimal ThrivingBelow = 0.60m;
    public const decimal SunnyBelow = 0.80m;
    public const decimal OverBudgetAt = 1.00m;

    public static PlantHealth PlantHealthFor(decimal usageRatio) => usageRatio switch
    {
        >= OverBudgetAt => PlantHealth.Wilting,
        < ThrivingBelow => PlantHealth.Thriving,
        _ => PlantHealth.Growing
    };

    public static WeatherCondition WeatherFor(decimal usageRatio) => usageRatio switch
    {
        >= OverBudgetAt => WeatherCondition.Stormy,
        < SunnyBelow => WeatherCondition.Sunny,
        _ => WeatherCondition.Cloudy
    };

    public static TreeStage TreeStageFor(decimal progress) => progress switch
    {
        >= 1m => TreeStage.Bloomed,
        >= 0.80m => TreeStage.Mature,
        >= 0.40m => TreeStage.Young,
        > 0m => TreeStage.Sapling,
        _ => TreeStage.Seed
    };
}
