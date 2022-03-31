using UnityEngine;
using System.Collections.Generic;

namespace Level
{
    [CreateAssetMenu(fileName = "Place Shape Operation", menuName = "ScriptableObjects/LevelGenerator/SplitOperation", order = 1)]
    public class SplitOperation : Operation
    {
        public List<Vector3> SplitLoc = new List<Vector3>();
        public Vector3 RandomRange = new Vector3();
        public override Stack<Shape> Apply(Stack<Shape> stack, List<Shape> results)
        {
            var leftShape = CreateInstance<Shape>();
            leftShape.Position = predecessor.Position;
            var shapeList = new List<Shape> { leftShape };

            foreach (var split in SplitLoc)
            {
                var rightShape = CreateInstance<Shape>();
                rightShape.Position = predecessor.Position;
                
                rightShape.Position.x += RandomRange.x > 0 ? Random.Range(split.x - RandomRange.x, split.x + RandomRange.x) : split.x;
                rightShape.Position.y += RandomRange.y > 0 ? Random.Range(split.y - RandomRange.y, split.y + RandomRange.y) : split.y;
                rightShape.Position.z += RandomRange.z > 0 ? Random.Range(split.z - RandomRange.z, split.z + RandomRange.z) : split.z;
                
                shapeList.Add(rightShape);
            }

            shapeList.Reverse();
            foreach (var s in shapeList)
                stack.Push(s);

            return stack;
        }
    }
}