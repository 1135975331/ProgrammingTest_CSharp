using System;
using System.Collections.Generic;
using System.Linq;
namespace ProgrammingTest
{
	// https://www.acmicpc.net/problem/12865
	public static class Baekjoon12865
	{
		public static void Baekjoon12865Main()
		{
			var readInt     = ReadLineToIntArr(Console.ReadLine());
			var thingAmount = readInt[0];
			var weightLimit = readInt[1];

			var thingsArr = new Thing[thingAmount];
			
			for(var i = 0; i < thingAmount; i++) {
				var _readInt = ReadLineToIntArr(Console.ReadLine());
				thingsArr[i] = new Thing(_readInt[0], readInt[1]);
			}
			
			var maxValue = -1;
			var backpack = new List<Thing>();
			
			var tempBackpack = new List<Thing>();

			foreach(var thing1 in thingsArr) {
				foreach(var thing2 in thingsArr) {
					tempBackpack.Add(thing2);

					if(tempBackpack.Sum(t => t.Weight) > weightLimit) {
						tempBackpack.Remove(thing2);
					}
				}
			}


		}

		private struct Thing
		{
			public Thing(int weight, int value)
			{
				this.Weight = weight;
				this.Value = value;
			}

			public int Weight { get; }
			public int Value { get; }
		}
		
		public static int[] ReadLineToIntArr(string readLineStr)
        {
        	return readLineStr.Split(' ').Select(int.Parse).ToArray();
        }
	}
	
	
}