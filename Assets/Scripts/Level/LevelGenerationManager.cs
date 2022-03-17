using UnityEditor;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Level
{
    public class LevelGenerationManager : MonoBehaviour
    {
        static LevelGenerationManager instance;
        
        [SerializeField]
        List<Rule> Rules;

        [SerializeField]
        Shape AxiomShape;

        [SerializeField]
        int maxXLen = 30;

        int minXLen = 0;

        GameObject player;

        public static LevelGenerationManager Instance
        {
            get { return instance; }
        }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
            }
        }

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            GenerateLevel();
        }

        public void GenerateLevel()
        {
            Stack<Shape> shapeStack = new Stack<Shape>();
            Stack < Tuple<int, int> > xRangeStack = new Stack<Tuple<int, int>>();
            Shape axiom = AxiomShape;
            shapeStack.Push(axiom);
            xRangeStack.Push(Tuple.Create(0, maxXLen));
            while (shapeStack.Count > 0)
            {
                Shape shape = shapeStack.Pop();
                Tuple<int, int> xRange = xRangeStack.Pop();
                List<Rule> rulesMatch = new List<Rule>();
                foreach (var rule in Rules)
                {
                    if (rule.PredecessorShape.Symbol == shape.Symbol)
                    {
                        rulesMatch.Add(rule);  
                    }
                }
                if (rulesMatch.Count == 0)
                    continue;
                var ruleChosen = rulesMatch[UnityEngine.Random.Range(0, rulesMatch.Count)];
                List<Shape> results = ruleChosen.CalculateRule();
                if (results.Count == 0)
                    continue;
                int index = 0;
                foreach (var resultShape in results)
                {
                    if (!resultShape.ShapeObject)
                    {
                        shapeStack.Push(resultShape);
                        int xMin = xRange.Item1 + (xRange.Item2 - xRange.Item1) / results.Count * index;
                        int xMax = xRange.Item1 + (xRange.Item2 - xRange.Item1) / results.Count * (index + 1);
                        xRangeStack.Push(Tuple.Create(xMin, xMax));
                        Debug.Log(resultShape.Symbol);
                        Debug.Log(xMin);
                        Debug.Log(xMax);
                    }


                    var offsetX = UnityEngine.Random.Range(xRange.Item1, xRange.Item2);
                   
                    var offsetY = 0;
                    if (resultShape.Symbol == Shape.SymbolEnum.COIN)
                        offsetY = 4;


                    if (resultShape.ShapeObject != null)
                        Instantiate(resultShape.ShapeObject, new Vector3(-offsetX, offsetY, 0), transform.rotation);
                    index += 1;
                }
            }
        }
    }
}