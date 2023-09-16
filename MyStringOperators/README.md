# My String Extensions

The most used string algorithms gathered under a single class

# How to use

<code>Cut(length)</code>

```
string sourceText = "++a little Bunny raBBit waNdering in a huge Forest";
string nullText = null;

Console.WriteLine(nullText.Cut(22));
//Output: ""

//----------------------------------

Console.WriteLine(sourceText.Cut(5));
//Output: "++a l"

```

<code>TrimInside()</code>

```
Console.WriteLine(sourceText.TrimInside());
//Output: "++alittleBunnyraBBitwaNderinginahugeForest"

```

<code>NullExists()</code>

```
var nulls = new List<string> { "t1", null, "t2" };
Console.WriteLine(nulls.NullExists());
//Output: "True"

//----------------------------------

nulls = new List<string> { "t1", "t2", "t3" };
Console.WriteLine(nulls.NullExists());
//Output: "False"

```

<code>ContainsNosense(seek)</code>

```
Console.WriteLine(sourceText.ContainsNosense("rabbit"));
//Output: "True"

//----------------------------------

Console.WriteLine(sourceText.ContainsNosense("horse"));
//Output: "False"

```

<code>CountWords(seek)</code>

```
Console.WriteLine(sourceText.CountWords("rabbit"));
//Output: 1

//----------------------------------
Console.WriteLine(sourceText.CountWords("horse"));
//Output: 0

```

<code>StringExtensions.RandomString(length)</code>

```
Console.WriteLine(StringExtensions.RandomString(12));
//Output: "%0n9jdhwENCv"

```

<code>EncryptHash()</code>

```
Console.WriteLine(sourceText.EncryptHash());
//Output: "9bbdadb281d35bfd90270a06abe0f12ec061957f38a36d8aceefb95630cac6d1"

```

<code>Mask(start, length)</code>

```
Console.WriteLine(sourceText.Mask(8,12));
//Output: "++a littl***********Bit waNdering in a huge Forest"

```

<code>ToImage()</code>

```
var imgBytes = sourceText.ToImage();
File.WriteAllBytes("img.png", imgBytes);
//Output: "img file"

```

<code>ForWordsIn(seek, act)</code>

```
sourceText.ForWordsIn("i", x => Console.WriteLine(x));
//Output:
    //"little
    //"raBBit"
    //"waNdering"
    //"in"

```

<code>ToTitleCase()</code>

```
Console.WriteLine(sourceText.ToTitleCase());
//Output: "++A Little Bunny Rabbit Wandering In A Huge Forest"

```

<code>ToBodyCase()</code>

```
Console.WriteLine(sourceText.ToBodyCase());
//Output: "++A little Bunny raBBit waNdering in a huge Forest"

```

<code>ToLowerUnderscored()</code>

```
Console.WriteLine(sourceText.ToLowerUnderscored());
//Output: "++a_little_bunny_ra_b_bit_wa_ndering_in_a_huge_forest"

```


<code>Singularize()</code>

```
Console.WriteLine(sourceText.Singularize());
//Output: "+a litle Buny raBit waNdering in a huge Forest"

```

<code>Singularize()</code>

```
Console.WriteLine("#".Multiplex(21));
//Output: "######################"


```
<code>LoopIn(func)</code>
```
var result = sourceText.LoopIn(x => {
    if (x.Equals('h')) {
        return true;
    }
    return false;
});
Console.WriteLine(result);
//Output: True

```

<code>ClearDigits()</code>

```
Console.WriteLine("123 ankara ++".ClearDigits());
//Output: "ankara ++"

```

<code>ClearSymbols()</code>

```
Console.WriteLine(sourceText.ClearSymbols());
//Output: "a little Bunny raBBit waNdering in a huge Forest"

```

<code>ClearDigits() + ClearSymbols() + TrimInside()</code>

```
Console.WriteLine("123 ankara ++ merkez".ClearDigits().ClearSymbols().TrimInside());
//Output: "ankaramerkez"

```
