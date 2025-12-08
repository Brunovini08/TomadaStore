using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomadaStore.Models.DTOs.Sale
{
    public class SaleRequestDTO
    {
        public int CustomerId { get; set; }
        public List<string> ProductsIds { get; set; }
    }
}
