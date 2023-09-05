using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Utilities;

public static class Util
{
	public static float GetRandAngle()
		=> (float) RandomInRange.RandomDoubleInRange(0, 360);

	/// <summary>
	/// 0.0f ~ 1.0f 사이의 float값을 0 ~ 255 사이의 byte 값으로 변환한다.
	/// </summary>
	/// <param name="f">변환할 float값 (0.0f ~ 1.0f)</param>
	/// <returns>byte값</returns>
	/// <exception cref="ArgumentOutOfRangeException">float값이 전제 범위(0.0f ~ 1.0f)를 벗어난 경우</exception>
	public static byte Float2Byte(float f)
	{
		if(f is > 1.0f or < 0.0f)
			throw new ArgumentOutOfRangeException($"Float value must be 0.0f < f < 1.0f but got {f}");

		return (byte) (255 * f);
	}

	/// <summary>
	/// 0 ~ 255 사이의 byte 값을 0.0f ~ 1.0f 사이의 float값으로 변환한다.
	/// </summary>
	/// <param name="b">변환할 byte값</param>
	/// <returns>변환된 float 값 (0.0f ~ 1.0f)</returns>
	public static float Byte2Float(byte b)
	{
		// if(b > 255)
		// 	throw new ArgumentOutOfRangeException($"Float value must be 0.0f < f < 1.0f but got {f}");

		return 255f / b;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="min">최솟값</param>
	/// <param name="value">값</param>
	/// <param name="max">최댓값</param>
	/// <returns>value가 min보다 작으면 min 반환, max보다 크다면 max 반환, 둘다 아니라면 value 그대로 반환</returns>
	public static float MinMax(float min, float value, float max)
	{
		if(min > value)
			return min;

		if(value > max)
			return max;

		return value;
	}

	/// <summary>
	/// 입력한 처음 값에서 입력한 마지막 값까지를 0~1 사이의 값으로 변환한다.
	/// 예를 들어, 처음 값 30, 마지막 값 100인 경우,
	/// 30 => 0.0, 70 => 0.5, 100 => 1.0이 된다.
	/// </summary>
	/// <returns></returns>
	public static float GetProgressValue(float min, float max, float value)
		=> (value - min) / (max - min);


	/// <summary>
	/// angleMin ~ angleMax의 범위를 벗어나는 60분법 각도값을 해당 범위 내의 값으로 보정하는 메소드
	/// </summary>
	/// <param name="eulerAngleVal">보정하고자 하는 오일러 각도값</param>
	/// <param name="angleMin">최소값 (기본값: 0)</param>
	/// <param name="angleMax">최대값 (기본값: 360)</param>
	/// <returns></returns>
	public static float CorrectEulerAngleValue(float eulerAngleVal, float angleMin = 0, float angleMax = 360)
	{
		if(angleMin > angleMax)
			throw new ArgumentException($"Minimum value({angleMin}) is larger than maximum value({angleMax})");

		var difference = angleMax - angleMin;

		while(eulerAngleVal < angleMin)
			eulerAngleVal += difference;

		while(eulerAngleVal >= angleMax)
			eulerAngleVal -= difference;

		return eulerAngleVal;
	}

	/// <summary>
	/// 문자열을 주어진 타입 파라미터 값으로 변환하는 메소드
	/// <br/><br/> 
	/// 참고: <a href="https://stackoverflow.com/questions/732677/converting-from-string-to-t">https://stackoverflow.com/questions/732677/converting-from-string-to-t</a>
	/// </summary>
	/// <param name="strValue">변환할 문자열</param>
	/// <typeparam name="T">변환할 타입</typeparam>
	/// <returns>주어진 타입(T)으로 변환된 값</returns>
	public static T GetValue<T>(string strValue)
		=> (T) Convert.ChangeType(strValue, typeof(T));


	public static bool IsInteger<T>()
	{
		var integerNumericTypes = new[] {
			typeof(sbyte), typeof(byte),
			typeof(short), typeof(ushort),
			typeof(int), typeof(uint),
			typeof(long), typeof(ulong)
		};

		return integerNumericTypes.Any(type => type == typeof(T));
	}

	public static bool IsFloat<T>()
	{
		var floatNumericTypes = new[] {
			typeof(float), typeof(double),
		};

		return floatNumericTypes.Any(type => type == typeof(T));
	}

	public static bool IsNumber<T>()
		=> IsInteger<T>() || IsFloat<T>();

	public static int GetLineNumberOfSpecificStr(string text, string lineToFind, StringComparison comparison = StringComparison.CurrentCulture)
	{
		using var reader = new StringReader(text);

		var lineNum = 0;
		while(reader.ReadLine() is { } line) {
			lineNum++;
			if(line.Equals(lineToFind, comparison))
				return lineNum;
		}
		return -1;
	}


	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool IsWhiteSpace(char ch)
	{
		switch(ch) {
			case '\u0009':
			case '\u000A':
			case '\u000B':
			case '\u000C':
			case '\u000D':
			case '\u0020':
			case '\u0085':
			case '\u00A0':
			case '\u1680':
			case '\u2000':
			case '\u2001':
			case '\u2002':
			case '\u2003':
			case '\u2004':
			case '\u2005':
			case '\u2006':
			case '\u2007':
			case '\u2008':
			case '\u2009':
			case '\u200A':
			case '\u2028':
			case '\u2029':
			case '\u202F':
			case '\u205F':
			case '\u3000':
				return true;
			default:
				return false;
		}
	}

	public static string RemoveWhiteSpaces(this string str)
		=> new string(str.Where(c => !IsWhiteSpace(c)).ToArray());

	public static double ReadDoubleOrDefault(double defaultValue = 0)
	{
		var str = Console.ReadLine();
		return str switch {
			null => defaultValue,
			""   => defaultValue,
			_    => double.Parse(str)
		};
	}
}