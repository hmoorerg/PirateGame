using System.Collections.Generic;
using System.Linq;
using System;


class DungeonGenerator
{
    public DungeonGenerator(int height, int maxWidth)
    {
        Height = height;
        MaxWidth = maxWidth;

        //Create the first layer which just has one entrace room with an open roof
        var entrance = new DungeonFloor(
            new RoomMetadata[]{
                new RoomMetadata()
                {
                    IsTopOpen = true
                }
            }
        );
        Layers.Add(entrance);

        // Generate the rest of the layers
        for (int i = 1; i < height - 1; i++)
        {
            int layerSize = random.Next(1, maxWidth + 1);
            int entranceOffset = random.Next(layerSize);
            int exitOffset = random.Next(layerSize);

            // Initialize the room array
            RoomMetadata[] rooms = new RoomMetadata[layerSize];
            for (int j = 0; j < rooms.Count(); j++)
            {
                // Assume that both sides are open for simplicity
                var room = new RoomMetadata()
                {
                    IsLeftOpen = true,
                    IsRightOpen = true,
                };
                rooms[j] = room;
            }

            // Close off the sides
            rooms[0].IsLeftOpen = false;
            rooms.Last().IsRightOpen = false;

            //Open up the entrances and exits
            rooms[entranceOffset].IsBottomOpen = true;
            rooms[exitOffset].IsTopOpen = true;

            Layers.Add(new DungeonFloor(rooms, entranceOffset, exitOffset));

        }

        var exit = new DungeonFloor(
            new RoomMetadata[]
            {
                new RoomMetadata()
                {
                    IsBottomOpen = true,
                }
            });
        Layers.Add(exit);
    }

    public int Height { get; }
    public int MaxWidth { get; }
    public List<DungeonFloor> Layers { get; private set; } = new List<DungeonFloor>();
    private System.Random random = new System.Random();
}

class DungeonFloor
{
    // Optional because there may not be an entrance
    public int? EntranceOffset = 0;
    // Optional because there may not be an exit
    public int? ExitOffset = 0;
    public RoomMetadata[] Rooms;

    public DungeonFloor(RoomMetadata[] rooms, int entranceOffset = 0, int exitOffset = 0)
    {
        Rooms = rooms;
        EntranceOffset = entranceOffset;
        ExitOffset = exitOffset;
    }
}

[Serializable]
public class RoomMetadata
{
    public bool IsTopOpen = false;
    public bool IsBottomOpen = false;
    public bool IsLeftOpen = false;
    public bool IsRightOpen = false;
    public float Probability = 1;

}