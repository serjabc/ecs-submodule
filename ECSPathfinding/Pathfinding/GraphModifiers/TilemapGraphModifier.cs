﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ME.ECS.Pathfinding {
    
    public class TilemapGraphModifier : GraphModifierBase {

        [System.Serializable]
        public struct Item {

            public UnityEngine.Tilemaps.TileBase requiredTile;
            public int tag;
            public int penaltyDelta;
            public float heightDelta;
            public bool modifyWalkability;
            public bool walkable;

        }

        public UnityEngine.Tilemaps.Tilemap tilemap;
        public Item[] items;
        public BoundsInt bounds;

        public override void ApplyBeforeConnections(Graph graph) {

            var halfOffset = new Vector3(this.tilemap.cellSize.x, 0f, this.tilemap.cellSize.z) * 0.5f;
            
            var visited = PoolHashSet<Node>.Spawn();
            foreach (var pos in this.bounds.allPositionsWithin) {

                var worldPos = pos + halfOffset;
                var cellPos = this.tilemap.layoutGrid.WorldToCell(worldPos);
                var tile = this.tilemap.GetTile(cellPos);
                for (int i = 0; i < this.items.Length; ++i) {

                    var item = this.items[i];
                    if (item.requiredTile == tile) {

                        var result = PoolList<Node>.Spawn(1);
                        graph.GetNodesInBounds(result, new Bounds(worldPos, this.tilemap.cellSize * 0.5f));
                        foreach (var node in result) {

                            if (visited.Contains(node) == false) {

                                visited.Add(node);
                                
                                node.penalty += item.penaltyDelta;
                                node.height += item.heightDelta;
                                node.tag = item.tag;

                            }

                        }
                        PoolList<Node>.Recycle(ref result);

                    } 

                }
                
            }
            PoolHashSet<Node>.Recycle(ref visited);
            
        }

        public override void ApplyAfterConnections(Graph graph) {
            
            var halfOffset = new Vector3(this.tilemap.cellSize.x, 0f, this.tilemap.cellSize.z) * 0.5f;

            var visited = PoolHashSet<Node>.Spawn();
            foreach (var pos in this.bounds.allPositionsWithin) {

                var worldPos = pos + halfOffset;
                var cellPos = this.tilemap.layoutGrid.WorldToCell(worldPos);
                var tile = this.tilemap.GetTile(cellPos);
                for (int i = 0; i < this.items.Length; ++i) {

                    var item = this.items[i];
                    if (item.modifyWalkability == false) continue;
                    
                    if (item.requiredTile == tile) {

                        var result = PoolList<Node>.Spawn(1);
                        graph.GetNodesInBounds(result, new Bounds(worldPos, this.tilemap.cellSize * 0.5f));
                        foreach (var node in result) {

                            if (visited.Contains(node) == false) {

                                visited.Add(node);
                                node.walkable = item.walkable;

                            }

                        }
                        PoolList<Node>.Recycle(ref result);

                    } 

                }
                
            }
            PoolHashSet<Node>.Recycle(ref visited);
            
        }

        public override void OnDrawGizmos() {

            var bounds = new Bounds(this.bounds.center, this.bounds.size);
            
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(bounds.center, bounds.size);
            
        }

    }

}
