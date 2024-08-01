# My String Extensions

The most used string algorithms gathered under a single class

# Install
```
NuGet\Install-Package MyStringExtensions -Version 1.0.0
```
version of package for use last version see [https://www.nuget.org/packages/MyStringExtensions/](https://www.nuget.org/packages/MyStringExtensions/)

# How to use

<code>Cut(length)</code>
Substring for unknown length. No errors for mismatches due to empty space or lengthSubstring for unknown length. No errors for mismatches due to empty space or length
```csharp
string sourceText = "++a little Bunny raBBit waNdering in a huge Forest";
string nullText = null;

Console.WriteLine(nullText.Cut(22));
//Output: ""

//----------------------------------

Console.WriteLine(sourceText.Cut(5));
//Output: "++a l"

```

<code>TrimInside()</code>
Trim all whitespace in text
```csharp
Console.WriteLine(sourceText.TrimInside());
//Output: "++alittleBunnyraBBitwaNderinginahugeForest"

```

<code>NullExists()</code>
Check Null or Empty value in string collection
```csharp
var nulls = new List<string> { "t1", null, "t2" };
Console.WriteLine(nulls.NullExists());
//Output: "True"

//----------------------------------

nulls = new List<string> { "t1", "t2", "t3" };
Console.WriteLine(nulls.NullExists());
//Output: "False"

```

<code>ContainsNosense(seek)</code>
Find a word in string without sensivity
```csharp
Console.WriteLine(sourceText.ContainsNosense("rabbit"));
//Output: "True"

//----------------------------------

Console.WriteLine(sourceText.ContainsNosense("horse"));
//Output: "False"

```

<code>CountWords(seek)</code>
Find how many times the word you are looking for is repeated in the text
```csharp
Console.WriteLine(sourceText.CountWords("rabbit"));
//Output: 1

//----------------------------------
Console.WriteLine(sourceText.CountWords("horse"));
//Output: 0

```

<code>RandomString(length)</code>
Generate Random text
```csharp
Console.WriteLine(StringExtensions.RandomString(12));
//Output: "%0n9jdhwENCv"

```

<code>EncryptHash()</code>
Generate non-recyclable writing from your text
```csharp
Console.WriteLine(sourceText.EncryptHash());
//Output: "9bbdadb281d35bfd90270a06abe0f12ec061957f38a36d8aceefb95630cac6d1"

```

<code>Mask(start, length)</code>
Generate masked text from your text
```csharp
Console.WriteLine(sourceText.Mask(8,12));
//Output: "++a littl***********Bit waNdering in a huge Forest"

```

<code>Mask(startLength, maskLength, endLength, maskChar='*')</code>
Generate masked text from your text
```csharp
Console.WriteLine("HelloWorld!".Mask(4,2, 2, 'x'));
//Helloxxld!

```
<code>ToImage()</code>
Text to image bytes. Exports png file bytes. Only works on Windows OS
```csharp
var imgBytes = sourceText.ToImage();
File.WriteAllBytes("img.png", imgBytes);
//Output: "img file"

```

<code>ForWordsIn(seek, act)</code>
Run a delegated action for each found word. Detailed
```csharp
sourceText.ForWordsIn("i", x => Console.WriteLine(x));
//Output:
    //"little
    //"raBBit"
    //"waNdering"
    //"in"

```

<code>ToTitleCase()</code>
Make uppercase the first letters after a space
```csharp
Console.WriteLine(sourceText.ToTitleCase());
//Output: "++A Little Bunny Rabbit Wandering In A Huge Forest"

```

<code>ToBodyCase()</code>
Make uppercase the first character of the text
```csharp
Console.WriteLine(sourceText.ToBodyCase());
//Output: "++A little Bunny raBBit waNdering in a huge Forest"

```

<code>ToLowerUnderscored()</code>
Make snakecase for source text
```csharp
Console.WriteLine(sourceText.ToLowerUnderscored());
//Output: "++a_little_bunny_ra_b_bit_wa_ndering_in_a_huge_forest"

```


<code>Singularize()</code>
Deduplicate characters that repeat side by side
```csharp
Console.WriteLine(sourceText.Singularize());
//Output: "+a litle Buny raBit waNdering in a huge Forest"

```

<code>Multiplex(length)</code>
Duplicate a selected text as many times as you like
```csharp
Console.WriteLine("#".Multiplex(21));
//Output: "######################"


```
<code>LoopIn(func)</code>
Run an action for each character in the text
```csharp
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
Remove numeric chars and return new string object
```csharp
Console.WriteLine("123 ankara ++".ClearDigits());
//Output: "ankara ++"

```

<code>ClearSymbols()</code>
Remove symbolic chars and return new string object
```csharp
Console.WriteLine(sourceText.ClearSymbols());
//Output: "a little Bunny raBBit waNdering in a huge Forest"

```

<code>ClearDigits() + ClearSymbols() + TrimInside()</code>

```csharp
Console.WriteLine("123 ankara ++ merkez".ClearDigits().ClearSymbols().TrimInside());
//Output: "ankaramerkez"

```
<code>Compress()</code>
Compress text size
```csharp
string gg = "Kediler, baðýmsýz ve meraklý yapýlarýyla bilinen evcil hayvanlardýr. Çeþitli boyut ve renklerde olmalarýna raðmen ortak bazý özellikleri vardýr. Okþandýklarýnda mýrýldanmalarý, avlanma içgüdülerinin güçlü olmasý ve yüksek yerlere týrmanmaktan hoþlanmalarý kedilerin dikkat çekici özelliklerindendir. Bazý insanlar kedileri yalnýz ve mesafeli bulurken, diðerleri onlarýn sevgi dolu ve sadýk arkadaþlar olduðunu düþünür. Tarih boyunca insanlarla birlikte yaþayan kediler, hem evlerde hem de sokaklarda yaþamýný sürdürebilirler.";
Console.WriteLine("Compressed Text      : " + gg);
Console.WriteLine();
Console.WriteLine("Compressed Size      : " + gg.Compress().Length);
Console.WriteLine("Decompressed Size    : " + Encoding.ASCII.GetBytes(gg).Length);

//Compressed Text      : Kediler, baðýmsýz ve meraklý yapýlarýyla bilinen evcil hayvanlardýr. Çeþitli boyut ve renklerde olmalarýna raðmen ortak bazý özellikleri vardýr. Okþandýklarýnda mýrýldanmalarý, avlanma içgüdülerinin güçlü olmasý ve yüksek yerlere týrmanmaktan hoþlanmalarý kedilerin dikkat çekici özelliklerindendir. Bazý insanlar kedileri yalnýz ve mesafeli bulurken, diðerleri onlarýn sevgi dolu ve sadýk arkadaþlar olduðunu düþünür. Tarih boyunca insanlarla birlikte yaþayan kediler, hem evlerde hem de sokaklarda yaþamýný sürdürebilirler.
//Compressed Size      : 460
//Decompressed Size    : 525

```