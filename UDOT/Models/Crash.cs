using System;
namespace UDOT.Models
{
    public class Crash
    {
        public int CRASH_ID { get; set; }
        public DateTime Crash_Datetime { get; set; }
        public string Route { get; set; }
        public float Milepoint { get; set;  }
        public float Lat_Utm_Y { get; set; }
        public float Long_Utm_X { get; set; }
        public string Main_Road_Name { get; set; }
        public string City { get; set; }
        public string County_Name { get; set; }
        public int Crash_Severity_ID { get; set; }
        public bool Work_Zone_Related { get; set; }
        public bool Pedestrian_Involved { get; set; }
        public bool Bicyclist_Involved { get; set; }
        public bool Motorcycle_Involved { get; set; }
        public bool Improper_Restraint { get; set; }
        public bool Unrestrained { get; set; }
        public bool DUI { get; set; }
        public bool Intersection_Related { get; set; }
        public bool Wild_Animal_Related { get; set; }
        public bool Domestic_Animal_Related { get; set; }
        public bool Overturn_Rollover { get; set; }
        public bool Commercial_Motor_VEH_Involved { get; set; }
        public bool Teenage_Driver_Involved { get; set; }
        public bool Older_Driver_Involved { get; set; }
        public bool Night_Dark_Condition { get; set; }
        public bool Single_Vehicle { get; set; }
        public bool Distracted_Driver { get; set; }
        public bool Drowsy_Driver { get; set; }
        public bool Roadway_Departure { get; set; }
    }
}
