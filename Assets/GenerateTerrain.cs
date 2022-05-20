using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.SceneManagement;


public class WorldSettings 
{
    public static int CurrentLevelNumber = 1;
    public static int DungeonHeight = 3;
    public static int DungeonWidth = 5;
}


public class GenerateTerrain : MonoBehaviour
{
    public void StartNextLevel(int heghtIncrease = 1, int widthIncrease = 1)
    {
        WorldSettings.DungeonHeight += heghtIncrease;
        WorldSettings.DungeonWidth += widthIncrease;
        WorldSettings.CurrentLevelNumber++;



        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }


    // Check how closely the room matches the required room shape
    int GetRoomSuitabilityScore(RoomMetadata required, RoomMetadata actual)
    {
        var totalDifference = 0;
        totalDifference += GetSideDifference(required.IsBottomOpen, actual.IsBottomOpen);
        totalDifference += GetSideDifference(required.IsTopOpen, actual.IsTopOpen);
        totalDifference += GetSideDifference(required.IsLeftOpen, actual.IsLeftOpen);
        totalDifference += GetSideDifference(required.IsRightOpen, actual.IsRightOpen);

        return totalDifference;

        int GetSideDifference(bool isOpeningRequired, bool doesOpeningExist)
        {
            if (isOpeningRequired)
            {
                if (doesOpeningExist)
                {
                    // Opening matches, don't penalize for this side
                    return 0;
                }
                // Opening not matching required, add large penalty
                return 10;
            }
            else
            {
                if (doesOpeningExist)
                {
                    // The opening exists when it doesn't need to, add a small penalty
                    return 1;
                }
                // No opening exists when one isn't required, don't assign penalty 
                return 0;
            }
        }
    }

    void Start()
    {
        if (Player == null) {
            Player = GameObject.FindWithTag("Player");
        }
        GenerateDungeon();
    }

    private void GenerateDungeon()
    {
        DungeonGenerator generator = new DungeonGenerator(height: WorldSettings.DungeonHeight, maxWidth: WorldSettings.DungeonWidth);

        // This variable gets the new starting point for each floor
        int currentXOffset = 0;
        for (int y = 0; y < generator.Height; y++)
        {
            var currentLayer = generator.Layers[y];

            var numberOfRooms = currentLayer.Rooms.Count();
            for (int x = 0; x < numberOfRooms; x++)
            {
                var currentRoomMetadata = currentLayer.Rooms[x];

                Vector2Int worldPosition = new Vector2Int(x - (currentLayer.EntranceOffset ?? 0) + currentXOffset, y);

                // This gets the most suitable rooms
                var suitableRooms = Rooms
                                    .Where(room => room.metadata.RoomType == currentRoomMetadata.RoomType) // Ensure that only matching room types are used
                                    .OrderBy(room => GetRoomSuitabilityScore(currentRoomMetadata, room.metadata)) // Sort the rooms by their suitability
                                    .GroupBy(room => GetRoomSuitabilityScore(currentRoomMetadata, room.metadata)) // Group up rooms with a similar suitability level
                                    .First() // Take the group of most suitable rooms
                                    .ToList(); //Convert back into a list
                
                Room randomRoom = GetRandomRoom(suitableRooms);

                //Add the new room to the map
                map[worldPosition] = new RoomState(randomRoom);
            }
            //Update the offset for the next floor's placement
            currentXOffset += (currentLayer.ExitOffset ?? 0) - (currentLayer.EntranceOffset ?? 0);
        }
    }

    private Room GetRandomRoom(List<Room> suitableRooms)
    {
        // Add a value of 1 to make sure that this works even if the probability is 0
        float probabilitySum = suitableRooms.Sum(room => room.metadata.Probability + 1);
        float randomNumber = UnityEngine.Random.Range(0f, probabilitySum);

        float currentTotal = 0;
        foreach (var room in suitableRooms)
        {
            currentTotal += room.metadata.Probability + 1;

            if (randomNumber <= currentTotal)
            {
                return room;
            }
        }

        throw new IndexOutOfRangeException("A room mistakenly wasn't chosen");

        // Randomly pick from the suitable rooms
        //return suitableRooms[UnityEngine.Random.Range(0,suitableRooms.Count())];
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartNextLevel(1, 1);
        }

        HandleRooms(Player.transform.position);
    }

    private Dictionary<Vector2Int, RoomState> map = new Dictionary<Vector2Int, RoomState>();
    private HashSet<Vector2Int> loadedChunksPositions = new HashSet<Vector2Int>();
    public GameObject Player;
    public GameObject MapParentObject;
    public int ChunkSizeX = 2;
    public int ChunkSizeY = 1;
    public int ChunkLoadRange = 5;
    public Room[] Rooms;



    public Room GetCurrentRoom()
    {
        var position = GetGridCoordinates(Player.transform.position);
        return map[new Vector2Int(position.x, position.y)].Room;
    }

    public Vector2Int GetGridCoordinates(Vector3 location)
    {
        int x = (int)Math.Round(location.x / ChunkSizeX);
        int y = (int)Math.Round(location.y / ChunkSizeY);

        return new Vector2Int(x, y);
    }
    private Vector2Int LastPosition = new Vector2Int(Int16.MaxValue, Int16.MaxValue);

    public void HandleRooms(Vector3 position3D)
    {
        var position = GetGridCoordinates(position3D);

        if (LastPosition == position)
        {
            // No change in position, don't update the chunks
            return;
        }

        HashSet<Vector2Int> RoomsToLoad = new HashSet<Vector2Int>();
        for (int i = -ChunkLoadRange; i <= ChunkLoadRange; i++)
        {
            for (int j = -ChunkLoadRange; j <= ChunkLoadRange; j++)
            {
                RoomsToLoad.Add(new Vector2Int(position.x + i, position.y + j));
            }
        }

        //Delete old chunks
        var roomsToUnload = loadedChunksPositions.Except(RoomsToLoad);
        foreach (var roomToUnload in roomsToUnload)
        {
            map[roomToUnload].Unload();
        }

        LoadRooms(RoomsToLoad);

        // Save this position
        LastPosition = position;
    }

    private void LoadRooms(HashSet<Vector2Int> ChunksToLoad)
    {
        foreach (var chunk in ChunksToLoad)
        {
            // Generate new chunks when visited if they don't already exist
            if (!map.ContainsKey(chunk))
            {
                map[chunk] = new RoomState(Rooms.Where(room =>
                {
                    var metadata = room.metadata;
                    return (!metadata.IsBottomOpen) && (!metadata.IsTopOpen) && (!metadata.IsLeftOpen) && (!metadata.IsRightOpen);
                }).First());
            }

            var obj = map[chunk];
            if (!obj.IsLoaded)
            {
                obj.Load(new Vector3(chunk.x * ChunkSizeX, chunk.y * ChunkSizeY, 0));
                obj.Instance.transform.parent = MapParentObject.transform;
                loadedChunksPositions.Add(chunk);
            }
        }
    }
}

[Serializable]
public class Room
{
    public GameObject prefab;
    public RoomMetadata metadata;
}

// Manages loading and unloading rooms
class RoomState
{
    public RoomState(Room room)
    {
        this.Room = room;
    }

    public void Load(Vector3 position)
    {
        IsLoaded = true;
        Instance = GameObject.Instantiate(Room.prefab);
        Instance.transform.position = position;
    }

    public void Unload()
    {
        IsLoaded = false;
        GameObject.Destroy(Instance.gameObject);

    }

    // Private so nothing messes with it
    public Room Room { get; }
    public GameObject Instance { get; private set; }
    public bool IsLoaded { get; private set; } = false;
}