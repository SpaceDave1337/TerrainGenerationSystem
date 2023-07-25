using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrasGeneration : MonoBehaviour, ITerrainBiomeGeneration
{
    [SerializeField] int grassLayer;
    [SerializeField] float grassOffset;
    public float[,,] GenerateMap(float[,,] _input, float[,] _heightMap, float _seed) //set parts of the map to be gras, if they're above a certain height
    {
        for (int i = 0; i < _input.GetLength(2); i++)
        {
            for (int x = 0; x < _input.GetLength(0); x++)
            {
                for (int y = 0; y < _input.GetLength(1); y++)
                {
                    if (i == grassLayer && _heightMap[x,y] > grassOffset)
                    {
                        _input[x, y, i] = 1;
                    }
                    else if (i != grassLayer && _heightMap[x,y] > grassOffset)
                    {
                        _input[x, y, i] = 0;
                    }
                }
            }

        }
        return _input;
    }
}
