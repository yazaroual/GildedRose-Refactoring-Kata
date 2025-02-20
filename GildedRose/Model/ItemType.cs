namespace GildedRoseKata;

public enum ItemType
{
    /// <summary>
    /// Item that improves with age
    /// </summary>
    ImprovesWithAgeItem,
    /// <summary>
    /// Item that improves with age but quality drops to 0 after sellIn date
    /// </summary>
    EventsItem,
    /// <summary>
    /// Legendary item that never has to be sold or decreases in quality
    /// </summary>
    LegendaryItem,
    /// <summary>
    /// Conjured items degrade in quality twice as fast as normal items
    /// </summary>
    ConjuredItem,
    /// <summary>
    /// Normal item degrades in quality by 1 each day
    /// </summary>
    NormalItem
}