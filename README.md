# Gilded Rose in C# xUnit

This readme and code comments will be in english as the code base and requirements were all in english.

## Initial System Specifications 

- All items have a `SellIn` value which denotes the number of days we have to sell the items
- All items have a `Quality` value which denotes how valuable the item is
- At the end of each day our system lowers both values for every item
- Once the sell by date has passed, `Quality` degrades twice as fast
- The `Quality` of an item is never negative
- **"Aged Brie"** actually increases in `Quality` the older it gets
- The `Quality` of an item is never more than 50
- **"Sulfuras"**, being a legendary item, never has to be sold or decreases in `Quality`
- **"Backstage passes"**, like aged brie, increases in `Quality` as its `SellIn` value approaches;
    - `Quality` increases by 2 when there are 10 days or less and by 3 when there are 5 days or less but
    - `Quality` drops to 0 after the concert
- An item can never have its Quality increase above 50
- **"Sulfuras"** is a legendary item and as such its Quality is 80 and it never alters.


## Requirements
- [ ] Add "Conjured" items to the system
- [ ] "Conjured" items degrade in Quality twice as fast as normal items
- [ ] Do not alter the Item class or Items property
- [ ] Add unit tests
- [ ] Pass Approval tests

## Workflow

1. Read trought the requirements 
2. Read the code and add TODOs for the pain points in the GildedRose class :
    a. Too many if/else statements, how to improve readability?
    b. Every check is based on the item name. Maybe we can introduce item types/categories ?

