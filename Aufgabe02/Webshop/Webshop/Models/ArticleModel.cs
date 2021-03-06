﻿using System.Collections.Generic;
using Webshop.Infrastructure.Data.Entities;

namespace Webshop.Models
{
    public class ArticleModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public int SelectedCategoryId { get; set; }
        public Product Product { get; set; }
    }
}