using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookApiWithCore.Contracts.v1.Requests
{
    public class UpdatePostRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
