﻿using System;
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

            var counter = 0;
            while (terrainNodes.Count > 0)
            {
                var index = rnd.Next(terrainNodes.Count);
                var shape = terrainNodes[index];

                //Get valid random node
                while (PlaceShapeOperation.occupiedObject.Any(x => x == shape.Position) || shape.Symbol != Shape.SymbolEnum.EMPTY)
                {
                    if (PlaceShapeOperation.occupiedObject.Any(x => x == shape.Position))
                        terrainNodes.RemoveAt(index);

                    if (terrainNodes.Count == 0)
                        break;

                    index = rnd.Next(terrainNodes.Count);
                    shape = terrainNodes[index];
                }


                counter ++;
                if (counter > MaxNumberBlockLevel)
                {
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

                        foreach (var node in possibleExitNodes)
                        {
                            if (Vector3.Distance(origin, node.Position) > Vector3.Distance(origin, shape.Position))
                            {
                                shape = node;
                            }
                        }
                    } 
                    while (PlaceShapeOperation.occupiedObject.Any(x => x == shape.Position) ||
                             shape.Symbol != Shape.SymbolEnum.EMPTY);

                    EndRule.CalculateRule(shape);
                    break;
                }

                

                //get the rules that can append to the chosen empty node (shape)
                var rulesMatch = Rules.Where(rule => rule.PredecessorShape.Symbol == shape.Symbol).ToList();

                if (rulesMatch.Count == 0)
                {
                    break;
                }

                //Pick a random rule to apply
                var ruleChosen = rulesMatch[Random.Range(0, rulesMatch.Count)];
                var (results, succeeded) = ruleChosen.CalculateRule(shape);

                if (succeeded)
                    terrainNodes.RemoveAt(index);

                if (results.Count == 0)
                    continue;

                var test = results.Where(x => x.Symbol == Shape.SymbolEnum.EMPTY).ToList();

                test = test.Where(x => x.Position.x % 20 == 0).ToList();

                terrainNodes.AddRange(test);
                contentNodes.AddRange(results.Where(x => x.Symbol == Shape.SymbolEnum.CONTENT).ToList());
                enemyNodes.AddRange(results.Where(x => x.Symbol == Shape.SymbolEnum.ENEMY).ToList());
                
            }

            while (terrainNodes.Count > 0)
            {
                var index = rnd.Next(terrainNodes.Count);
                var shape = terrainNodes[index];

                while (PlaceShapeOperation.occupiedObject.Any(x => x == shape.Position) || shape.Symbol != Shape.SymbolEnum.EMPTY || shape.Position.x % 20 != 0)
                {
                    if (PlaceShapeOperation.occupiedObject.Any(x => x == shape.Position) || shape.Position.x % 20 != 0)
                        terrainNodes.RemoveAt(index);

                    if(terrainNodes.Count == 0)
                        break;

                    index = rnd.Next(terrainNodes.Count);
                    shape = terrainNodes[index];
                }

                BlockTunnelRule.CalculateRule(shape);
                try
                {
                    terrainNodes.RemoveAt(index);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Debug.Log("Out of range");
                }
            }

            counter = 0;
            while (contentNodes.Count > 0)
            {
                counter++;
                if (counter > 1000)
                    break;

                var index = rnd.Next(contentNodes.Count);
                var shape = contentNodes[index];

                //get the rules that can append to the chosen empty node (shape)
                var rulesMatch = ContentRules.Where(rule => Shape.SymbolEnum.CONTENT == shape.Symbol).ToList();

                if (rulesMatch.Count == 0) break;

                //Pick a random rule to apply
                var ruleChosen = rulesMatch[Random.Range(0, rulesMatch.Count)];
                ruleChosen.CalculateRule(shape);
                contentNodes.RemoveAt(index);
            }

            counter = 0;
            while (enemyNodes.Count > 0)
            {
                counter++;
                if (counter > 1000)
                    break;

                var index = rnd.Next(enemyNodes.Count);
                var shape = enemyNodes[index];

                //get the rules that can append to the chosen empty node (shape)
                var rulesMatch = EnnemyRules.Where(rule => Shape.SymbolEnum.ENEMY == shape.Symbol).ToList();

                if (rulesMatch.Count == 0) break;

                //Pick a random rule to apply
                var ruleChosen = rulesMatch[Random.Range(0, rulesMatch.Count)];
                ruleChosen.CalculateRule(shape);
                enemyNodes.RemoveAt(index);
            }
        }

        void Start()
        {
            GenerateLevel();
            NavigationBaker.BakeSurfaces();
        }
    }
}