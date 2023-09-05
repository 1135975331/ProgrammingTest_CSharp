using System;

namespace Utilities
{
	public static class FloorRoundCeil
	{
		public static double FloorFrom(double value, int pos)
		{
			if(pos < 1)
				throw new ArgumentException("\'pos\' should be at least 1.");

			var powOf10 = Math.Pow(10, pos-1);

			return (double)Math.Floor((float)value*powOf10) / powOf10;
		}

		public static double RoundFrom(double value, int pos)
		{
			if(pos < 1)
				throw new ArgumentException("\'pos\' should be at least 1.");

			var powOf10 = Math.Pow(10, pos-1);

			return (double)Math.Round((float)value*powOf10) / powOf10;
		}

		public static double CeilFrom(double value, int pos)		
		{
			if(pos < 1)
				throw new ArgumentException("\'pos\' should be at least 1.");

			var powOf10 = Math.Pow(10, pos-1);

			return (double)Math.Ceiling((float)value*powOf10) / powOf10;
		}
	}
}