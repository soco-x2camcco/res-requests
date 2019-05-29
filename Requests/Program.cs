using Requests.NationalWeatherServiceData;
using Requests.RESi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requests
{
    class Program
    {
        static void Main(string[] args)
        {
            //LatLongPoint altusLocation = new LatLongPoint(33.8329279, -84.4229255);
            //var weatherService = new NationalWeatherService("2F55B2D9-7079-47DE-A264-40F37695C5DD", "participant 12452", "device 1289236", "Fake Data", "Superfake");
            //weatherService.GetSevenDayForecast(altusLocation).Wait();

            //var resi = new RequestParticipants();
            //resi.GetParticipants("57845511FA600AF3E0531C32800A32FC", "2F55B2D9-7079-47DE-A264-40F37695C5DD", "3128603", true).Wait();

            var skyCentricsRequestor = new SkyCentricsRequestor();
            //skyCentricsRequestor.SetEvent("13637", "LU").Wait();
            skyCentricsRequestor.GetDevices().Wait();
            //skyCentricsRequestor.DeleteDevice("20f85ed88cf6").Wait();

            //skyCentricsRequestor.SetLoadShed("13637", true).Wait();
            /*var device = new Device.Device();
            device.id = 13637;
            device.name = "CRC Water HPWH 1 (test)";
            device.user = 18913;
            device.group = 3304;
            device.client = 0;
            device.mac = "20f85ed88cf6";
            requestor.SetDevice(device).Wait();*/


            //var sensiboRequestor = new SensiboRequestor();
            //sensiboRequestor.GetDevices().Wait();
            //sensiboRequestor.GetDeviceACStates("vspGoqDs").Wait();
            //sensiboRequestor.SetDeviceACState("vspGoqDs").Wait();
            //sensiboRequestor.GetDeviceHistoricalSettings("vspGoqDs").Wait();
            //sensiboRequestor.GetDeviceSmartmode("vspGoqDs").Wait();       


            //var apiMgmt = new ApiManagementRequestor();
            //apiMgmt.ListBackend().Wait();

            //var ecobeeRequestor = new EcobeeRequestor();
            //ecobeeRequestor.GetDevices().Wait();
        }
    }
}
