/*Console.WriteLine(string.Join(", ", GetSyllables("araba")));
Console.WriteLine(string.Join(", ", GetSyllables("biçimine")));
Console.WriteLine(string.Join(", ", GetSyllables("insanın")));
Console.WriteLine(string.Join(", ", GetSyllables("karaca")));

Console.WriteLine(string.Join(", ", GetSyllables("aldı")));
Console.WriteLine(string.Join(", ", GetSyllables("birlik")));
Console.WriteLine(string.Join(", ", GetSyllables("sevmek")));

Console.WriteLine(string.Join(", ", GetSyllables("altlık")));
Console.WriteLine(string.Join(", ", GetSyllables("türkçe")));
Console.WriteLine(string.Join(", ", GetSyllables("korkmak")));
Console.WriteLine(string.Join(", ", GetSyllables("transkripsiyon")));
Console.WriteLine(string.Join(", ", GetSyllables("house")));
Console.WriteLine(string.Join(", ", GetSyllables("tyre")));
*/

string q = "ok attı";
if(q.Length > 5)
{
	string lastWord = q.Split(' ').Last();
	lastWord.Dump();
	GetSyllables(lastWord)[^1].Dump();
}

static List<string> GetSyllables(string word)
{
	List<string> syllables = new List<string>();

	// Gelen kelimenin ünlü harfler 1, ünsüzler 0 olacak şekilde desenini çıkarır.
	string bits = string.Join("", word.Select(l => "aeıioöuü".Contains(l) ? '1' : '0'));

	// Yakalanacak desenleri ve desen yakalandığında kelimenin hangi pozisyondan kesileceğini tanımlayan liste
	var separators = new List<(string pattern, int cutPos)>()
		{
			("101", 1),
			("1001", 2),
			("10001", 3),
			("100001", 4)
		};

	int index = 0, cutStartPos = 0;

	// index değerini elimizdeki bitler üzerinde yürütmeye başlıyoruz.
	while (index < bits.Length)
	{
		// Elimizdeki her ayırıcıyı (seperator), bits'in index'inci karakterinden itibaren tek tek deneyerek yakalamaya çalışıyoruz.
		foreach (var (seperatorPattern, seperatorCutPos) in separators)
		{
			if (bits.Substring(index).StartsWith(seperatorPattern))
			{
				// Yakaladığımızda, en son cutStartPos pozisyonundan, bulunduğumuz pozisyonun seperatorCutPos kadar ilerisine kadar bölümü alıp syllables listesine ekliyoruz.
				syllables.Add(word.Substring(cutStartPos, index + seperatorCutPos - cutStartPos));

				// Index'imiz seperatorCutPos kadar ilerliyor, ve cutStartPos'u index'le aynı yapıyoruz.
				index += seperatorCutPos;
				cutStartPos = index;
				break;
			}
		}

		// Index ilerliyor, cutStartPos'da değişiklik yok.
		index += 1;
	}

	// Son kalan heceyi elle listeye ekliyoruz.
	syllables.Add(word.Substring(cutStartPos));
	return syllables;
}
