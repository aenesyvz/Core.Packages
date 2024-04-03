﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ElasticSearch.Models;

public class ElasticSearchGetModel<T>
{
    public string ElasticId { get; set; }
    public T Item { get; set; }

    public ElasticSearchGetModel()
    {
        ElasticId = string.Empty;
        Item = default!;
    }

    public ElasticSearchGetModel(string elasticId, T item)
    {
        ElasticId = elasticId;
        Item = item;
    }
}
