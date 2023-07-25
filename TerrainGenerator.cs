using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TerrainGenerator : MonoBehaviour, ISaveable
{
    //general
    float seed;
    Terrain terrain;
    TerrainData terrainData;
    //Note that Splatmap Resolution and Heightmap resolution HAVE to be the same, else all the scripts will break!

    //Heights
    float[,] heightMap;
    //Biomes/Colors
    float[,,] textureMap;

    //is initialisation done?
    bool initDone = false;

    private void Start()
    {
        terrain = gameObject.GetComponent<Terrain>();
        terrainData = terrain.terrainData;

        Invoke("FirstGeneration", 0.1f);
    }

    void FirstGeneration()
    {
        if (initDone) return;
        SpawnTerrain();
    }

    void SpawnTerrain() //generate new map & terrain
    {
        //generate new seed
        seed = UnityEngine.Random.Range(0f, 1000000f);
        //set heights
        SetHeights();
        //set biomes
        SetBiomes();
        //->generate colors on the floor, color alpha map
        //->generate detail map grass
        //populate map
        PopulateMap();
        //->place trees & vegetaion based on biome map
        //->place ores, stones etc

    }

    void LoadTerrain() //load the map from the save file
    {
        //load seed, should already be loaded
        //set heights
        SetHeights();
        //set biomes
        SetBiomes();
        //-> set colors on the floor
        //-> set grass
    }

    [ContextMenu("Set Heights")]
    void SetHeights()
    {
        //make new heightmap float
        heightMap = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];

        //get all terraingen modules
        ITerrainHeightGeneration[] _terrainHeightGenerations = gameObject.GetComponents<ITerrainHeightGeneration>();
        if (_terrainHeightGenerations == null) return;

        //go through each _terrainModule of the terrain
        foreach (ITerrainHeightGeneration _terrainModule in _terrainHeightGenerations)
        {
            //pass the heightmap through all terrain genertion modules
            heightMap = _terrainModule.GenerateMap(heightMap, seed);
        }
        
        //draw map and terrain

        terrainData.SetHeights(0, 0, heightMap);
    }

    [ContextMenu("Set Colors")]
    void SetBiomes()
    {
        //make a new Texture Alphamap
        textureMap = new float[terrainData.alphamapResolution, terrainData.alphamapResolution, terrainData.alphamapLayers];
        //get all biomegen modules
        ITerrainBiomeGeneration[] _terrainBiomeGenerations = gameObject.GetComponents<ITerrainBiomeGeneration>();
        if (_terrainBiomeGenerations == null) return;
        //go through each biomemodule of the terrain
        foreach (ITerrainBiomeGeneration _terrainModule in _terrainBiomeGenerations)
        {
            //pass the color alphamap through all terrain generation modules
            textureMap = _terrainModule.GenerateMap(textureMap, heightMap, seed);
        }
        //draw texture onto the terrain

        terrainData.SetAlphamaps(0,0,textureMap);
    }

    [ContextMenu("Populate Map")]
    void PopulateMap()
    {
        //get all gameobject generation modules
        ITerrainGameobjectGeneration[] _terrainGameobjectGenerations = gameObject.GetComponents<ITerrainGameobjectGeneration>();
        if (_terrainGameobjectGenerations == null) return;
        //go through every object spawner module
        foreach(ITerrainGameobjectGeneration _terrainModule in _terrainGameobjectGenerations)
        {
            _terrainModule.PopulateMap(textureMap, heightMap, seed, terrain);
        }
    }


    public object CaptureState() //saves the state
    {
        return new SaveData
        {
            terrainSeed = seed,
            initialGenerationDone = true
        };
    }

    public void RestoreState(object _state) //loads the state
    {
        SaveData _saveData = (SaveData)_state;
        seed = _saveData.terrainSeed;
        initDone = _saveData.initialGenerationDone;
        if (_saveData.initialGenerationDone)
        {
            LoadTerrain();
        }
    }

    [Serializable]
    struct SaveData
    {
        public float terrainSeed;
        public bool initialGenerationDone;
    }
}
