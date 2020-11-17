using System.Linq;

namespace WUG.BehaviorTreeDemo
{
    public abstract class Composite : Node
    {

        protected int CurrentChildIndex = 0;

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="name">Display name of the composite</param>
        /// <param name="nodes">Children nodes</param>
        protected Composite(string displayName, params Node[] childNodes)
        {
            Name = displayName;

            ChildNodes.AddRange(childNodes.ToList());
        }

    }
}
