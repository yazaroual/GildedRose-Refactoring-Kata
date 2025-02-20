# Gilded Rose in C# xUnit

This is my solution to refactor the Gilded Rose Inventory program.
This readme and code comments will be in english as the code base and requirements were in english.

## Initial System Specifications 

- All items have a `SellIn` value which denotes the number of days we have to sell the items
- All items have a `Quality` value which denotes how valuable the item is
- At the end of each day our system lowers both values for every item
- Once the sell by date has passed, `Quality` degrades twice as fast
- The `Quality` of an item is never negative
- The `Quality` of an item is never more than 50
- An item can never have its Quality increase above 50
- **"Aged Brie"** actually increases in `Quality` the older it gets. Quality increases by 2 each day after SellIn.
- **"Sulfuras"**, being a legendary item, never has to be sold or decreases in `Quality`. `SellIn` never decreases.
- **"Backstage passes"**, like aged brie, increases in `Quality` as its `SellIn` value approaches;
    - `Quality` increases by 1 when there are more than 10 days
    - `Quality` increases by 2 when there are 10 days or less 
    - `Quality` increases by 3 when there are 5 days or less
    - `Quality` drops to 0 after the concert
- **"Sulfuras"** is a legendary item and as such its Quality is 80 and it never alters.


## Requirements
- [x] Add "Conjured" items to the system
- [x] "Conjured" items degrade in Quality twice as fast as normal items
- [x] Do not alter the Item class or Items property
- [x] Add unit tests
- [x] Pass Approval tests

## Workflow

This is a step by step description of how I discovered and managed to refactor the code.

1. Read trought the requirements 
2. Write down what we know about the current system specifications. See [Initial System Specifications](#initial-system-specifications)
3. Read the code and add TODOs for the pain points in the GildedRose class :
    a. Too many if/else statements, how to improve readability?
    b. Every check is based on the item name. Maybe we can introduce item types/categories ?
4. Fix existing unit tests. Also use the following naming convention for the tests:
    * The name of the method being tested.
    * The scenario under which it's being tested.
    * The expected behavior when the scenario is invoked.
5. Add additional unit tests to cover the solution before the refactoring. Approval tests will be kept to verify the output but I won't provide more text based test cases. As I added unit tests, I also discovered specifications that were not clearly documented. I updated the specifications chapter accordingly. Ex : **"Aged Brie"** quality increases by 2 each day after SellIn date was reached.
6. Now that the current implementation is tested. And all tests are green. We can start improving the code to prepare the implementation of the new category.
7. Using the tests, I refactored the `GildedRose` class without adding support for conjured item.
    a. Introduced an `ItemType` enum to get the item type based on item name
    b. Extracted the logic of each item type in it's own function
    c. Using a switch statement, call the correct function for each item type. This should allow us to easily add a new type.
8. Added the `Conjured Items` category

## What could be improved if I had more time

- Clarify the requirements with Allison. For some items, we update the quality, then the SellIn date then we re-update the quality again. I kept it as is to not break the current system but that could be discussed with the owner and simplified.
- Right now the Item Type is based on the item name, it can cause errors if there is a typo. We could add specific classes for each type of items, all would inherit from the `Item` class. That would be cleaner.


