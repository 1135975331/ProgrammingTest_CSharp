using System;
namespace ProgrammingTest_CSharp;

public class Baekjoon1316
{
    /*https://www.acmicpc.net/problem/1316*/
    public static void Main()
    {
        var strAmount      = int.Parse(Console.ReadLine() ?? "0");
        var groupWordCount = 0;

        for(var i = 0; i < strAmount; i++) {
            var str = Console.ReadLine() ?? "0";
            groupWordCount += GetIsGroupWord(str) ? 1 : 0;
        }
        
        Console.WriteLine(groupWordCount);
    }

    public static bool GetIsGroupWord(string str)
    {
        for(var a = 0; a < str.Length; a++) {
            var selectedChar = str[a];
	                
            var prevIndex = a;
            for(var b = a+1; b < str.Length; b++) {
                if(selectedChar != str[b])
                    continue;

                if(b == prevIndex + 1)
                    prevIndex = b;
                else
                    return false;
            }
            
            a = prevIndex;
        }
        
        return true;
    }
}