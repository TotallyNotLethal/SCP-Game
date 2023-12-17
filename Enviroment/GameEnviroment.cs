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

    public float NoiseLevel { get; private set; }
    public Vector3 WindDirection { get; private set; }

    private const float TemperatureChangeRate = 0.1f;
    private const float RadiationChangeRate = 0.05f;
    private const float AirQualityChangeRate = 0.1f;

    public GameEnvironment()
    {
        Temperature = 20f;
        CurrentWeather = WeatherType.Clear;
        RadiationLevel = 0f;
        CurrentTimeOfDay = TimeOfDay.Day;
        AirQuality = 1f;
        IsIndoors = false;
        WindSpeed = 5f;
        IsRaining = false;
        Humidity = 50f;
        NoiseLevel = 0f;
        WindDirection = Vector3.forward;
    }

    public void UpdateEnvironment(float deltaTime)
    {
        SimulateDayNightCycle(deltaTime);
        SimulateWeatherChanges(deltaTime);

        AdjustTemperature(deltaTime);
        AdjustRadiationLevels(deltaTime);
        AdjustAirQuality(deltaTime);
    }

    private void SimulateDayNightCycle(float deltaTime)
    {
    }

    private void SimulateWeatherChanges(float deltaTime)
    {
    }

    private void AdjustTemperature(float deltaTime)
    {
        Temperature += CurrentWeather == WeatherType.Sunny ? TemperatureChangeRate * deltaTime : -TemperatureChangeRate * deltaTime;
        Temperature = Mathf.Clamp(Temperature, -10f, 40f);
    }

    private void AdjustRadiationLevels(float deltaTime)
    {
        RadiationLevel += RadiationChangeRate * deltaTime;
        RadiationLevel = Mathf.Clamp(RadiationLevel, 0f, 10f);
    }

    private void AdjustAirQuality(float deltaTime)
    {
        AirQuality += AirQualityChangeRate * deltaTime;
        AirQuality = Mathf.Clamp(AirQuality, 0f, 1f);
    }

    public void SetTemperature(float newTemperature) { /* ... */ }
    public void SetWeather(WeatherType weather) { /* ... */ }
    public void SetRadiationLevel(float newRadiationLevel) { /* ... */ }
    public void SetTimeOfDay(TimeOfDay timeOfDay) { /* ... */ }
    public void SetAirQuality(float newAirQuality) { /* ... */ }
    public void SetIndoors(bool indoors) { /* ... */ }
    public void SetWindSpeed(float speed) { /* ... */ }
    public void SetRain(bool isRaining) { /* ... */ }
    public void SetHumidity(float humidity) { /* ... */ }

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



