using UnityEditor;
using System.Collections.Generic;
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
            GenerateLevel();
        }

        public void GenerateLevel()
        {
            Stack<Shape> shapeStack = new Stack<Shape>();
            Shape axiom = AxiomShape;
            shapeStack.Push(axiom);
            int offsetYCur = 0;
            int offsetXCur = 0;
            while (shapeStack.Count > 0)
            {
                Shape shape = shapeStack.Pop();
                // TODO: better randomness than this
                // Rules.Reverse();
                foreach (var rule in Rules)
                {
                    if (rule.PredecessorShape.Symbol == shape.Symbol)
                    {
                        List<Shape> results = rule.CalculateRule();
                        if (results.Count == 0)
                            continue;

                        foreach (var resultShape in results)
                        {
                            if (!resultShape.ShapeObject)
                                shapeStack.Push(resultShape);
                           
                            Debug.Log(resultShape.name);
                            int offsetXIndex = (int)resultShape.Attributes.FindIndex(a => a == Shape.AttributeEnum.OFFSET_X);
                            Debug.Log(offsetXCur);
                            int offsetYIndex = (int)resultShape.Attributes.FindIndex(a => a == Shape.AttributeEnum.OFFSET_Y);
                            int offsetX = 2;
                            int offsetY = 0;
                           // if (offsetXIndex != -1)
                           //     offsetXIndex += resultShape.AttributeValues[offsetXIndex];
                           // if (offsetYIndex != -1)
                           //     offsetYIndex += resultShape.AttributeValues[offsetYIndex];
                            offsetXCur -= offsetX;
                            if (resultShape.Symbol == Shape.SymbolEnum.COIN)
                                offsetY = 4;
                            if (resultShape.ShapeObject != null)
                                Instantiate(resultShape.ShapeObject, new Vector3(offsetXCur, offsetY, 0), transform.rotation);
                            
                        }
                        
                    }
                }
            }
        }
    }
}