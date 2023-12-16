using System;
using UnityEngine;

public class GameEnvironment
{
    public float Temperature { get; private set; }
    public WeatherType CurrentWeather { get; private set; }
    public float RadiationLevel { get; private set; }
    public TimeOfDay CurrentTimeOfDay { get; private set; }
    public float AirQuality { get; private set; }
    public bool IsIndoors { get; private set; }
    public float WindSpeed { get; private set; }
    public bool IsRaining { get; private set; }
    public float Humidity { get; private set; }

    // Additional properties for detailed environmental simulation
    public float NoiseLevel { get; private set; }
    public Vector3 WindDirection { get; private set; }

    // Environmental change rates (can be adjusted to control the speed of change)
    private const float TemperatureChangeRate = 0.1f;
    private const float RadiationChangeRate = 0.05f;
    private const float AirQualityChangeRate = 0.1f;

    // Constructor
    public GameEnvironment()
    {
        // Initialize with default values
        Temperature = 20f; // Room temperature in Celsius
        CurrentWeather = WeatherType.Clear;
        RadiationLevel = 0f;
        CurrentTimeOfDay = TimeOfDay.Day;
        AirQuality = 1f; // Scale from 0 (worst) to 1 (best)
        IsIndoors = false;
        WindSpeed = 5f; // Default wind speed
        IsRaining = false;
        Humidity = 50f; // Default humidity percentage
        NoiseLevel = 0f;
        WindDirection = Vector3.forward;
    }

    // Update the environment (e.g., called every frame or on a time interval)
    public void UpdateEnvironment(float deltaTime)
    {
        // Implement logic to update environmental factors
        // This can be based on a game timer, player actions, random events, etc.
        SimulateDayNightCycle(deltaTime);
        SimulateWeatherChanges(deltaTime);

        // Example: Dynamically adjust temperature based on time of day and weather
        AdjustTemperature(deltaTime);
        AdjustRadiationLevels(deltaTime);
        AdjustAirQuality(deltaTime);

        // Additional environmental simulation logic...
    }

    private void SimulateDayNightCycle(float deltaTime)
    {
        // Logic for changing the time of day
        // Example: Advance time of day based on deltaTime and switch between dawn, day, dusk, night
    }

    private void SimulateWeatherChanges(float deltaTime)
    {
        // Logic for changing weather conditions
        // Example: Randomly change weather conditions over time
    }

    private void AdjustTemperature(float deltaTime)
    {
        // Example logic to increase/decrease temperature
        Temperature += CurrentWeather == WeatherType.Sunny ? TemperatureChangeRate * deltaTime : -TemperatureChangeRate * deltaTime;
        Temperature = Mathf.Clamp(Temperature, -10f, 40f); // Example temperature bounds
    }

    private void AdjustRadiationLevels(float deltaTime)
    {
        // Example logic to adjust radiation levels
        RadiationLevel += RadiationChangeRate * deltaTime;
        RadiationLevel = Mathf.Clamp(RadiationLevel, 0f, 10f); // Example radiation level bounds
    }

    private void AdjustAirQuality(float deltaTime)
    {
        // Example logic to improve/worsen air quality
        AirQuality += AirQualityChangeRate * deltaTime;
        AirQuality = Mathf.Clamp(AirQuality, 0f, 1f); // Air quality bounds
    }

    // Methods to set or modify environmental factors
    public void SetTemperature(float newTemperature) { /* ... */ }
    public void SetWeather(WeatherType weather) { /* ... */ }
    public void SetRadiationLevel(float newRadiationLevel) { /* ... */ }
    public void SetTimeOfDay(TimeOfDay timeOfDay) { /* ... */ }
    public void SetAirQuality(float newAirQuality) { /* ... */ }
    public void SetIndoors(bool indoors) { /* ... */ }
    public void SetWindSpeed(float speed) { /* ... */ }
    public void SetRain(bool isRaining) { /* ... */ }
    public void SetHumidity(float humidity) { /* ... */ }

    // Additional methods for specific environmental interactions
    // ...

    // Enums for weather and time of day
    public enum WeatherType
    {
        Clear,
        Rainy,
        Stormy,
        Snowy,
        Foggy,
        Sunny
    }

    public enum TimeOfDay
    {
        Dawn,
        Day,
        Dusk,
        Night
    }
}



