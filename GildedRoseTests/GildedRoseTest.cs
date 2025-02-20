using Xunit;
using System.Collections.Generic;
using GildedRoseKata;

namespace GildedRoseTests;

public class GildedRoseTest
{

    // Add more tests for : 
    // - At the end of each day lower values for Quality and Sellin - Across 30 days
    // - When the sell by date has passed, Quality degrades twice as fast
    // - The Quality of an item is never negative
    // - The Quality of an item is never more than 50
    // - "Aged Brie" actually increases in Quality the older it gets
    // - "Sulfuras", being a legendary item, never has to be sold or decreases in Quality
    // - "Backstage passes", increases in Quality as its SellIn value approaches; Quality increases by 2 when there are 10 days or less and by 3 when there are 5 days or less but Quality drops to 0 after the concert
    // - "Conjured" items degrade in Quality twice as fast as normal items

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



    [Fact]
    public void UpdateQuality_SingleItem_UpdatesSellInAndQualityFor30Days()
    {
        IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 30, Quality = 30 } };
        GildedRose app = new GildedRose(Items);
        for (int i = 0; i < 30; i++)
        {
            app.UpdateQuality();
        }
        Assert.Single(Items);
        Assert.Equal("foo", Items[0].Name);
        Assert.Equal(0, Items[0].Quality);
        Assert.Equal(0, Items[0].SellIn);
    }

    [Fact]
    public void UpdateQuality_TenQualityItem_PassedItemsDegradesTwiceAsFast()
    {
        IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 1, Quality = 10 } };
        GildedRose app = new GildedRose(Items);
        for (int i = 0; i < 3; i++)
        {
            app.UpdateQuality();
        }
        Assert.Single(Items);
        Assert.Equal("foo", Items[0].Name);
        //Asserts that quality is 5, because it degrades by 1 the first day, and by 2 the next two days
        Assert.Equal(5, Items[0].Quality);
        Assert.Equal(-2, Items[0].SellIn);
    }

    [Fact]
    public void UpdateQuality_OneQualityItem_QualityIsNeverNegative()
    {
        IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 1, Quality = 1 } };
        GildedRose app = new GildedRose(Items);

        for (int i = 0; i < 2; i++)
        {
            app.UpdateQuality();
        }

        Assert.Single(Items);
        Assert.Equal("foo", Items[0].Name);
        //Asserts that quality is 0, not -1 
        Assert.Equal(0, Items[0].Quality);
        Assert.Equal(-1, Items[0].SellIn);
    }

    [Fact]
    public void UpdateQuality_AgedBrieItem_QualityIncreasesByOneAsItGetsOlder()
    {
        IList<Item> Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 10, Quality = 1 } };
        GildedRose app = new GildedRose(Items);

        for (int i = 0; i < 10; i++)
        {
            app.UpdateQuality();
        }

        Assert.Single(Items);
        Assert.Equal("Aged Brie", Items[0].Name);
        Assert.Equal(11, Items[0].Quality);
        Assert.Equal(0, Items[0].SellIn);
    }

    [Fact]
    public void UpdateQuality_AgedBrieItem_QualityIncreasesBy2AfterSellInDate()
    {
        IList<Item> Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 1, Quality = 1 } };
        GildedRose app = new GildedRose(Items);

        for (int i = 0; i < 10; i++)
        {
            app.UpdateQuality();
        }

        Assert.Single(Items);
        Assert.Equal("Aged Brie", Items[0].Name);
        Assert.Equal(20, Items[0].Quality);
        Assert.Equal(-9, Items[0].SellIn);
    }


    [Fact]
    public void UpdateQuality_AgedBrieItem_QualityNeverGoesAbove50()
    {
        IList<Item> Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 1, Quality = 49 } };
        GildedRose app = new GildedRose(Items);

        for (int i = 0; i < 2; i++)
        {
            app.UpdateQuality();
        }

        Assert.Single(Items);
        Assert.Equal("Aged Brie", Items[0].Name);
        //Asserts that quality is 50, because it never goes above 50
        Assert.Equal(50, Items[0].Quality);
        Assert.Equal(-1, Items[0].SellIn);
    }

    //Sulfuras never deacreases in quality
    [Fact]
    public void UpdateQuality_SulfurasItem_QualityAndSellInNeverDecreases()
    {
        IList<Item> Items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 1, Quality = 80 } };
        GildedRose app = new GildedRose(Items);

        for (int i = 0; i < 10; i++)
        {
            app.UpdateQuality();
        }

        Assert.Single(Items);
        Assert.Equal("Sulfuras, Hand of Ragnaros", Items[0].Name);
        //Asserts that quality is 80, because it never decreases
        Assert.Equal(80, Items[0].Quality);
        Assert.Equal(1, Items[0].SellIn);
    }

    // - "Backstage passes", increases in Quality as its SellIn value approaches; Quality increases by 2 when there are 10 days or less and by 3 when there are 5 days or less but Quality drops to 0 after the concert
    [Fact]
    public void UpdateQuality_BackstagePassesItem_QualityIncreasesBasedOnSellInDate()
    {
        IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 11, Quality = 1 } };
        GildedRose app = new GildedRose(Items);

        for (int i = 0; i < 11; i++)
        {
            app.UpdateQuality();
        }

        Assert.Single(Items);
        Assert.Equal("Backstage passes to a TAFKAL80ETC concert", Items[0].Name);
        //Asserts that quality is 27, 
        //increases by 1 the first day = 2
        //by 2 the next 5 days = 10 + 2
        //by 3 the last 5 days = 15 + 10 + 2
        Assert.Equal(27, Items[0].Quality);
        Assert.Equal(0, Items[0].SellIn);
    }

    [Fact]
    public void UpdateQuality_BackstagePassesItem_QualityDropsToZeroAfterConcert()
    {
        IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 1, Quality = 1 } };
        GildedRose app = new GildedRose(Items);

        for (int i = 0; i < 2; i++)
        {
            app.UpdateQuality();
        }

        Assert.Single(Items);
        Assert.Equal("Backstage passes to a TAFKAL80ETC concert", Items[0].Name);
        Assert.Equal(0, Items[0].Quality);
        Assert.Equal(-1, Items[0].SellIn);
    }

    //Asserts that a backstage item never goes above 50
    [Fact]
    public void UpdateQuality_BackstagePassesItem_QualityNeverGoesAbove50()
    {
        IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 1, Quality = 49 } };
        GildedRose app = new GildedRose(Items);

        for (int i = 0; i < 1; i++)
        {
            app.UpdateQuality();
        }

        Assert.Single(Items);
        Assert.Equal("Backstage passes to a TAFKAL80ETC concert", Items[0].Name);
        //Asserts that quality is 50, as Quality was already at 49 and SellIn at 1, it should increase by 3
        Assert.Equal(50, Items[0].Quality);
        Assert.Equal(0, Items[0].SellIn);
    }

}