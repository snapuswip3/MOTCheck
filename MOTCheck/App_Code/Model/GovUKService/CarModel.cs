using System;
using System.Collections.Generic;

namespace MOTCheck.Model.GovUKService
{
    public class CarModel
    {
        public string Registration { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public DateTime? FirstUsedDate { get; set; }
        public string FuelType { get; set; }
        public string PrimaryColour { get; set; }
        public string VehicleId { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public DateTime? ManufactureDate { get; set; }
        public int? EngineSize { get; set; }
        public List<MotTestModel> MotTests { get; set; }
    }
}