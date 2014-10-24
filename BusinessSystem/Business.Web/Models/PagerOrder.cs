﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business.Web.Models
{
    public class PagerOrder
    {
        public bool IsPager { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public string OrderByValue { get; set; }

        public string OrderByDesc { get; set; }
    }
}