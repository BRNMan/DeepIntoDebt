using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public Tilemap floor;
    public Vector2 size= new Vector2(256,256);
    public Tile mainTile;
    public List<Tile> tiles;
    public float clusterRate = 0.01f;

    public float clusterSize = 2.0f;

    public TilemapCollider2D tilemapCollider;

    // Start is called before the first frame update
    void Start()
    {
        GenerateFloor();
        SetCameraBounds();
    }

    private void SetCameraBounds() {
        
    }

    private void GenerateFloor() {
        floor.transform.position -= new Vector3(size.x*floor.cellSize.x/2-11, size.y*floor.cellSize.y/2, 0.0f);
        for(int x = 0; x < size.x; x++) {
            for(int y = 0; y < size.y; y++) {
                floor.SetTile(new Vector3Int(x,y,0),mainTile);
            }
        }

        for(int x = 0; x < size.x; x++) {
            for(int y = 0; y < size.y; y++) {
                if(Random.Range(0.0f,1.0f) < clusterRate) {
                    //Make a cluster
                    //Get all tiles in radius.
                    for(int x2 = (int)Mathf.Clamp(x-clusterSize,0,size.x); x2 < (int)Mathf.Clamp(x+clusterSize,0,size.x); x2++) {
                        for(int y2 = (int)Mathf.Clamp(y-clusterSize,0,size.y); y2 < (int)Mathf.Clamp(y+clusterSize,0,size.y); y2++) {
                            if(Vector2.Distance(new Vector2(x,y), new Vector2(x2,y2)) < clusterSize) {
                                int randTileIndex = Random.Range(0,tiles.Count);
                                Tile randomTile = tiles[randTileIndex];
                                floor.SetTile(new Vector3Int(x2,y2,0), randomTile);
                            }
                        }
                    }
                }
            }
        }

        tilemapCollider.ProcessTilemapChanges();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
