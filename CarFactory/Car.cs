using System;

namespace CarFactory
{
    public class Car
    {
        public string Model { get; set; }
        public string Color { get; set; }
        public string RegNr { get; set; }

        public override string ToString()
        {
            return $"{Model} {Color} {RegNr}";
        }
    }
}
