
using System.Collections.Generic;
using static System.Math;
using UnityEngine;

public partial class BoardManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public enum TilePrefabNames { empty, grass_1, grass_2, grass_3, grass_4, baseTile, invertedGrass_1, invertedGrass_2, invertedGrass_3, invertedGrass_4, desert_empty, desert_1, desert_2, desert_3, desert_4 };
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
            biomes.Add(new DefaultBiome(Vector2Int.zero));
            biomes.Add(new Wastes(Vector2Int.zero));
            for (int i = 0; i < 7; i++)
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

        public virtual double getDistance(Vector2Int point) {
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

        public override double getDistance(Vector2Int point) {
            if ((point - center).sqrMagnitude <= 16)
                return -1;
            else
                return double.MaxValue;
        }
    }

    class Grasslands : Biome
    {

        public Grasslands(Vector2Int center) : base(center) { }

        public override Tile genTile(Vector2Int loc) {
            float r = Random.value;
            if (r < .5)         // Standard grass
                return Tile.InstantiateTile(bm.tilePrefabs[(int)TilePrefabNames.grass_1], tileHolder, loc);
            else if (r < .8)    // Bright grass
                return Tile.InstantiateTile(bm.tilePrefabs[(int)TilePrefabNames.grass_2], tileHolder, loc);
            else if (r < .95)   // Mossy rock thing
                return Tile.InstantiateTile(bm.tilePrefabs[(int)TilePrefabNames.grass_3], tileHolder, loc);
            else                // Scrap bush
                return Tile.InstantiateTile(bm.tilePrefabs[(int)TilePrefabNames.grass_4], tileHolder, loc);
        }
    }

    class InvertedGrasslands : Biome
    {

        public InvertedGrasslands(Vector2Int center) : base(center) { }

        public override Tile genTile(Vector2Int loc) {
            float r = Random.value;
            if (r < .5)         // Thing
                return Tile.InstantiateTile(bm.tilePrefabs[(int)TilePrefabNames.invertedGrass_1], tileHolder, loc);
            else if (r < .8)    // Other thing
                return Tile.InstantiateTile(bm.tilePrefabs[(int)TilePrefabNames.invertedGrass_2], tileHolder, loc);
            else if (r < .95)   // Mossy rock thing
                return Tile.InstantiateTile(bm.tilePrefabs[(int)TilePrefabNames.invertedGrass_3], tileHolder, loc);
            else                // Thing
                return Tile.InstantiateTile(bm.tilePrefabs[(int)TilePrefabNames.invertedGrass_4], tileHolder, loc);
        }
    }

    class Wastes : Biome
    {

        public Wastes(Vector2Int center) : base(center) { }

        public override Tile genTile(Vector2Int loc) {
            float r = Random.value;
            if (r < .9)         // Empty
                return Tile.InstantiateTile(bm.tilePrefabs[(int)TilePrefabNames.desert_empty], tileHolder, loc);
            else if (r < .9925) // Dead grass
                return Tile.InstantiateTile(bm.tilePrefabs[(int)TilePrefabNames.desert_1], tileHolder, loc);
            else if (r < .995)  // Radio tower
                return Tile.InstantiateTile(bm.tilePrefabs[(int)TilePrefabNames.desert_2], tileHolder, loc);
            else if (r < .9975) // Scrap Heap
                return Tile.InstantiateTile(bm.tilePrefabs[(int)TilePrefabNames.desert_3], tileHolder, loc);
            else                // Sand pit
                return Tile.InstantiateTile(bm.tilePrefabs[(int)TilePrefabNames.desert_4], tileHolder, loc);
        }

        public override double getDistance(Vector2Int point) {
            if (base.getDistance(point) > 40)
                return -1;
            else
                return double.MaxValue;
        }

    }

}
