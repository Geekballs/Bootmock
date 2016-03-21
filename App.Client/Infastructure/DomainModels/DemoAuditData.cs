using System;
using System.ComponentModel.DataAnnotations;

namespace App.Client.Infastructure.DomainModels
{
    public class DemoAuditData
    {
        [Key]
        public Guid AuditId { get; set; }

        public DateTime AuditDate { get; set; }

        public decimal MinPerf { get; set; }

        public decimal MaxPerf { get; set; }

        public decimal AvgPerf { get; set; }

        public int Volume { get; set; }
    }
}