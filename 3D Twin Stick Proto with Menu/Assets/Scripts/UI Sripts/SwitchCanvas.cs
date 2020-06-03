using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//This scene has been done by Aziz Ali
public class SwitchCanvas : MonoBehaviour
{
    //GameObjects
    public GameObject CanvasOff;
    public GameObject CanvasOn;
    public GameObject FirstObjectSelected;

    //This function allows you to change canvas'
    public void ChangeCanvas()
    {
        CanvasOff.SetActive(true);
        CanvasOn.SetActive(false);
        //This allows you to choose which button is first selected on the new canvas
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(FirstObjectSelected, null);
    }
}
