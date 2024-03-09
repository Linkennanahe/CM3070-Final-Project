using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AutoMove : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust the speed as needed

    Animator animator;
    SpriteRenderer spriteRenderer;

    Transform player; // Reference to the player's transform

    public Tilemap walkableTilemap; // Assign the walkable tilemap in the Unity Editor

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Automatically find the player by tag
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogError("Player not found! Make sure the player has the 'Player' tag.");
        }

        // Get reference to the walkable tilemap
        if (walkableTilemap != null)
        {
            walkableTilemap = walkableTilemap.GetComponent<Tilemap>();
        }

        if (walkableTilemap == null)
        {
            Debug.LogError("Walkable tilemap not assigned! Assign the walkable tilemap GameObject in the Unity Editor.");
        }
    }

    void Update()
    {
        MoveTowardsPlayer();
        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        // Check the direction of movement
        Vector3Int currentCell = walkableTilemap.WorldToCell(transform.position);
        Vector3Int targetCell = walkableTilemap.WorldToCell(player.position);

        Vector2 movement = new Vector2(targetCell.x - currentCell.x, targetCell.y - currentCell.y);

        if (movement != Vector2.zero)
        {
            animator.SetBool("isMoving", true);
            // Flip the sprite when moving left
            spriteRenderer.flipX = movement.x < 0;
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    void MoveTowardsPlayer()
    {
        if (player != null && walkableTilemap != null)
        {
            // Get the current position in grid coordinates
            Vector3Int currentCell = walkableTilemap.WorldToCell(transform.position);

            // Get the player's position in grid coordinates
            Vector3Int targetCell = walkableTilemap.WorldToCell(player.position);

            // Perform A* pathfinding to get the next cell in the path
            List<Vector3Int> path = AStarPathfinding(currentCell, targetCell);

            if (path.Count > 0)
            {
                // Move towards the next cell in the path
                Vector3 nextPosition = walkableTilemap.GetCellCenterWorld(path[0]);
                transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);
            }
        }
    }

    List<Vector3Int> AStarPathfinding(Vector3Int start, Vector3Int target)
    {
        // Create a priority queue for the open list
        PriorityQueue<Vector3Int> openList = new PriorityQueue<Vector3Int>();
        openList.Enqueue(start, 0);

        // Create dictionaries to store gCost, hCost, and parent
        Dictionary<Vector3Int, float> gCost = new Dictionary<Vector3Int, float>();
        Dictionary<Vector3Int, float> hCost = new Dictionary<Vector3Int, float>();
        Dictionary<Vector3Int, Vector3Int> parent = new Dictionary<Vector3Int, Vector3Int>();

        // Initialize start node
        gCost[start] = 0;
        hCost[start] = Heuristic(start, target);

        while (openList.Count > 0)
        {
            Vector3Int current = openList.Dequeue();

            if (current == target)
            {
                // Reconstruct path
                List<Vector3Int> path = new List<Vector3Int>();
                while (parent.ContainsKey(current))
                {
                    path.Add(current);
                    current = parent[current];
                }
                path.Reverse();
                return path;
            }

            foreach (Vector3Int neighbor in GetNeighbors(current))
            {
                float cost = gCost[current] + Vector3Int.Distance(current, neighbor);

                if (!gCost.ContainsKey(neighbor) || cost < gCost[neighbor])
                {
                    gCost[neighbor] = cost;
                    hCost[neighbor] = Heuristic(neighbor, target);
                    float priority = cost + hCost[neighbor];
                    openList.Enqueue(neighbor, priority);
                    parent[neighbor] = current;
                }
            }
        }

        // If no path found, return an empty list
        return new List<Vector3Int>();
    }

    List<Vector3Int> GetNeighbors(Vector3Int current)
    {
        List<Vector3Int> neighbors = new List<Vector3Int>();

        // Add adjacent cells as neighbors if they are walkable
        TryAddNeighbor(neighbors, new Vector3Int(current.x + 1, current.y, current.z));
        TryAddNeighbor(neighbors, new Vector3Int(current.x - 1, current.y, current.z));
        TryAddNeighbor(neighbors, new Vector3Int(current.x, current.y + 1, current.z));
        TryAddNeighbor(neighbors, new Vector3Int(current.x, current.y - 1, current.z));

        return neighbors;
    }

    void TryAddNeighbor(List<Vector3Int> neighbors, Vector3Int position)
    {
        // Check if the position is within the walkable tilemap bounds and if the tile at that position is walkable
        if (walkableTilemap != null && walkableTilemap.HasTile(position))
        {
            neighbors.Add(position);
        }
    }

    float Heuristic(Vector3Int a, Vector3Int b)
    {
        // Manhattan distance heuristic
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y) + Mathf.Abs(a.z - b.z);
    }
}

// Priority queue implementation for A*
public class PriorityQueue<T>
{
    private List<(T, float)> elements = new List<(T, float)>();

    public int Count
    {
        get { return elements.Count; }
    }

    public void Enqueue(T item, float priority)
    {
        elements.Add((item, priority));
    }

    public T Dequeue()
    {
        int bestIndex = 0;

        for (int i = 1; i < elements.Count; i++)
        {
            if (elements[i].Item2 < elements[bestIndex].Item2)
            {
                bestIndex = i;
            }
        }

        T bestItem = elements[bestIndex].Item1;
        elements.RemoveAt(bestIndex);
        return bestItem;
    }
}
