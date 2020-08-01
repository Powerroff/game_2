
using System.Collections.Generic;
using static System.Math;
using UnityEngine;

public partial class BoardManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public enum TilePrefabNames { empty, grass_1, grass_2, baseTile, invertedGrass_1 };
    public GameObject tileHolder;

    Dictionary<Vector2Int, Tile> tileDict;

    BiomeManager bioM;

    public void generateBoard() {
        tileDict = new Dictionary<Vector2Int, Tile>();
        bioM = new BiomeManager();
        bioM.init();
    }

    class BiomeManager
    {
        List<Biome> biomes;
        public void init() {
            biomes = new List<Biome>();
            biomes.Add(new DefaultBiome(new Vector2Int(0, 0)));
            for (int i = 0; i < 5; i++)
                biomes.Add(Biome.random(new Vector2Int(Random.Range(-30, 30), Random.Range(-30, 30))));
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
        protected BoardManager bm;
        protected Transform tileHolder;
        protected Vector2Int center;
        float scale;
        int waves;

        public Biome(Vector2Int center) {
            bm = GameManager.instance.bm;
            tileHolder = bm.tileHolder.transform;
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

        public abstract Tile genTile(Vector2Int loc);
        
        public static Biome random(Vector2Int center) {
            int i = Random.Range(0, 2);
            switch(i) {
                case 0:
                    return new Grasslands(center);
                case 1:
                default:
                    return new InvertedGrasslands(center);
            }
        }
    }

    class DefaultBiome : Biome
    {
        GameObject baseTile;
        GameObject grass_0;
        GameObject grass_1;
        public DefaultBiome(Vector2Int center) : base(center) {
            baseTile = bm.tilePrefabs[(int)TilePrefabNames.baseTile];
            grass_0 = bm.tilePrefabs[(int)TilePrefabNames.empty];
            grass_1 = bm.tilePrefabs[(int)TilePrefabNames.grass_1];
        }

        public override Tile genTile(Vector2Int loc) {
            if (loc.Equals(center))
                return Tile.InstantiateTile(baseTile, tileHolder, loc);
            else if (Random.value > .1)
                return Tile.InstantiateTile(grass_0, tileHolder, loc);
            else
                return Tile.InstantiateTile(grass_1, tileHolder, loc);
        }
    }

    class Grasslands : Biome
    {
        GameObject grass_1;
        GameObject grass_2;

        public Grasslands(Vector2Int center) : base(center) {
            grass_1 = bm.tilePrefabs[(int)TilePrefabNames.grass_1];
            grass_2 = bm.tilePrefabs[(int)TilePrefabNames.grass_2];
        }

        public override Tile genTile(Vector2Int loc) {
            if (Random.value > .5)
                return Tile.InstantiateTile(grass_1, tileHolder, loc);
            else
                return Tile.InstantiateTile(grass_2, tileHolder, loc);
        }
    }

    class InvertedGrasslands : Biome
    {

        GameObject invertedGrass;
        public InvertedGrasslands(Vector2Int center) : base(center) { 
            invertedGrass = bm.tilePrefabs[(int)TilePrefabNames.invertedGrass_1];
        }

        public override Tile genTile(Vector2Int loc) {
            return Tile.InstantiateTile(invertedGrass, tileHolder, loc);
            //float r = Random.value;
            //if (r < .7)
            //    return new TileInfo(bm.invertedGrassSprites[1]);
            //else if (r < .9)
            //    return new TileInfo(bm.invertedGrassSprites[2]);
            //else if (r < .95)
            //    return new TileInfo(bm.invertedGrassSprites[3]);
            //else
            //    return new TileInfo(bm.invertedGrassSprites[4]);
        }
    }

}
