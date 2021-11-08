using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoxScripts;
using UnityEngine.UI;

public class Archive : MonoBehaviour
{
    
    [Header("Archive pages")]
    public List<ArchivePage> Pages;
    private List<int> ArchivePositions;
    private int forcedPage = -1;
    private int currentPage;
    [HideInInspector] public bool wantToForcePage = false;

    [Header("Nodes")]
    public float VerticalSpan = 54f;
    public GameObject nodePrefab;
    public GameObject pointPrefab;
    public Sprite EmptyCircle;
    public Sprite FilledCircle;
    [SerializeField] private List<GameObject> nodes;
    private float totalHeight = 0f;
    public GameObject NodesContainer;
    public Text ActionText;

    public ControllerData controller;

    private float flipTimer = 0f;

    [Header("Sounds")]
    
    public string openSound = "event:/";
    public string exitSound = "event:/";
    public string papelSound = "event:/";

    private void Awake() {
        ArchivePositions = new List<int>();
        nodes = new List<GameObject>();
    }

    public List<ArchivePage> notUsedPages;
    public List<ArchivePage> currentPages;

    public void SetPage(string Identifier)
    {
        // SEARCH FOR IDENTIFIER-LANG


    }

    public bool GetPage(string Identifier, out ArchivePage finalPage)
    {
        finalPage = default;
        if (currentPages.Count <= 0) return false;

        foreach (ArchivePage p in currentPages)
        {
            if(p.Identifier == Identifier) 
            {
                finalPage = p;
                return true;
            }
        }

        return false;
    }

    private void Update() {
        if(flipTimer > 0) flipTimer -= Time.deltaTime;
        /*if(controller.isInput2Down && Pages[GetPageFromListPost(currentPage)].isDoubleFaced && flipTimer <= 0)
        {
            flipTimer = 0.75f;
            Pages[GetPageFromListPost(currentPage)].Flip();
        }*/
    }

    private void DestroyNodes()
    {   if(nodes != null) foreach(GameObject diaryObject in nodes) Destroy(diaryObject);
        nodes = new List<GameObject>();
        NodesContainer.GetComponent<RectTransform>().anchoredPosition = new Vector2(96f, 0);
    }

    private void CreateNode()
    {

    }

    private void PlaceNewButton(ArchivePage page)
    {
        Vector2 anchoredPos = 
            currentPages.Count > 0 ?
            currentPages[currentPages.Count - 1].Button.GetComponent<RectTransform>().anchoredPosition + (Vector2.down * VerticalSpan):
            Vector2.zero;
        page.Button.transform.SetParent(NodesContainer.transform);
        page.Button.GetComponent<RectTransform>().localScale = Vector3.one;
        page.Button.GetComponent<RectTransform>().anchoredPosition = anchoredPos;
    }
}
