using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{


    public Sprite[] grassSprites;

    public GameObject tilePrefab;
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    BiomeManager bioM;
    public void generateBoard() {
        bioM = new BiomeManager();
        bioM.init();
    }

    class BiomeManager
    {
        List<Biome> biomes;
        public void init() {
            biomes = new List<Biome>();
            for (int i = 0; i < 6; i++) {
                biomes.Add(Biome.random());
            }
        }

        public Biome getBiome(Vector2 point) {
            //Get argmin distance
            Biome nearestBiome = biomes[0];
            foreach(Biome b in biomes) {
                if (b.getDistance(point) < nearestBiome.getDistance(point)) {
                    nearestBiome = b;
                }
            }
            return nearestBiome;
        }

    }

    class Biome
    {
        Vector2 center;
        public int label;
        float scale;
        int waves;

        public static Biome random() {
            Biome b = new Biome();
            b.center = new Vector2(UnityEngine.Random.Range(-30, 30), UnityEngine.Random.Range(-30, 30));
            b.label = UnityEngine.Random.Range(0, 3);
            b.scale = UnityEngine.Random.Range(.5f, 1.5f);
            b.waves = UnityEngine.Random.Range(4, 8);
            //UnityEngine.Debug.Log("Biome" + b.center.toString() + " --  Label " + b.label);
            return b;
        }

        public double getDistance(Vector2 point) {
            return (Vector2.Distance(center, point)+0.85*Math.Sin(waves * Math.PI / 180f * Vector2.SignedAngle(center - point, Vector2.right))) * scale;
        }
    }

    class TileInfo
    {

    }

    public void renderBoard() {
        for (int i = -30; i <= 30; i++) {
            for (int j = -30; j <= 30; j++) {
                Vector2 loc = new Vector2(i, j);
                GameObject t = Instantiate(tilePrefab, transform);
                t.transform.SetPositionAndRotation(loc, Quaternion.identity);
                t.GetComponent<Tile>().setSprite(grassSprites[bioM.getBiome(loc).label]);
            }
        }
    }



}
