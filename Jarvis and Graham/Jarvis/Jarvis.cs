﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace Jarvis
{
	class Jarvis
	{

		public static int n = 15;
		
		public static List<Point> Points = new List<Point>();
      

        public static List<Point> ConvexHull(List<Point> points)
		{
			List<Point> hull = new List<Point>();

			// cel mai din stanga punct
			Point pointInHull = points.Where(p => p.X == points.Min(min => min.X)).First();

			Point endPoint;
			do
			{
				hull.Add(pointInHull);
				endPoint = points[0];

				for (int i = 1; i < points.Count; i++)
				{
					if ((pointInHull == endPoint) || (Orientation(pointInHull, endPoint, points[i]) == -1))
					{
						endPoint = points[i];
					}
				}

				pointInHull = endPoint;

			}
			while (endPoint != hull[0]);

			return hull;
		}

		private static int Orientation(Point p1, Point p2, Point p)
		{
			// Determinantul 
			int Orient = (p2.X - p1.X) * (p.Y - p1.Y) - (p.X - p1.X) * (p2.Y - p1.Y);

			if (Orient > 0)
				return -1; //Orientat in partea stanga
			if (Orient < 0)
				return 1; //Orientat in partea dreapta

			return 0; //Orientat coliniar
		}
	}
}

