﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using DeltaInverter;
//
//    var welcome = Welcome.FromJson(jsonString);

namespace Requests.DeltaInverter
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class History
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("items")]
        public Item[] Items { get; set; }
    }

    public partial class Item
    {
        [JsonProperty("time")]
        public DateTimeOffset Time { get; set; }

        [JsonProperty("gridFreq")]
        public long GridFreq { get; set; }

        [JsonProperty("pvTodayE")]
        public long PvTodayE { get; set; }

        [JsonProperty("pvInfo sumE")]
        public long PvInfoSumE { get; set; }

        [JsonProperty("consumptionE")]
        public long ConsumptionE { get; set; }

        [JsonProperty("gridBuyE")]
        public long GridBuyE { get; set; }

        [JsonProperty("gridSellE")]
        public long GridSellE { get; set; }

        [JsonProperty("sn")]
        public string Sn { get; set; }
    }

    public partial class History
    {
        public static History FromJson(string json) => JsonConvert.DeserializeObject<History>(json, DeltaInverter.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this History self) => JsonConvert.SerializeObject(self, DeltaInverter.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
