using UnityEngine;

public class ArchiveButton : MonoBehaviour {

    private Archive archive;
    private bool isActive = false;
    public GameObject Dot;

    public bool isNew = true;

    public void ToggleButton()
    {
        if(isNew)
        {
            isNew = false;
            // DESTROY ANIMATION
        }


        isActive = !isActive;
        if(isActive) archive.SetPage(gameObject.name);
        Dot.SetActive(isActive);
    }

    private void Awake() {
        isActive = true;
        ToggleButton();
    }
}