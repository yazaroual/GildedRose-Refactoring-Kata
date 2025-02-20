using Xunit;
using System.Collections.Generic;
using GildedRoseKata;

namespace GildedRoseTests;

public class GildedRoseTest
{
    [Fact]
    public void UpdateQuality_SingleItem_UpdatesSellInAndQuality()
    {
        IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 2, Quality = 2 } };
        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();
        Assert.Single(Items);
        Assert.Equal("foo", Items[0].Name);
        Assert.Equal(1, Items[0].Quality);
        Assert.Equal(1, Items[0].SellIn);
    }

}