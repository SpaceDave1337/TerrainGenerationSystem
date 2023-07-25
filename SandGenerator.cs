using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandGenerator : MonoBehaviour, ITerrainBiomeGeneration
{
    [SerializeField] int sandLayer;
    public float[,,] GenerateMap(float[,,] _input, float[,] _heightMap, float _seed) //set the entire map to be sand
    {
        for (int i = 0; i < _input.GetLength(2); i++)
        {
            for(int x = 0; x < _input.GetLength(0); x++)
            {
                for (int y = 0; y < _input.GetLength(1); y++)
                {
                    if (i == sandLayer)
                    {
                        _input[x, y, i] = 1;
                    }
                    else
                    {
                        _input[x, y, i] = 0;
                    }
                }
            }

        }
        return _input;
    }
}
