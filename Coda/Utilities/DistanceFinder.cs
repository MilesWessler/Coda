using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZipCodeCoords;

namespace Coda.Utilities
{
    public static class DistanceFinder
    {
        public static double FindDistanceBetweenCoordinates(double userLatitude, double userLongitude, double otherLatitude, double otherLongitude)
        {
            return Math.Sqrt(Math.Pow(69.1 * (userLatitude - otherLatitude), 2) +
                              Math.Pow(53.0 * (userLongitude - otherLongitude), 2));

        }
    }
}