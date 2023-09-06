#define TEST
#undef TEST

using System;
namespace ProgrammingTest_CSharp;

/*https://www.acmicpc.net/problem/15486*/
public class Baekjoon15486
{
    public static void Baekjoon15486Main()  
    {
#if TEST
        var n = 7;
        var ti = new[] {3,5,1,1,2,4,2};
        var pi = new[] {10,20,10,20,15,40,200};
#else
        var n = int.Parse(Console.ReadLine() ?? "0");
        
        var ti = new int[n];
        var pi = new int[n];

        for(var i = 0; i < n; i++) {
            var parsedStr = (Console.ReadLine() ?? "0").Split(' ');
            ti[i] = int.Parse(parsedStr[0]);
            pi[i] = int.Parse(parsedStr[1]);
        }
#endif
        
        Console.WriteLine(GetTotalP(ti, pi, 0, n));
    }

    private static int GetTotalP(int[] ti, int[] pi, int index, int n)
    {
        if(index >= n)
            return 0;
        if(index + ti[index] > n)  // 0 <= index <= 6
            return 0;

        var curMaxP = -1;
        
        for(var i = 0; i<n; i++)
            curMaxP = Math.Max(curMaxP, pi[index] + GetTotalP(ti, pi, index + i + ti[index], n));

        return curMaxP;
    }
    
}