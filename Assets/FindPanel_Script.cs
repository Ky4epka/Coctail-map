using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class SubmitEvent: UnityEvent<string>
{

}

public class FindPanel_Script : MonoBehaviour {

        public RectTransform Body = null;
        public GameObject Unit = null;

        public CoctailManager_Script CoctailManager = null;

        public InputField FindInput = null;
        public MatchList_Script MatchList = null;
        public GameObject ClearInputButtonUnit = null;

        public SubmitEvent OnSubmit = new SubmitEvent();

    public void OnInputValueChanged(string value)
    {
        if (value.Trim() == "")
        {
            MatchList.HideList();
            ClearInputButtonUnit.SetActive(false);
            return;
        }
        else
        {
            ClearInputButtonUnit.SetActive(true);
        }

        List<string> list = new List<string>();
        CoctailManager.MatchCoctailByString(value, true, out list);
        MatchList.ShowList(list);
    }

    public void OnInputSubmit(string value)
    {
        OnSubmit.Invoke(value);
    }

    public string FindText
    {
        get
        {
            return FindInput.text;
        }

        set
        {
            FindInput.text = value;
        }
    }

    public void ClearInput()
    {
        MatchList.HideList();
        FindText = "";
        ClearInputButtonUnit.SetActive(false);
    }
    
	// Use this for initialization
	void Start () {
        MatchList.OnSubmit.AddListener(OnInputSubmit);
        FindInput.onValueChanged.AddListener(OnInputValueChanged);
        ClearInput();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
