string sourceText = "++a little Bunny raBBit waNdering in a huge Forest";
string nullText = null;

//----------------------------------
//Cut(length)
//----------------------------------
Console.WriteLine(nullText.Cut(22));
//Output: ""
Console.WriteLine(sourceText.Cut(5));
//Output: "++a l"

//----------------------------------
//TrimInside()
//----------------------------------
Console.WriteLine(sourceText.TrimInside());
//Output: "++alittleBunnyraBBitwaNderinginahugeForest"

//----------------------------------
//NullExists()
//----------------------------------
var nulls = new List<string> { "t1", null, "t2" };
Console.WriteLine(nulls.NullExists());
//Output: "True"
//----------------------------------
nulls = new List<string> { "t1", "t2", "t3" };
Console.WriteLine(nulls.NullExists());
//Output: "False"

//----------------------------------
//ContainsNosense(seek)
//----------------------------------
Console.WriteLine(sourceText.ContainsNosense("rabbit"));
//Output: "True"
//----------------------------------
Console.WriteLine(sourceText.ContainsNosense("horse"));
//Output: "False"


//----------------------------------
//CountWords(seek)
//----------------------------------
Console.WriteLine(sourceText.CountWords("rabbit"));
//Output: 1
//----------------------------------
Console.WriteLine(sourceText.CountWords("horse"));
//Output: 0

//----------------------------------
//StringExtensions.RandomString(length)
//----------------------------------
Console.WriteLine(StringExtensions.RandomString(12));
//Output: "%0n9jdhwENCv"


//----------------------------------
//EncryptHash()
//----------------------------------
Console.WriteLine(sourceText.EncryptHash());
//Output: "9bbdadb281d35bfd90270a06abe0f12ec061957f38a36d8aceefb95630cac6d1"


//----------------------------------
//Mask(start, length)
//----------------------------------
Console.WriteLine(sourceText.Mask(8,12));
//Output: "++a littl***********Bit waNdering in a huge Forest"


//----------------------------------
//ToImage()
//----------------------------------
var imgBytes = sourceText.ToImage();
File.WriteAllBytes("img.png", imgBytes);
//Output: "img file"

//----------------------------------
//ForWordsIn(seek, act)
//----------------------------------
sourceText.ForWordsIn("i", x => Console.WriteLine(x));
//Output:
    //"little
    //"raBBit"
    //"waNdering"
    //"in"


//----------------------------------
//ToTitleCase()
//----------------------------------
Console.WriteLine(sourceText.ToTitleCase());
//Output: "++A Little Bunny Rabbit Wandering In A Huge Forest"


//----------------------------------
//ToBodyCase()
//----------------------------------
Console.WriteLine(sourceText.ToBodyCase());
//Output: "++A little Bunny raBBit waNdering in a huge Forest"


//----------------------------------
//ToLowerUnderscored()
//----------------------------------
Console.WriteLine(sourceText.ToLowerUnderscored());
//Output: "++a_little_bunny_ra_b_bit_wa_ndering_in_a_huge_forest"


//----------------------------------
//Singularize()
//----------------------------------
Console.WriteLine(sourceText.Singularize());
//Output: "+a litle Buny raBit waNdering in a huge Forest"

//----------------------------------
//Multiplex(length)
//----------------------------------
Console.WriteLine("#".Multiplex(21));
//Output: "######################"


//----------------------------------
//LoopIn(func)
//----------------------------------
var result = sourceText.LoopIn(x => {
    if (x.Equals('h')) {
        return true;
    }
    return false;
});
Console.WriteLine(result);
//Output: True


//----------------------------------
//ClearDigits()
//----------------------------------
Console.WriteLine("123 ankara ++".ClearDigits());
//Output: "ankara ++"


//----------------------------------
//ClearSymbols()
//----------------------------------
Console.WriteLine(sourceText.ClearSymbols());
//Output: "a little Bunny raBBit waNdering in a huge Forest"


//----------------------------------
//ClearDigits() + ClearSymbols() + TrimInside()
//----------------------------------
Console.WriteLine("123 ankara ++ merkez".ClearDigits().ClearSymbols().TrimInside());
//Output: "ankaramerkez"










