using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomadaStore.Models.Models.Enums.Customer;

namespace TomadaStore.Models.DTOs.Customer
{
    public class CustomerUpdateSituationDTO
    {
        public SituationType Situation { get; set; }
    }
}
