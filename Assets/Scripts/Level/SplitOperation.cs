using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Level
{
    [CreateAssetMenu(fileName = "Place Shape Operation", menuName = "ScriptableObjects/LevelGenerator/SplitOperation", order = 1)]
    public class SplitOperation : Operation
    {
        public List<Vector3> SplitLoc = new List<Vector3>();
        public Vector3 randomRange = new Vector3();
        public override Stack<Shape> Apply(Stack<Shape> stack, List<Shape> results)
        {
            var leftShape = CreateInstance<Shape>();
            leftShape.Position = predecessor.Position;
            var shapeList = new List<Shape> { leftShape };

            foreach (var split in SplitLoc)
            {
                var rightShape = CreateInstance<Shape>();
                rightShape.Position = predecessor.Position;
                // if (!Mathf.Approximately(split.x, 0))
                //     rightShape.Position.x = predecessor.Position.x + UnityEngine.Random.Range(split.x - randomRange.x, split.x + randomRange.x);
                // if (!Mathf.Approximately(split.y, 0))
                //     rightShape.Position.y = predecessor.Position.y + UnityEngine.Random.Range(split.y - randomRange.y, split.y + randomRange.y);
                // if (!Mathf.Approximately(split.z, 0))
                // rightShape.Position.z = predecessor.Position.z + UnityEngine.Random.Range(split.z - randomRange.z, split.z + randomRange.z);
                
                rightShape.Position.x += split.x;
                rightShape.Position.y += split.y;
                rightShape.Position.z += split.z;

                if (split.x > 0)
                {
                    Debug.Log(rightShape);
                }

                
                shapeList.Add(rightShape);
            }

            shapeList.Reverse();
            foreach (var s in shapeList)
                stack.Push(s);

            return stack;
        }
    }
}