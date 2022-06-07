using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShortUrlDemo.Models
{
    public partial class Short
    {
        [Key]
        public string Code { get; set; }
        public string Url { get; set; }
    }
}
