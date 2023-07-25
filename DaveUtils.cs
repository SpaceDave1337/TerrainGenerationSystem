using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DaveUtils
{
    //utilities:
    public static float[,] PerlinToArray(int _xSize, int _ySize, float _seed, float _scale) //generate perlin noise 2d array with a seed, output is between 0 and 1
    {
        float[,] _output = new float[_xSize, _ySize];
        for (int x = 0; x < _xSize; x++)
        {
            for (int y = 0; y < _ySize; y++)
            {
                _output[x, y] = Mathf.PerlinNoise((x + _seed) * _scale, (y + _seed) * _scale);
            }
        }

        return _output;
    }

    public static float[,] CircularFalloff(int _xSize, int _ySize) //get a circular fall off map 2d array, output is between 0 and 1
    {
        float[,] _output = new float[_xSize, _ySize];
        float _centerX = _xSize / 2;
        float _centerY = _ySize / 2;

        float _maxRadius = Mathf.Min(_centerX, _centerY);

        for (int x = 0; x < _xSize; x++)
        {
            for (int y = 0; y < _ySize; y++)
            {
                float _XDistance = Mathf.Abs(x - _centerX);
                float _YDistance = Mathf.Abs(y - _centerY);

                float _centerDistance = Mathf.Sqrt(_XDistance * _XDistance + _YDistance * _YDistance);

                float _normalizedDistance = _centerDistance / _maxRadius;

                float _falloff = Mathf.Cos(_normalizedDistance * Mathf.PI * 0.5f);

                _output[x, y] = (_falloff + 1) / 2;
            }
        }
        return _output;
    }

    public static float Remap(float _value, float _fromMin, float _fromMax, float _toMin, float _toMax) //remap fuction
    {
        // Clamp the value within the "from" range
        float _clampedValue = Mathf.Clamp(_value, _fromMin, _fromMax);

        // Map the clamped value from the "from" range to the "to" range
        float _remappedValue = (_clampedValue - _fromMin) / (_fromMax - _fromMin) * (_toMax - _toMin) + _toMin;

        return _remappedValue;
    }
}
