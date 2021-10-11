using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BoxScripts
{
    public class Line
    {
        public float x1 { get; set; }
        public float y1 { get; set; }

        public float x2 { get; set; }
        public float y2 { get; set; }

        public Line() { x1 = y1 = x2 = y2 = 0; }
        public Line(Vector2 point1, Vector2 point2) : this (point1.x, point1.y, point2.x, point2.y) {}
        public Line(Vector3 point1, Vector3 point2) : this (point1.x, point1.y, point2.x, point2.y) {}
        public Line (float _x1, float _y1, float _x2, float _y2)
        {
            x1 = _x1;
            y1 = _y1;
            x2 = _x2;
            y2 = _y2;
        }
    }
    // https://stackoverflow.com/questions/4543506/algorithm-for-intersection-of-2-lines
    public static class LineIntersection
    {
        public static bool HasIntersection(Line lineA, Line lineB, out Vector2 vec, float tolerance = 0.0001f)
        {
            vec = Vector2.zero;

            double x1 = lineA.x1, y1 = lineA.y1;
            double x2 = lineA.x2, y2 = lineA.y2;

            double x3 = lineB.x1, y3 = lineB.y1;
            double x4 = lineB.x2, y4 = lineB.y2;
            
            if (Math.Abs(x1 - x2) < tolerance && Math.Abs(x3 - x4) < tolerance && Math.Abs(x1 - x3) < tolerance) return false;
            if (Math.Abs(y1 - y2) < tolerance && Math.Abs(y3 - y4) < tolerance && Math.Abs(y1 - y3) < tolerance) return false;
            if (Math.Abs(x1 - x2) < tolerance && Math.Abs(x3 - x4) < tolerance) return false;
            if (Math.Abs(y1 - y2) < tolerance && Math.Abs(y3 - y4) < tolerance) return false;

            double x, y;

            if (Math.Abs(x1 - x2) < tolerance)
            {
                double m2 = (y4 - y3) / (x4 - x3);
                double c2 = -m2 * x3 + y3;
                x = x1;
                y = c2 + m2 * x1;
            }
            else if (Math.Abs(x3 - x4) < tolerance)
            {
                double m1 = (y2 - y1) / (x2 - x1);
                double c1 = -m1 * x1 + y1;
                x = x3;
                y = c1 + m1 * x3;
            }
            else
            {
                double m1 = (y2 - y1) / (x2 - x1);
                double c1 = -m1 * x1 + y1;

                double m2 = (y4 - y3) / (x4 - x3);
                double c2 = -m2 * x3 + y3;

                x = (c1 - c2) / (m2 - m1);
                y = c2 + m2 * x;

                if (!(Math.Abs(-m1 * x + y - c1) < tolerance
                    && Math.Abs(-m2 * x + y - c2) < tolerance))
                    {
                        return false;
                    }
            }
            if (IsInsideLine(lineA, x, y) &&
                IsInsideLine(lineB, x, y))
                {
                    vec =  new Vector2 { x = (float) x, y = (float) y };
                    return true;
                }
                return false;
        }
        private static bool IsInsideLine(Line line, double x, double y)
        {
            return (x > line.x1 && x < line.x2
                        || x > line.x2 && x < line.x1)
                && (y > line.y1 && y < line.y2
                        || y > line.y2 && y < line.y1);
        }
    }
}