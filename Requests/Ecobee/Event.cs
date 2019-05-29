namespace Requests.Ecobee
{
    public class Event
    {
        /// <summary>
        /// The type of event. 
        /// Values: 
        ///     hold, demandResponse, sensor, switchOccupancy,
        ///     vacation, quickSave, today, autoAway, autoHome
        /// </summary>
        public string type;
        /// <summary>
        /// The unique event name.
        /// </summary>
        public string name;
        /// <summary>
        /// Whether the event is currently active or not.
        /// </summary>
        public bool running;
        /// <summary>
        /// The event start date in thermostat local time.
        /// </summary>
        public string startDate;
        /// <summary>
        /// The event start time in thermostat local time.
        /// </summary>
        public string startTime;
        /// <summary>
        /// The event end date in thermostat local time.
        /// </summary>
        public string endDate;
        /// <summary>
        /// The event end time in thermostat local time.
        /// </summary>
        public string endTime;
        /// <summary>
        /// Whether there are persons occupying the property during the event.
        /// </summary>
        public bool isOccupied;
        /// <summary>
        /// Whether cooling will be turned off during the event.
        /// </summary>
        public bool isCoolOff;
        /// <summary>
        /// Whether heating will be turned off during the event.
        /// </summary>
        public bool isHeatOff;
        /// <summary>
        /// The cooling absolute temperature to set.
        /// </summary>
        public int coolHoldTemp;
        /// <summary>
        /// The heating absolute temperature to set.
        /// </summary>
        public int heatHoldTemp;
        /// <summary>
        /// The fan mode during the event. 
        /// Values: 
        ///     auto, on 
        /// Default: based on current climate and hvac mode.
        /// </summary>
        public string fan;
        /// <summary>
        /// The ventilator mode during the vent. 
        /// Values: 
        ///     auto, minontime, on, off.
        /// </summary>
        public string vent;
        /// <summary>
        /// The minimum amount of time the ventilator equipment must stay on on each duty cycle.
        /// </summary>
        public int ventilatorMinOnTime;
        /// <summary>
        /// Whether this event is mandatory or the end user can cancel it.
        /// </summary>
        public bool isOptional;
        /// <summary>
        /// Whether the event is using a relative temperature setting to the currently active program climate.
        /// </summary>
        public bool isTemperatureRelative;
        /// <summary>
        /// The relative cool temperature adjustment.
        /// </summary>
        public int coolRelativeTemp;
        /// <summary>
        /// The relative heat temperature adjustment.
        /// </summary>
        public int heatRelativeTemp;
        /// <summary>
        /// Whether the event uses absolute temperatures to set the values.Default: true for DRs.
        /// </summary>
        public bool isTemperatureAbsolute;
        /// <summary>
        /// Indicates the % scheduled runtime during a Demand Response event.
        /// Valid range is 0 - 100%.
        /// Default = 100, indicates no change to schedule.
        /// </summary>
        public int dutyCyclePercentage;
        /// <summary>
        /// The minimum number of minutes to run the fan each hour.
        /// Range: 0-60
        /// Default: 0
        /// </summary>
        public int fanMinOnTime;
        /// <summary>
        /// True if this calendar event was created because of the occupied sensor.
        /// </summary>
        public bool occupiedSensorActive;
        /// <summary>
        /// True if this calendar event was created because of the unoccupied sensor
        /// </summary>
        public bool unoccupiedSensorActive;
        /// <summary>
        /// Unsupported. Future feature.
        /// </summary>
        public int drRampUpTemp;
        /// <summary>
        /// Unsupported.Future feature.
        /// </summary>
        public int drRampUpTime;
        /// <summary>
        /// Unique identifier set by the server to link one or more events and alerts together.
        /// </summary>
        public string linkRef;
        /// <summary>
        /// Used for display purposes to indicate what climate (if any) is being used for the hold.
        /// </summary>
        public string holdClimateRef;
    }
}