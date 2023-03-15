using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour {
    // Start is called before the first frame update
    public GameObject[] trashObjects;
    public Transform parent;
    public enum Direction {
        North,
        South,
        East,
        West
    }
    private Vector2 maxVertex = Vector2.zero;
    private Dictionary<Direction, Vector2> directions = new Dictionary<Direction, Vector2>{
        { Direction.North, new Vector2(-1, 0) },
        { Direction.South, new Vector2(1, 0) },
        { Direction.East, new Vector2(0, 1) },
        { Direction.West, new Vector2(0, -1) }
    };
    public Direction OppositeDirection(Direction dir) {
        switch (dir) {
            case Direction.North:
                return Direction.South;
            case Direction.South:
                return Direction.North;
            case Direction.East:
                return Direction.West;
            default:
                return Direction.East;
        }
    }
    private Dictionary<Direction, Vector2> startPosition;
    private float heightMid;
    public Direction direction;
    public int probability;
    public int _probability {
        get {
            return probability;
        }
        set {
            probability = value;
        }
    }
    void Start() {
        GetComponent<WaterMesh>().MeshChanged += UpdateVertex;
    }
    void UpdateVertex(Mesh mesh) {
        maxVertex = new Vector2(int.MinValue, int.MinValue);
        Vector2 heightTemp = new Vector2(float.MaxValue, float.MinValue);
        foreach (Vector3 vertex in mesh.vertices) { 
            maxVertex = new Vector2(Mathf.Max(maxVertex.x, vertex.x), Mathf.Max(maxVertex.y, vertex.z));
            heightTemp = new Vector2(Mathf.Min(maxVertex.x, vertex.y), Mathf.Min(maxVertex.y, vertex.y));
        }
        heightMid = (heightTemp.x + heightTemp.y) / 2;
        startPosition = new Dictionary<Direction, Vector2>{
            {Direction.North, new Vector2(maxVertex.x * 2 / 3, -1)},
            {Direction.South, new Vector2(maxVertex.x *1/3, -1)},
            {Direction.East, new Vector2(-1, maxVertex.y * 1 / 3)},
            {Direction.West, new Vector2(-1, maxVertex.y * 2 / 3)},
        };
    }
    void Update() {
        if (maxVertex == Vector2.zero)
            UpdateVertex(GetComponent<WaterMesh>().mesh);
        if (Random.Range(0, 100) < probability) {
            GameObject go = trashObjects[Random.Range(0, trashObjects.Length)];
            Instantiate(
                go,
                gameObject.transform.position + new Vector3(0, 0, -maxVertex.y)/*No se el por qué de esta suma, pero funciona*/ +
                new Vector3(
                    startPosition[direction].x == -1 ? Random.Range(maxVertex.x * 1 / 3, maxVertex.x * 2 / 3) : startPosition[direction].x,
                    transform.position.y + 0.1f,
                    startPosition[direction].y == -1 ? Random.Range(maxVertex.y * 1 / 3, maxVertex.y * 2 / 3) : startPosition[direction].y
                ),
                go.transform.rotation,
                parent                
            ).GetComponent<TrashMovement>().Init(directions[direction], new Vector2(transform.position.x, -transform.position.z) + startPosition[OppositeDirection(direction)], GetComponent<WaterMesh>().maxHeight) ;
        }
    }
}
