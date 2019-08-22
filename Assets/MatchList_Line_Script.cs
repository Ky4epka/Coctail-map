using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MatchList_Line_Script : MonoBehaviour {

    public RectTransform Body = null;
    public GameObject Unit = null;

    public Text ValueText = null;
    public GameObject LineBreaker = null;

    public SubmitEvent OnSubmit = new SubmitEvent();

    public void ShowLineBreaker(bool value)
    {
        LineBreaker.SetActive(value);
    }

    public string Value
    {
        get
        {
            return ValueText.text;
        }

        set
        {
            ValueText.text = value;
        }
    }

    public void OnPointerDown(BaseEventData eventData)
    {
        Debug.Log("Line pressed:" + Value);
        OnSubmit.Invoke(Value);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
