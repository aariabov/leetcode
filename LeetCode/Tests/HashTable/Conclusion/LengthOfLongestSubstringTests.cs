namespace Tests.HashTable.Conclusion;

/// <summary>
/// Максимальная длина подстроки, у которой символы не повторяются https://leetcode.com/explore/learn/card/hash-table/187/conclusion-hash-table/1135/
/// </summary>
public class LengthOfLongestSubstringTests
{
    [Theory]
    [InlineData("abcabcbb", 3)]
    [InlineData("bbbbb", 1)]
    [InlineData("pwwkew", 3)]
    [InlineData("aab", 2)]
    [InlineData(" ", 1)]
    [InlineData("dvdf", 3)]
    [InlineData("ckilbkd", 5)]
    [InlineData("abba", 2)]
    public void Test(string a, int expected)
    {
        var result = LengthOfLongestSubstring(a);
        Assert.Equal(expected, result);
    }
    
    public int LengthOfLongestSubstring(string s) {
        // Словарь для хранения последнего индекса каждого символа
        Dictionary<char, int> charIndexMap = new Dictionary<char, int>();
        int maxLength = 0;
        int left = 0; // Левая граница окна
        
        for (int right = 0; right < s.Length; right++) {
            char currentChar = s[right];
            
            // Если символ уже встречался и его индекс >= left, т.е вместо удаления из справочника, мы тупо его игнорим
            if (charIndexMap.ContainsKey(currentChar) && charIndexMap[currentChar] >= left) {
                // Сдвигаем левую границу окна на позицию после повторения
                left = charIndexMap[currentChar] + 1;
            }
            
            // Обновляем индекс текущего символа
            charIndexMap[currentChar] = right;
            
            // Обновляем максимальную длину
            maxLength = Math.Max(maxLength, right - left + 1);
        }
        
        return maxLength;
    }
    
    public int LengthOfLongestSubstring1(string s) {
        HashSet<char> set = new HashSet<char>();
        int maxLength = 0;
        int left = 0;
        
        for (int right = 0; right < s.Length; right++) {
            // Удаляем символы с левой стороны, пока не уберем дубли
            while (set.Contains(s[right])) {
                set.Remove(s[left]);
                left++;
            }
            
            set.Add(s[right]);
            maxLength = Math.Max(maxLength, right - left + 1);
        }
        
        return maxLength;
    }
    
    // моя версия
    public int LengthOfLongestSubstringMy(string s)
    {
        var max = 0;
        var curr = 0;
        
        // запоминаем символ и его индекс
        var dict = new Dictionary<char, int>();
        var i = 0;
        var startIdx = 0;
        while (i < s.Length)
        {
            if (dict.TryAdd(s[i], i))
            {
                // все ок, увеличиваем счетчик
                i++;
                curr++;
            }
            else
            {
                // встретили дубль
                if (curr > max)
                {
                    max = curr;
                }

                var charIdx = dict[s[i]]; // индекс дубля
                curr = i - charIdx;
                
                // удаляем все символы до дубля
                while (startIdx < charIdx + 1)
                {
                    dict.Remove(s[startIdx]);
                    startIdx++;
                }
                
                dict[s[i]] = i;
                i++;
            }
        }

        return curr > max ? curr : max;
    }
}