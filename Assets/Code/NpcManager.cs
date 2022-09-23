using Pathfinding;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    private const int PAPER_LAYER = 7;
    private const int ROCK_LAYER = 6;
    private const int SCISSOR_LAYER = 8;

    [SerializeField] private int sideAmmount = 1;
    [SerializeField] private BasicNpcSpawner spawner;

    [Header("Setup")]

    [SerializeField] private Sprite paperSprite;
    [SerializeField] private Sprite rockSprite;
    [SerializeField] private Sprite scissorSprite;

    public static NpcManager Singleton {get; set;}

    public List<str_Npc> List_PaperGO { get; private set; }
    public List<str_Npc> List_RockGO { get; private set; }
    public List<str_Npc> List_ScissorsGO { get; private set; }

    private void Awake()
    {
        if (Singleton is null)
        {
            Singleton = this;
        }
        else
        {
            Debug.LogError("More than one AiManager instence instantiated");
            enabled = false;
        }

        List_PaperGO = new List<str_Npc>();
        List_RockGO = new List<str_Npc>();
        List_ScissorsGO = new List<str_Npc>();
    }

    private void Start()
    {
        spawner.SpawnNPCs(sideAmmount);

        #region Assign Target to each npc

        foreach (var paper in List_PaperGO)
        {
            var target = List_RockGO.First(x => x.chased == false);
            target.chased = true;
            paper.gameObject.GetComponent<AIDestinationSetter>().target = target.gameObject.transform;
        }

        foreach (var rock in List_RockGO)
        {
            var target = List_ScissorsGO.First(x => x.chased == false);
            target.chased = true;
            rock.gameObject.GetComponent<AIDestinationSetter>().target = target.gameObject.transform;
        }

        foreach (var scissor in List_ScissorsGO)
        {
            var target = List_PaperGO.First(x => x.chased == false);
            target.chased = true;
            scissor.gameObject.GetComponent<AIDestinationSetter>().target = target.gameObject.transform;
        }
        #endregion
    }

    public void AddPaperToList(GameObject paperGO)
    {
        List_PaperGO.Add(new str_Npc(paperGO,false));
    }

    public void AddRockToList(GameObject rockGO)
    {
        List_RockGO.Add(new str_Npc(rockGO, false));
    }
    public void AddScissorToList(GameObject scissorGO)
    {
        List_ScissorsGO.Add(new str_Npc(scissorGO, false));
    }
    public void RemovePaperFromList(GameObject paperGO)
    {
        List_PaperGO.Remove(List_PaperGO.First(x => x.gameObject == paperGO));
    }
    public void RemoveRockFromList(GameObject rockGO)
    {
        List_RockGO.Remove(List_RockGO.First(x => x.gameObject == rockGO));
    }
    public void RemoveScissorfromList(GameObject scissorGO)
    {
        List_ScissorsGO.Remove(List_ScissorsGO.First(x => x.gameObject == scissorGO));
    }


    public void ChangeRockToPaper(GameObject npc)
    {
        npc.GetComponent<SpriteRenderer>().sprite = paperSprite;

        npc.layer = PAPER_LAYER;

        RemoveRockFromList(npc);
        AddPaperToList(npc);

        npc.GetComponent<AIDestinationSetter>().target = List_RockGO.First().gameObject.transform;
    }

    public void ChangeScissorToRock(GameObject npc)
    {
        npc.GetComponent<SpriteRenderer>().sprite = rockSprite;

        npc.layer = ROCK_LAYER;

        RemoveScissorfromList(npc);
        AddRockToList(npc);

        npc.GetComponent<AIDestinationSetter>().target = List_ScissorsGO.First().gameObject.transform;
    }

    public void ChangePaperToScissor(GameObject npc)
    {
        npc.GetComponent<SpriteRenderer>().sprite = scissorSprite;

        npc.layer = SCISSOR_LAYER;

        RemovePaperFromList(npc);
        AddScissorToList(npc);

        npc.GetComponent<AIDestinationSetter>().target = List_PaperGO.First().gameObject.transform;
    }

    public struct str_Npc
    {
        public GameObject gameObject;
        public bool chased;

        public str_Npc(GameObject gameObject, bool chased)
        {
            this.gameObject = gameObject;
            this.chased = chased;
        }
    }
}
