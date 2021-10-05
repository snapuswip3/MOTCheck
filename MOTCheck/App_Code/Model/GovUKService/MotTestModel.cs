using System;
using System.Collections.Generic;

namespace MOTCheck.Model.GovUKService
{
    public class MotTestModel
    {
        public DateTime? CompletedDate { get; set; }
        public string TestResult { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int? OdometerValue { get; set; }
        public string OdometerUnit { get; set; }
        public string OdometerResultType { get; set; }
        public long? MotTestNumber { get; set; }
        public List<MotReasonForRefusalModel> RfrAndComments { get; set; }
    }
}