using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightPerlinGenerator : MonoBehaviour, ITerrainHeightGeneration
{
    [SerializeField] float scale = 0.01f;
    [SerializeField] float weight = 0.5f;
    public float[,] GenerateMap(float[,] _input, float _seed)
    {
        int _xSize = _input.GetLength(0);
        int _ySize = _input.GetLength(1);

        float[,] _output = DaveUtils.PerlinToArray(_xSize, _ySize, _seed, scale);
        for (int x = 0; x < _xSize; x++)
        {
            for(int y = 0; y < _ySize; y++)
            {
                _output[x, y] *= weight;
                _output[x, y] += _input[x, y];
            }
        }
        return _output;
    }
}
