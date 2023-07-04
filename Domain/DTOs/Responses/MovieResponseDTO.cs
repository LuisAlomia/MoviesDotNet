using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Responses
{
    public class MovieResponseDTO
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public String ImageURl { get; set; }
    }
}
