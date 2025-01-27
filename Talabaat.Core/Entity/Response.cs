﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabaat.Core.Entity
{
    public class Response
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int BrandId { get; set; }
        public string Brand { get; set; }
        public int CatogryId { get; set; }
        public string Catogry { get; set; }
    }
}
