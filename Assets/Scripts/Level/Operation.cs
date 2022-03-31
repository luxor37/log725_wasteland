﻿using UnityEngine;
using System.Collections.Generic;

namespace Level
{
    public abstract class Operation : ScriptableObject
    {
        public Shape predecessor;

        public abstract Stack<Shape> Apply(Stack<Shape> stack, List<Shape> results);
    }
}