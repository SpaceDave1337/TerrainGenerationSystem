using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveable //objects the saving system can save
{
    object CaptureState();

    void RestoreState(object _state);
}

public interface ITerrainHeightGeneration //scripts that are part of the terrain generation stack for height generation
{
    float[,] GenerateMap(float[,] _input, float _seed);
}

public interface ITerrainBiomeGeneration //scripts that are part of the terrain generation stack for biome generation
{
    float[,,] GenerateMap(float[,,] _input, float[,] _heightMap, float _seed);
}

public interface ITerrainGameobjectGeneration //scripts that are part of the terrain generation stack for placing gameObjects on the terrain
{
    void PopulateMap(float[,,] _biomeMap, float[,] _heightMap, float _seed, Terrain _terrain);
}
