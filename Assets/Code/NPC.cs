using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPC : MonoBehaviour
{

    [SerializeField,Range(0,2)] public int npcValue = 0;

    public enum NpcType {Paper,Rock,Scissor};
    public NpcType npcType { get; private set; }

    private void Awake()
    {
        npcType = (NpcType)npcValue;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var npcComponent = collision.gameObject.GetComponent<NPC>();

        if (npcComponent is null) return;

        switch (npcType)
        {
            case NpcType.Paper:
                if (npcComponent.npcType == NpcType.Rock)
                {
                    //Convert Rock to Paper.
                    npcComponent.npcType = NpcType.Paper;
                    NpcManager.Singleton.ChangeRockToPaper(collision.gameObject);

                    //Set new target.
                    GetComponent<AIDestinationSetter>().target = NpcManager.Singleton.List_RockGO.First().gameObject.transform;
                }
                break;

            case NpcType.Rock:
                if (npcComponent.npcType == NpcType.Scissor)
                {
                    //Convert Scissor to Rock.
                    npcComponent.npcType = NpcType.Rock;
                    NpcManager.Singleton.ChangeScissorToRock(collision.gameObject);

                    //Set new target.
                    GetComponent<AIDestinationSetter>().target = NpcManager.Singleton.List_ScissorsGO.First().gameObject.transform;
                }
                break;

            case NpcType.Scissor:
                if (npcComponent.npcType == NpcType.Paper)
                {
                    //Convert Paper to Scissor.
                    npcComponent.npcType = NpcType.Scissor;
                    NpcManager.Singleton.ChangePaperToScissor(collision.gameObject);

                    //Set new target.
                    GetComponent<AIDestinationSetter>().target = NpcManager.Singleton.List_PaperGO.First().gameObject.transform;
                }
                break;
        }
    }
}
