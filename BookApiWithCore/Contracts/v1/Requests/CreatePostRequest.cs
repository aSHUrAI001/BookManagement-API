using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookApiWithCore.Contracts.v1.Requests
{
    public class CreatePostRequest
    {

        [Required(ErrorMessage ="Please Add Name")]
        [MaxLength(50,ErrorMessage ="Name Should be under 50 char.")]
        public string Name { get; set; }
    }
}
