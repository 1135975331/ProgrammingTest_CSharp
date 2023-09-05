#define DEBUG
#undef DEBUG

using System;
using System.Text;
using System.Threading;
using Utilities;
namespace ProgrammingTest;


public static class LangtonAnt
{
	public static void LangtonAntMain()
	{
#if DEBUG
		var fieldSize = 75;
		var fieldSizeX = 75;
		var fieldSizeY = 46;
		var stepFrom = 0;
		var stepUntil = 20000;
		var updateIntervalMs = 10;
#else		
		Console.Write("Enter field X length: ");
		var fieldSizeX = (int)Util.ReadDoubleOrDefault(75);
		Console.Write("Enter field Y length: ");
		var fieldSizeY = (int)Util.ReadDoubleOrDefault(46);
			
		Console.Write("Enter step to stop: ");
		var stepUntil = (int)Util.ReadDoubleOrDefault(12000);
			
		Console.Write("Enter update interval(MS, Enter negative to update step by step): ");
		var updateIntervalMs = (int)Util.ReadDoubleOrDefault(10);
#endif
		var stepByStepEnabled = updateIntervalMs < 0;
		
		var field             = new Field(fieldSizeX, fieldSizeY);
		var ant               = new Ant(fieldSizeX / 2 + 8, fieldSizeY / 2, Direction.NORTH);

		Console.WriteLine(PrintProgress.PrintCurrentProgress(field.AntField, ant.Coordinate, ant.LookingDirection, ant.Steps));
		Console.WriteLine("Field is set. Please resize the window or the font.\n (Press Enter to Start Simulation)");
		Console.ReadLine();

		try {
			while(ant.Steps < stepUntil) {
				field.InvertFieldTile(ant.Coordinate);
				ant.Step(field.AntField[ant.Coordinate.Y, ant.Coordinate.X]);

				// Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n");
				Console.WriteLine(PrintProgress.PrintCurrentProgress(field.AntField, ant.Coordinate, ant.LookingDirection, ant.Steps));

				if(stepByStepEnabled) {
					Console.WriteLine("Press Enter to Continue...");
					Console.ReadLine();
				}
				else
					Thread.Sleep(updateIntervalMs);
			}
		}
		catch(IndexOutOfRangeException ioore) {
			Console.WriteLine("The ant is out of the tile. Simulation terminated.\n(Retry with larger amount of tile.)");
			Console.Write("(Press Enter to exit)  ");
			Console.ReadLine();
			
			return;
		}
		catch(Exception e) {
			Console.WriteLine(e.StackTrace);
			Console.WriteLine(e.Message);
			Console.ReadLine();
			
			return;
		}
		
		Console.WriteLine("Reached to target step. Simulation terminated.");
		Console.Write("(Press Enter to exit)  ");
		Console.ReadLine();
	}



}

internal struct Ant
{
	public IntVector2 Coordinate { get; set; }
	public int Steps { get; set; }
	public Direction LookingDirection { get; set; }
		
	public Ant(IntVector2 initCoord, Direction initDirection)
	{
		Coordinate       = initCoord;
		Steps            = 0;
		LookingDirection = initDirection;
	}
		
	public Ant(int initX, int initY, Direction initDirection): this(new IntVector2(initX, initY), initDirection)
	{ }

	public void Step(bool curFieldTile)
	{
		// curFieldTile   true == black, false: white
		LookingDirection = curFieldTile ? DirectionExtension.TurnRight(LookingDirection) : DirectionExtension.TurnLeft(LookingDirection);

		MoveToward();
		Steps++;
	}

	private void MoveToward()
	{
		var coordChange = LookingDirection switch {
			Direction.NORTH => new IntVector2(0, 1),
			Direction.WEST  => new IntVector2(-1, 0),
			Direction.SOUTH => new IntVector2(0, -1),
			Direction.EAST  => new IntVector2(1, 0),
			_               => throw new ArgumentOutOfRangeException()
		};
			
		Coordinate += coordChange;
	}


}

internal struct Field
{
	public bool[,] AntField { get; set; } // false == white, true == black

	public Field(int initSizeX, int initSizeY)  
		=> AntField = new bool[initSizeY, initSizeX];  // 1차원: y축, 2차원: x축  
	public Field(int initSize) : this(initSize, initSize)
	{
	}

	public void InvertFieldTile(IntVector2 antCoord)
		=> AntField[antCoord.Y, antCoord.X] = !AntField[antCoord.Y, antCoord.X];
}
	
internal struct PrintProgress
{
	private static readonly StringBuilder Sb = new StringBuilder();
	private const char W_SQ = '□';
	private const char B_SQ = '■';
	
	public static string PrintCurrentProgress(bool[,] antField, IntVector2 antCoord, Direction antDir, int steps)
	{
		Sb.Clear();
		var curAntSteppingTile = '0';
		
		for(var y = antField.GetLength(0) - 1; y >= 0; y--) {
			for(var x = 0; x < antField.GetLength(1); x++) {
				var curTile = antField[y, x] ? B_SQ : W_SQ;
				
				if(antCoord.X == x && antCoord.Y == y) {  // When a and b is equal to antCoord.x, y
					curAntSteppingTile = curTile;
					Sb.Append(DirectionExtension.GetDirectionChar(antDir));
				}
				else
					Sb.Append(curTile);
			}
			Sb.AppendLine();
		}
			
		Sb.AppendLine();
		Sb.Append(antCoord.ToString())
		   .AppendLine($"  Direction: {antDir}")
		   .Append($"Current Tile: {curAntSteppingTile}")
		   .AppendLine($"  Steps: {steps}");

		return Sb.ToString();
	}
}
	
internal struct IntVector2
{
	public IntVector2(int x, int y)
	{
		X = x;
		Y = y;
	}
		
	public int X { get; set; }
	public int Y { get; set; }
		
	public static IntVector2 operator +(IntVector2 a, IntVector2 b)
		=> new IntVector2(a.X + b.X, a.Y + b.Y);
		
	public override string ToString()
		=> $"X: {X}, Y: {Y}";
}
	
internal enum Direction
{
	NORTH, WEST, SOUTH, EAST
}

internal struct DirectionExtension
{
	public static Direction TurnLeft(Direction curDir)
	{
		return curDir switch {
			Direction.NORTH => Direction.WEST,
			Direction.WEST  => Direction.SOUTH,
			Direction.SOUTH => Direction.EAST,
			Direction.EAST  => Direction.NORTH,
			_               => throw new ArgumentOutOfRangeException($"Invalid Ant Direction: {curDir}")
		};
	}

	public static Direction TurnRight(Direction curDir)
	{
		return curDir switch {
			Direction.NORTH => Direction.EAST,
			Direction.WEST  => Direction.NORTH,
			Direction.SOUTH => Direction.WEST,
			Direction.EAST  => Direction.SOUTH,
			_               => throw new ArgumentOutOfRangeException($"Invalid Ant Direction: {curDir}")
		};
	}

	public static char GetDirectionChar(Direction curDir)
	{
		return curDir switch {
			Direction.NORTH => '↑',
			Direction.WEST  => '←',
			Direction.SOUTH => '↓',
			Direction.EAST  => '→',
			_               => throw new ArgumentOutOfRangeException($"Invalid Ant Direction: {curDir}")
		};
	}
}