using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace Assets.WUG.Scripts
{
    /// <summary>
    /// General helper methods 
    /// </summary>
   public static class GeneralExtensions
    {
        public static float AsPercentOfFixedDeltaTime(this float thisFloat) => Time.fixedDeltaTime / thisFloat;

        /// <summary>
        /// Returns the current vector with a new X
        /// </summary>
        public static Vector3 WithNewY(this Vector3 vector, float newY) => new Vector3(vector.x, newY, vector.z);

        /// <summary>
        /// Returns the current vector with a new X
        /// </summary>
        public static Vector2 WithNewX(this Vector2 vector, float newX) => new Vector2(newX, vector.y);
    }
}
