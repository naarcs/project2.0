using System;
using System.Collections.Generic;

namespace fruitwebshop2._0.Models;

public partial class Orderitem
{
    public int OrderItemId { get; set; }

    public int? OrderId { get; set; }

    public int? FruitId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public virtual Fruit? Fruit { get; set; }

    public virtual Order? Order { get; set; }
}
