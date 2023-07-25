using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPopulate : MonoBehaviour, ITerrainGameobjectGeneration
{
    [SerializeField] Transform wantedParent;

    [SerializeField] int amount;
    [SerializeField] int exclusionLayer;

    [SerializeField] GameObject item;
    [SerializeField] float yOffset;
    [SerializeField] float randomOffset;
    [SerializeField] float maxSideRotation;

    [SerializeField] float spawnDistance;
    [SerializeField] LayerMask _objectLayer;

    float xScale;
    float yScale;
    public void PopulateMap(float[,,] _biomeMap, float[,] _heightMap, float _seed, Terrain _terrain)
    {
        xScale = _terrain.terrainData.size.x / _terrain.terrainData.heightmapResolution;
        yScale = _terrain.terrainData.size.z / _terrain.terrainData.heightmapResolution;
        //for loop with wanted placed trees amount
        for (int i = 0; i < amount; i++)
        {
            bool _objectPlaced = false;
            while (!_objectPlaced)
            {
                //randomly choose a position on the terrain and try spawning a tree, try as long as a tree was placed
                int x = Random.Range(0, _biomeMap.GetLength(0));
                int y = Random.Range(0, _biomeMap.GetLength(1));
                //if a forest biome
                if (_biomeMap[x, y, exclusionLayer] == 0)
                {
                    //get real world position
                    Vector2 _2dPosition = GetRealWorldPosition(_terrain, y, x);
                    //add a small offset to this position
                    _2dPosition += new Vector2(Random.Range(-randomOffset, randomOffset), Random.Range(-randomOffset, randomOffset));
                    //cast raycast from above the map
                    RaycastHit _raycast;
                    if (Physics.Raycast(new Vector3(_2dPosition.x, _terrain.terrainData.size.y + 100, _2dPosition.y), new Vector3(0, -1, 0), out _raycast))
                    {

                        //spawn overlap sphere with a wanted size, check if any gameobjects with default tag are there, if so, return, else continue

                        if (Physics.OverlapSphere(_raycast.point, spawnDistance, _objectLayer).Length == 0) //if the gameobject was placed, let this be true
                        {
                            Quaternion _rotation = Quaternion.Euler(Random.Range(0, maxSideRotation), Random.Range(0, 360), 0);
                            //get hit position (only hit terrain with Ground Layer)
                            //-spawn the gameobject on the hit position
                            Instantiate(item, new Vector3(_raycast.point.x,_raycast.point.y + yOffset, _raycast.point.z), _rotation, wantedParent);
                            _objectPlaced = true;
                        }
                    }
                }
            }
        }
    }

    Vector2 GetRealWorldPosition(Terrain _terrain, int x, int y)
    {
        return new Vector2((x * xScale) + _terrain.GetPosition().x, (y * yScale) + _terrain.GetPosition().x);
    }
}