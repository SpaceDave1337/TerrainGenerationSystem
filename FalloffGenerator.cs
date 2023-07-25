using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalloffGenerator : MonoBehaviour, ITerrainHeightGeneration
{
    [SerializeField] float _offset = 0f;
    [SerializeField] float _scale = 1f;
    public float[,] GenerateMap(float[,] _input, float _seed) ///will generate an output between 0 and 1
    {
        int _xSize = _input.GetLength(0);
        int _ySize = _input.GetLength(1);

        float[,] _output = DaveUtils.CircularFalloff(_xSize, _ySize);
        for (int x = 0; x < _xSize; x++)
        {
            for (int y = 0; y < _ySize; y++)
            {
                _output[x, y] = (_output[x, y] + _offset) * _scale;

                _output[x, y] *= _input[x, y];
            }
        }
        return _output;
    }
}
