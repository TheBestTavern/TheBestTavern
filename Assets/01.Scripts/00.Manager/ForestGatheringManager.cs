using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using static DesignEnums;

public class ForestGatheringManager : GatheringManager
{
    public GatheringMapController gatheringMapController;

    public override void Start()
    {
        gatheringMapController.CreateMapProps();
        biome = DesignEnums.BiomeType.forest;
        base.Start();
    }
}

