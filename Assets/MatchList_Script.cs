using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MatchList_Script : MonoBehaviour {

    public GameObject Unit = null;

    public FindPanel_Script FindPanel = null;
    public GameObject ContentUnit = null;
    public RectTransform ContentBody = null;
    public MatchList_Line_Script LineSample = null;
    public GameObject NoLinesUnit = null;

    public SubmitEvent OnSubmit = new SubmitEvent();

    public List<MatchList_Line_Script> Lines = new List<MatchList_Line_Script>();


    public void ShowList(List<string> list)
    {
        int sibl_ind = LineSample.Body.GetSiblingIndex();
        ContentUnit.SetActive(true);
        Clear();

        if (list.Count == 0)
        {
            NoLinesUnit.SetActive(true);
        }
        else
        {

            NoLinesUnit.SetActive(false);
        }

        for (int i = 0; i<list.Count; i++)
        {
            sibl_ind++;
            MatchList_Line_Script line = MatchList_Line_Script.Instantiate(LineSample, ContentBody);
            line.Unit.SetActive(true);
            line.Value = list[i];
            line.OnSubmit.AddListener(OnLineSubmit);
            line.Body.SetSiblingIndex(sibl_ind);

            if (i == list.Count - 1)
                line.ShowLineBreaker(false);

            Lines.Add(line);
        }
    }

    public void HideList()
    {
        Clear();
        ContentUnit.SetActive(false);
    }

    public void OnLineSubmit(string value)
    {
        if (value == "no_lines")
        {
            FindPanel.ClearInput();
            return;
        }

        OnSubmit.Invoke(value);
    }

    public void Clear()
    {
        for (int i = 0; i < Lines.Count; i++)
        {
            MatchList_Line_Script.Destroy(Lines[i].Unit);
        }

        Lines.Clear();
    }

	// Use this for initialization
	void Start () {
        HideList();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
