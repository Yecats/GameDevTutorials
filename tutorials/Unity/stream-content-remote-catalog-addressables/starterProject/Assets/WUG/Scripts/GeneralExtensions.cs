using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace Assets.WUG.Scripts
{
    /// <summary>
    /// Static methods that help streamline routine actions
    /// </summary>
   public static class GeneralExtensions
    {
        /// <summary>
        /// Returns the current vector with a new Y
        /// </summary>
        public static Vector3 WithNewY(this Vector3 vector, float newY) => new Vector3(vector.x, newY, vector.z);

        /// <summary>
        /// Returns the current vector with a new X
        /// </summary>
        public static Vector3 WithNewX(this Vector3 vector, float newX) => new Vector3(newX, vector.y, vector.z);


        /// <summary>
        /// Returns the current vector with a new Z
        /// </summary>
        public static Vector3 WithNewZ(this Vector3 vector, float newZ) => new Vector3(vector.x, vector.y, newZ);

    }
}
