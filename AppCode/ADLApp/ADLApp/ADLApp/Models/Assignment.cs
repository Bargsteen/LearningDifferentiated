﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADLApp.Models
{
    public abstract class Assignment
    {
        public int AssignmentId { get; set; }
        public string Headline { get; set; }
        public string Question { get; set; }
    }
}
