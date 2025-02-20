using System;
using System.Collections.Generic;

namespace GildedRoseKata;

public class GildedRose
{
    IList<Item> Items;

    public GildedRose(IList<Item> Items)
    {
        this.Items = Items;
    }

    public void UpdateQuality()
    {
        for (var i = 0; i < Items.Count; i++)
        {

            switch (GetItemType(Items[i]))
            {
                case ItemType.ImprovesWithAgeItem:
                    UpdateImprovesWithAgeItem(Items[i]);
                    break;
                case ItemType.EventsItem:
                    UpdateEventsItem(Items[i]);
                    break;
                case ItemType.LegendaryItem:
                    //Nothing changes for legendary items
                    break;
                case ItemType.ConjuredItem:
                    UpdateConjuredItem(Items[i]);
                    break;
                default:
                    //By default everything is a normal item
                    UpdateNormalItem(Items[i]);
                    break;
            }
        }
    }

    /// <summary>
    /// Update quality for conjured items. Conjured items degrade in quality twice as fast as normal items
    /// </summary>
    private void UpdateConjuredItem(Item item)
    {
        if (item.Quality > 0)
        {
            item.Quality = item.Quality - 2;
        }

        item.SellIn = item.SellIn - 1;

        if (item.Quality > 0 && item.SellIn < 0)
        {
            item.Quality = item.Quality - 2;
        }
    }


    /// <summary>
    /// Update quality for normal items. Normal items degrade in quality by 1 each day
    /// </summary>
    private void UpdateNormalItem(Item item)
    {
        if (item.Quality > 0)
        {
            item.Quality = item.Quality - 1;
        }

        //Note : The initial logic was to update quality before updating sellIn and then updated quality again if sellIn < 0.
        item.SellIn = item.SellIn - 1;

        if (item.Quality > 0 && item.SellIn < 0)
        {
            item.Quality = item.Quality - 1;
        }
    }

    /// <summary>
    /// Update quality for events items. Events items improve in quality as sellIn approaches.
    /// </summary>
    private void UpdateEventsItem(Item item)
    {
        if (item.Quality < 50)
        {
            if (item.SellIn < 11 && item.SellIn > 5)
            {
                item.Quality = item.Quality + 2;
            }
            else if (item.SellIn < 6 && item.SellIn > -1)
            {
                if (item.Quality < 50)
                {
                    item.Quality = item.Quality + 3;
                }
            }
            else
            {
                item.Quality = item.Quality + 1;
            }
        }

        //Make sure quality never goes above 50
        if (item.Quality > 50)
        {
            item.Quality = 50;
        }

        //Note : The initial logic was to update quality before updating sellIn and then updated quality again if sellIn < 0.
        item.SellIn = item.SellIn - 1;

        if (item.SellIn < 0)
        {
            item.Quality = 0;
        }
    }

    /// <summary>
    /// Update quality for items that improve with age.
    /// </summary>
    private void UpdateImprovesWithAgeItem(Item item)
    {
        item.SellIn = item.SellIn - 1;

        if (item.Quality < 50)
        {
            if (item.SellIn < 0)
            {
                item.Quality = item.Quality + 2;
            }
            else
            {
                item.Quality = item.Quality + 1;
            }
        }
    }

    /// <summary>
    /// Get the type of item based on it's name.
    /// </summary>
    public ItemType GetItemType(Item item)
    {
        if (item.Name == "Aged Brie")
        {
            return ItemType.ImprovesWithAgeItem;
        }
        else if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
        {
            return ItemType.EventsItem;
        }
        else if (item.Name == "Sulfuras, Hand of Ragnaros")
        {
            return ItemType.LegendaryItem;
        }
        else if (item.Name.StartsWith("Conjured"))
        {
            return ItemType.ConjuredItem;
        }
        else
        {
            return ItemType.NormalItem;
        }
    }

}
