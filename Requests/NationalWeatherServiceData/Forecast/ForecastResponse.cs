using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Requests.NationalWeatherServiceData.Forecast
{
    public class ForecastResponse
    {
        [JsonProperty(PropertyName = "@context")]
        public Context context { get; set; }
        [JsonProperty(PropertyName = "@id")]
        public string id { get; set; }
        [JsonProperty(PropertyName = "@type")]
        public string type { get; set; }
        public DateTime updateTime { get; set; }
        public string validTimes { get; set; }
        public string geometry { get; set; }
        public Elevation elevation { get; set; }
        public string forecastOffice { get; set; }
        public string gridId { get; set; }
        public string gridX { get; set; }
        public string gridY { get; set; }
        public Temperature temperature { get; set; }
        public Dewpoint dewpoint { get; set; }
        public MaxTemperature maxTemperature { get; set; }
        public MinTemperature minTemperature { get; set; }
        public RelativeHumidity relativeHumidity { get; set; }
        public ApparentTemperature apparentTemperature { get; set; }
        public HeatIndex heatIndex { get; set; }
        public WindChill windChill { get; set; }
        public SkyCover skyCover { get; set; }
        public WindDirection windDirection { get; set; }
        public WindSpeed windSpeed { get; set; }
        public WindGust windGust { get; set; }
        public Weather weather { get; set; }
        public Hazards hazards { get; set; }
        public ProbabilityOfPrecipitation probabilityOfPrecipitation { get; set; }
        public QuantitativePrecipitation quantitativePrecipitation { get; set; }
        public IceAccumulation iceAccumulation { get; set; }
        public SnowfallAmount snowfallAmount { get; set; }
        public SnowLevel snowLevel { get; set; }
        public CeilingHeight ceilingHeight { get; set; }
        public Visibility visibility { get; set; }
        public TransportWindSpeed transportWindSpeed { get; set; }
        public TransportWindDirection transportWindDirection { get; set; }
        public MixingHeight mixingHeight { get; set; }
        public HainesIndex hainesIndex { get; set; }
        public LightningActivityLevel lightningActivityLevel { get; set; }
        public TwentyFootWindSpeed twentyFootWindSpeed { get; set; }
        public TwentyFootWindDirection twentyFootWindDirection { get; set; }
        public WaveHeight waveHeight { get; set; }
        public WavePeriod wavePeriod { get; set; }
        public WaveDirection waveDirection { get; set; }
        public PrimarySwellHeight primarySwellHeight { get; set; }
        public PrimarySwellDirection primarySwellDirection { get; set; }
        public SecondarySwellHeight secondarySwellHeight { get; set; }
        public SecondarySwellDirection secondarySwellDirection { get; set; }
        public WavePeriod2 wavePeriod2 { get; set; }
        public WindWaveHeight windWaveHeight { get; set; }
        public DispersionIndex dispersionIndex { get; set; }
        public Pressure pressure { get; set; }
        public ProbabilityOfTropicalStormWinds probabilityOfTropicalStormWinds { get; set; }
        public ProbabilityOfHurricaneWinds probabilityOfHurricaneWinds { get; set; }
        public PotentialOf15mphWinds potentialOf15mphWinds { get; set; }
        public PotentialOf25mphWinds potentialOf25mphWinds { get; set; }
        public PotentialOf35mphWinds potentialOf35mphWinds { get; set; }
        public PotentialOf45mphWinds potentialOf45mphWinds { get; set; }
        public PotentialOf20mphWindGusts potentialOf20mphWindGusts { get; set; }
        public PotentialOf30mphWindGusts potentialOf30mphWindGusts { get; set; }
        public PotentialOf40mphWindGusts potentialOf40mphWindGusts { get; set; }
        public PotentialOf50mphWindGusts potentialOf50mphWindGusts { get; set; }
        public PotentialOf60mphWindGusts potentialOf60mphWindGusts { get; set; }
        public GrasslandFireDangerIndex grasslandFireDangerIndex { get; set; }
        public ProbabilityOfThunder probabilityOfThunder { get; set; }
        public DavisStabilityIndex davisStabilityIndex { get; set; }
        public AtmosphericDispersionIndex atmosphericDispersionIndex { get; set; }
        public LowVisibilityOccurrenceRiskIndex lowVisibilityOccurrenceRiskIndex { get; set; }
        public Stability stability { get; set; }
        public RedFlagThreatIndex redFlagThreatIndex { get; set; }
    }

    public class Geometry
    {
        [JsonProperty(PropertyName = "@id")]
        public string id { get; set; }
        [JsonProperty(PropertyName = "@type")]
        public string type { get; set; }
    }

    public class Distance
    {
        [JsonProperty(PropertyName = "@id")]
        public string id { get; set; }
        [JsonProperty(PropertyName = "@type")]
        public string type { get; set; }
    }

    public class Bearing
    {
        [JsonProperty(PropertyName = "@type")]
        public string type { get; set; }
    }

    public class Value
    {
        [JsonProperty(PropertyName = "@id")]
        public string id { get; set; }
    }

    public class Uom
    {
        [JsonProperty(PropertyName = "@id")]
        public string id { get; set; }
        [JsonProperty(PropertyName = "@type")]
        public string type { get; set; }
    }

    public class ForecastOffice
    {
        [JsonProperty(PropertyName = "@type")]
        public string type { get; set; }
    }

    public class ForecastGridData
    {
        [JsonProperty(PropertyName = "@type")]
        public string type { get; set; }
    }

    public class PublicZone
    {
        [JsonProperty(PropertyName = "@type")]
        public string type { get; set; }
    }

    public class County
    {
        [JsonProperty(PropertyName = "@type")]
        public string type { get; set; }
    }

    public class Context
    {
        public string wx { get; set; }
        public string s { get; set; }
        public string geo { get; set; }
        public string unit { get; set; }
        [JsonProperty(PropertyName = "@vocab")]
        public string vocab { get; set; }
        public Geometry geometry { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public Distance distance { get; set; }
        public Bearing bearing { get; set; }
        public Value value { get; set; }
        public Uom uom { get; set; }
        public ForecastOffice forecastOffice { get; set; }
        public ForecastGridData forecastGridData { get; set; }
        public PublicZone publicZone { get; set; }
        public County county { get; set; }
    }

    public class Elevation
    {
        public double value { get; set; }
        public string unitCode { get; set; }
    }

    public class TimedValue
    {
        public string validTime { get; set; }
        public double value { get; set; }
    }

    public class Temperature
    {
        public string sourceUnit { get; set; }
        public string uom { get; set; }
        public List<TimedValue> values { get; set; }
    }

    public class Dewpoint
    {
        public string sourceUnit { get; set; }
        public string uom { get; set; }
        public List<TimedValue> values { get; set; }
    }

    public class MaxTemperature
    {
        public string sourceUnit { get; set; }
        public string uom { get; set; }
        public List<TimedValue> values { get; set; }
    }


    public class MinTemperature
    {
        public string sourceUnit { get; set; }
        public string uom { get; set; }
        public List<TimedValue> values { get; set; }
    }


    public class RelativeHumidity
    {
        public string sourceUnit { get; set; }
        public string uom { get; set; }
        public List<TimedValue> values { get; set; }
    }

    public class ApparentTemperature
    {
        public string sourceUnit { get; set; }
        public string uom { get; set; }
        public List<TimedValue> values { get; set; }
    }

    public class HeatIndex
    {
        public string sourceUnit { get; set; }
        public string uom { get; set; }
        public List<TimedValue> values { get; set; }
    }

    public class WindChill
    {
        public string sourceUnit { get; set; }
        public string uom { get; set; }
        public List<TimedValue> values { get; set; }
    }

    public class SkyCover
    {
        public string sourceUnit { get; set; }
        public string uom { get; set; }
        public List<TimedValue> values { get; set; }
    }

    public class WindDirection
    {
        public string sourceUnit { get; set; }
        public string uom { get; set; }
        public List<TimedValue> values { get; set; }
    }

    public class WindSpeed
    {
        public string sourceUnit { get; set; }
        public string uom { get; set; }
        public List<TimedValue> values { get; set; }
    }

    public class WindGust
    {
        public string sourceUnit { get; set; }
        public string uom { get; set; }
        public List<TimedValue> values { get; set; }
    }

    public class WeatherEvent
    {
        public string coverage { get; set; }
        public string weather { get; set; }
        public string intensity { get; set; }
        public object visibility { get; set; }
        public object attributes { get; set; }
    }

    public class WeatherEventCollection
    {
        public string validTime { get; set; }
        public List<WeatherEvent> value { get; set; }
    }

    public class Weather
    {
        public List<WeatherEventCollection> values { get; set; }
    }

    public class HazardEvent
    {
        public string phenomenon { get; set; }
        public string significance { get; set; }
        public object event_number { get; set; }
    }

    public class HazardEventCollection
    {
        public string validTime { get; set; }
        public List<HazardEvent> value { get; set; }
    }

    public class Hazards
    {
        public List<HazardEventCollection> values { get; set; }
    }

    public class ProbabilityOfPrecipitation
    {
        public string sourceUnit { get; set; }
        public string uom { get; set; }
        public List<TimedValue> values { get; set; }
    }

    public class QuantitativePrecipitation
    {
        public string sourceUnit { get; set; }
        public string uom { get; set; }
        public List<TimedValue> values { get; set; }
    }

    public class IceAccumulation
    {
        public string sourceUnit { get; set; }
        public string uom { get; set; }
        public List<TimedValue> values { get; set; }
    }

    public class SnowfallAmount
    {
        public string sourceUnit { get; set; }
        public string uom { get; set; }
        public List<TimedValue> values { get; set; }
    }

    public class SnowLevel
    {
        public string sourceUnit { get; set; }
        public string uom { get; set; }
        public List<object> values { get; set; }
    }

    public class CeilingHeight
    {
        public string sourceUnit { get; set; }
        public string uom { get; set; }
        public List<TimedValue> values { get; set; }
    }

    public class Visibility
    {
        public string sourceUnit { get; set; }
        public string uom { get; set; }
        public List<TimedValue> values { get; set; }
    }

    public class TransportWindSpeed
    {
        public string sourceUnit { get; set; }
        public string uom { get; set; }
        public List<TimedValue> values { get; set; }
    }

    public class TransportWindDirection
    {
        public string sourceUnit { get; set; }
        public string uom { get; set; }
        public List<TimedValue> values { get; set; }
    }

    public class MixingHeight
    {
        public string sourceUnit { get; set; }
        public string uom { get; set; }
        public List<TimedValue> values { get; set; }
    }

    public class HainesIndex
    {
        public List<TimedValue> values { get; set; }
    }

    public class LightningActivityLevel
    {
        public List<TimedValue> values { get; set; }
    }

    public class TwentyFootWindSpeed
    {
        public string sourceUnit { get; set; }
        public string uom { get; set; }
        public List<TimedValue> values { get; set; }
    }

    public class TwentyFootWindDirection
    {
        public string sourceUnit { get; set; }
        public string uom { get; set; }
        public List<TimedValue> values { get; set; }
    }

    public class WaveHeight
    {
        public object sourceUnit { get; set; }
        public List<object> values { get; set; }
    }

    public class WavePeriod
    {
        public object sourceUnit { get; set; }
        public List<object> values { get; set; }
    }

    public class WaveDirection
    {
        public object sourceUnit { get; set; }
        public List<object> values { get; set; }
    }

    public class PrimarySwellHeight
    {
        public object sourceUnit { get; set; }
        public List<object> values { get; set; }
    }

    public class PrimarySwellDirection
    {
        public object sourceUnit { get; set; }
        public List<object> values { get; set; }
    }

    public class SecondarySwellHeight
    {
        public object sourceUnit { get; set; }
        public List<object> values { get; set; }
    }

    public class SecondarySwellDirection
    {
        public object sourceUnit { get; set; }
        public List<object> values { get; set; }
    }

    public class WavePeriod2
    {
        public object sourceUnit { get; set; }
        public List<object> values { get; set; }
    }

    public class WindWaveHeight
    {
        public object sourceUnit { get; set; }
        public List<object> values { get; set; }
    }

    public class DispersionIndex
    {
        public List<TimedValue> values { get; set; }
    }

    public class Pressure
    {
        public List<TimedValue> values { get; set; }
    }

    public class ProbabilityOfTropicalStormWinds
    {
        public string sourceUnit { get; set; }
        public string uom { get; set; }
        public List<object> values { get; set; }
    }

    public class ProbabilityOfHurricaneWinds
    {
        public string sourceUnit { get; set; }
        public string uom { get; set; }
        public List<object> values { get; set; }
    }

    public class PotentialOf15mphWinds
    {
        public List<object> values { get; set; }
    }

    public class PotentialOf25mphWinds
    {
        public List<object> values { get; set; }
    }

    public class PotentialOf35mphWinds
    {
        public List<object> values { get; set; }
    }

    public class PotentialOf45mphWinds
    {
        public List<object> values { get; set; }
    }

    public class PotentialOf20mphWindGusts
    {
        public List<object> values { get; set; }
    }

    public class PotentialOf30mphWindGusts
    {
        public List<object> values { get; set; }
    }

    public class PotentialOf40mphWindGusts
    {
        public List<object> values { get; set; }
    }

    public class PotentialOf50mphWindGusts
    {
        public List<object> values { get; set; }
    }

    public class PotentialOf60mphWindGusts
    {
        public List<object> values { get; set; }
    }

    public class GrasslandFireDangerIndex
    {
        public List<object> values { get; set; }
    }

    public class ProbabilityOfThunder
    {
        public List<object> values { get; set; }
    }

    public class DavisStabilityIndex
    {
        public List<TimedValue> values { get; set; }
    }

    public class AtmosphericDispersionIndex
    {
        public List<TimedValue> values { get; set; }
    }

    public class LowVisibilityOccurrenceRiskIndex
    {
        public List<TimedValue> values { get; set; }
    }

    public class Stability
    {
        public List<TimedValue> values { get; set; }
    }

    public class RedFlagThreatIndex
    {
        public List<object> values { get; set; }
    }

}
