using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaGatheringManager : GatheringManager
{
    public override void Start()
    {
        biome = DesignEnums.BiomeType.sea;
        base.Start();
    }
}
