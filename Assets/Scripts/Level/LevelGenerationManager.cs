using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Level
{
    public class LevelGenerationManager : MonoBehaviour
    {
        [SerializeField]
        List<Rule> Rules;

        [SerializeField]
        List<Rule> ContentRules;

        [SerializeField]
        List<Rule> EnnemyRules;

        public Rule StartRule;

        public Rule EndRule;

        public Rule BlockTunnelRule;

        [SerializeField]
        Shape AxiomShape;

        public int MaxNumberBlockLevel = 1000000;

        private readonly System.Random rnd = new System.Random();

        public void GenerateLevel()
        {
            PlaceShapeOperation.occupiedObject = new List<Vector3>();
            PlaceShapeOperation.occupiedEmpty = new List<Vector3>();

            var axiom = AxiomShape;
            var terrainNodes = new List<Shape>();
            var contentNodes = new List<Shape>();
            var enemyNodes = new List<Shape>();

            terrainNodes.AddRange(StartRule.CalculateRule(axiom).Item1);

            //------Placing Level Blocks------
            var counter = 0;
            while (terrainNodes.Count > 0)
            {
                var index = rnd.Next(terrainNodes.Count);
                var shape = terrainNodes[index];

                //Get valid random node
                while (PlaceShapeOperation.occupiedObject.Any(x => x == shape.Position))
                {
                    terrainNodes.RemoveAt(index);

                    if (terrainNodes.Count == 0) break;

                    index = rnd.Next(terrainNodes.Count);
                    shape = terrainNodes[index];
                }

                counter ++;
                if (counter > MaxNumberBlockLevel)
                {
                    PlaceEndLevel(terrainNodes);
                    break;
                }

                //get the rules that can append to the chosen empty node (shape)
                var rulesMatch = Rules.Where(rule => rule.PredecessorShape.Symbol == shape.Symbol).ToList();

                if (rulesMatch.Count == 0) break;

                //Pick a random rule to apply
                var ruleChosen = rulesMatch[Random.Range(0, rulesMatch.Count)];
                var (results, succeeded) = ruleChosen.CalculateRule(shape);

                if (succeeded) terrainNodes.RemoveAt(index);

                if (results.Count == 0) continue;
                
                terrainNodes.AddRange(results.Where(x => x.Symbol == Shape.SymbolEnum.EMPTY && x.Position.x % 20 == 0).ToList());
                contentNodes.AddRange(results.Where(x => x.Symbol == Shape.SymbolEnum.CONTENT).ToList());
                enemyNodes.AddRange(results.Where(x => x.Symbol == Shape.SymbolEnum.ENEMY).ToList());
                
            }

            //------Placing Blocked Tunnel Blocks------
            PlaceBlockedTunnelBlocks(terrainNodes);

            //------Placing Content------
            PlaceContent(Shape.SymbolEnum.CONTENT, contentNodes, ContentRules);

            //------Placing Enemies------
            PlaceContent(Shape.SymbolEnum.ENEMY, enemyNodes, EnnemyRules);
        }

        private void PlaceBlockedTunnelBlocks(IList<Shape> terrainNodes)
        {
            while (terrainNodes.Count > 0)
            {
                var index = rnd.Next(terrainNodes.Count);
                var shape = terrainNodes[index];

                while (PlaceShapeOperation.occupiedObject.Any(x => x == shape.Position))
                {
                    terrainNodes.RemoveAt(index);

                    if (terrainNodes.Count == 0) break;

                    index = rnd.Next(terrainNodes.Count);
                    shape = terrainNodes[index];
                }

                if (terrainNodes.Count == 0) break;

                BlockTunnelRule.CalculateRule(shape);

                try
                {
                    terrainNodes.RemoveAt(index);
                }
                catch (ArgumentOutOfRangeException)
                {
                    Debug.Log("Out of range");
                }
            }
        }

        private void PlaceEndLevel(IList<Shape> terrainNodes)
        {
            var index = rnd.Next(terrainNodes.Count);
            var shape = terrainNodes[index];

            var origin = new Vector3(0, 0, 0);

            //create a separate list to find the furthest valid one to place the exit
            var possibleExitNodes = new List<Shape>();
            possibleExitNodes.AddRange(terrainNodes);

            do
            {
                if (PlaceShapeOperation.occupiedObject.Any(x => x == shape.Position))
                    possibleExitNodes.RemoveAt(index);

                if (possibleExitNodes.Count == 0)
                    break;

                foreach (var node in possibleExitNodes.Where(node =>
                    Vector3.Distance(origin, node.Position) > Vector3.Distance(origin, shape.Position)))
                {
                    shape = node;
                }
            }
            while (PlaceShapeOperation.occupiedObject.Any(x => x == shape.Position) ||
                   shape.Symbol != Shape.SymbolEnum.EMPTY);

            EndRule.CalculateRule(shape);
        }

        private void PlaceContent(Shape.SymbolEnum contentType, IList<Shape> emptyNodes, IList<Rule> rules)
        {
            var counter = 0;
            while (emptyNodes.Count > 0)
            {
                counter++;
                if (counter > 1000)
                    break;

                var index = rnd.Next(emptyNodes.Count);
                var shape = emptyNodes[index];
                
                var rulesMatch = rules.Where(rule => shape.Symbol == contentType).ToList();

                if (rulesMatch.Count == 0) break;

                //Pick a random rule to apply
                var ruleChosen = rulesMatch[Random.Range(0, rulesMatch.Count)];
                ruleChosen.CalculateRule(shape);
                emptyNodes.RemoveAt(index);
            }
        }

        void Start()
        {
            GenerateLevel();
            NavigationBaker.BakeSurfaces();
        }
    }
}