using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour {
    // Start is called before the first frame update
    public GameObject[] trashObjects;
    public Transform parent;
    public Vector3 maxDistance;
    public Transform raft;
    public enum Direction {
        North,
        South,
        East,
        West
    }
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
        //GetComponent<WaterMesh>().MeshChanged += UpdateVertex;
        UpdateVertex(GetComponent<MeshFilter>().mesh);
    }
    void UpdateVertex(Mesh mesh) {
        startPosition = new Dictionary<Direction, Vector2>{
            {Direction.North, new Vector2(maxDistance.x, -1)},
            {Direction.South, new Vector2(-maxDistance.x, -1)},
            {Direction.East, new Vector2(-1, maxDistance.z)},
            {Direction.West, new Vector2(-1, -maxDistance.z)},
        };
    }
    void Update() {
        if (Random.Range(0, 100) < probability) {
            GameObject go = trashObjects[Random.Range(0, trashObjects.Length)];

            go = Instantiate(
                go,
                new Vector3(
                    startPosition[direction].x == -1 ? Random.Range(-maxDistance.x, maxDistance.x) : startPosition[direction].x,
                    transform.position.y + 2f,
                    startPosition[direction].y == -1 ? Random.Range(-maxDistance.z, maxDistance.z) : startPosition[direction].y
                ),
                go.transform.rotation,
                parent
            );
            go.GetComponent<TrashMovement>().Init(directions[direction], startPosition[OppositeDirection(direction)], maxDistance.y) ;
        }
    }
}
