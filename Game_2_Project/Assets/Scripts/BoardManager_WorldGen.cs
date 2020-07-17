using System.Collections;
using System.Collections.Generic;
using static System.Math;
using UnityEngine;
using System.Diagnostics;

public partial class BoardManager : MonoBehaviour
{
    public Sprite[] grassSprites;
    public GameObject tilePrefab;

    Dictionary<Vector2Int, TileInfo> tileDict;

    BiomeManager bioM;

    public void generateBoard() {
        tileDict = new Dictionary<Vector2Int, TileInfo>();
        bioM = new BiomeManager();
        bioM.init();
    }

    class BiomeManager
    {
        List<Biome> biomes;
        public void init() {
            biomes = new List<Biome>();
            biomes.Add(new DefaultBiome(new Vector2Int(0, 0)));
        }

        public Biome getBiome(Vector2Int point) {
            //Get argmin distance
            Biome nearestBiome = biomes[0];
            foreach (Biome b in biomes) {
                if (b.getDistance(point) < nearestBiome.getDistance(point)) {
                    nearestBiome = b;
                }
            }
            return nearestBiome;
        }

    }

    abstract class Biome
    {
        protected BoardManager bm = GameManager.instance.bm;
        Vector2Int center;
        float scale;
        int waves;

        public Biome(Vector2Int center) {
            this.center = center;
            scale = UnityEngine.Random.Range(.5f, 1.5f);
            waves = UnityEngine.Random.Range(4, 8);
        }

        public Biome(Vector2Int center, float scale, int waves) {
            this.center = center;
            this.scale = scale;
            this.waves = waves;
        }

        public double getDistance(Vector2Int point) {
            return (Vector2.Distance(center, point) + 0.85 * Sin(waves * PI / 180f * Vector2.SignedAngle(center - point, Vector2.right))) * scale;
        }

        public abstract TileInfo genTile();
        
    }

    class DefaultBiome : Biome
    {
        public DefaultBiome(Vector2Int center) : base(center) { }

        public override TileInfo genTile() {
            if (Random.value > .1)
                return new TileInfo(bm.grassSprites[0]);
            else
                return new TileInfo(bm.grassSprites[1]);
        }
    }

    class TileInfo
    {
        public bool explored;
        public Sprite sprite;
        public TileInfo(Sprite sprite) {
            explored = false;
            this.sprite = sprite;
        }
    }
}
