﻿using GildedRoseKata;

using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

namespace GildedRoseTests;

public class ApprovalTest
{
    [Fact]
    public Task UpdateQuality_SingleItem_UpdatesSellInAndQuality()
    {
        Item[] items = { new Item { Name = "foo", SellIn = 2, Quality = 2 } };
        GildedRose app = new GildedRose(items);
        app.UpdateQuality();
        
        return Verifier.Verify(items);
    }
    
    [Fact]
    public Task ThirtyDays()
    {
        var fakeoutput = new StringBuilder();
        Console.SetOut(new StringWriter(fakeoutput));
        Console.SetIn(new StringReader($"a{Environment.NewLine}"));

        Program.Main(new string[] { "30" });
        var output = fakeoutput.ToString();

        return Verifier.Verify(output);
    }
}