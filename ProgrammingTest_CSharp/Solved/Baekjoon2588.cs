
namespace ProgrammingTest_CSharp;

/*https://www.acmicpc.net/problem/2588*/
public class Baekjoon2588
{
    public static void Main()
    {
        var val1 = int.Parse(Console.ReadLine() ?? string.Empty);
        var val2 = int.Parse(Console.ReadLine() ?? string.Empty);
        var res  = 0;
        
        for(var digit=1; digit<=3; digit++) {
            var digitNum        = GetDigit(val2, digit);
            var intermediateRes = val1 * digitNum;
            Console.WriteLine(intermediateRes);
            res += intermediateRes * (int)Math.Pow(10, digit-1);
        }

        Console.WriteLine(res);
    }
    
    private static int GetDigit(int value, int digit) => value % (int)Math.Pow(10, digit) / (int)Math.Pow(10, digit-1);
}
