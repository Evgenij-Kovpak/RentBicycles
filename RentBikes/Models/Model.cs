﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RentBikes.Models
{
    public class Model
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Make Make { get; set; }
        
        public int MakeId { get; set; }
    }
}
