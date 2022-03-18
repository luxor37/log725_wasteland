using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Level
{
    public abstract class Operation : ScriptableObject
    {
        public Shape predecessor;

        abstract public void Apply(Stack<Shape> stack, List<Shape> results);
    }
}