﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coda.Models
{
    public class TablatureRating
    {
        public int Id { get; set; }
        public int TabulatureId { get; set; }
        public int Rating { get; set; }
    }
}
