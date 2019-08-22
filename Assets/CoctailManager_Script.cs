using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CoctailManager_ProgressEvent: UnityEvent<float>
{

};

public class CoctailManager_Script : MonoBehaviour {

    public GameObject Unit = null;
    public GameObject UIUnit = null;
    public Coctail_Script CoctailUI = null;

    public CoctailManager_ProgressEvent OnProgress = new CoctailManager_ProgressEvent();

    private List<Coctail_Class> fCoctails = new List<Coctail_Class>();

    private string INI_COCTAILS_INFO_SECTION_NAME = "COCTAILS_INFO";
    private string INI_COCTAILS_COUNT_VALUE = "Count";


    public void LoadData(INIParser data)
    {
        Clear();

        OnProgress.Invoke(0);

        int count = data.ReadValue(INI_COCTAILS_INFO_SECTION_NAME, INI_COCTAILS_COUNT_VALUE, 0);
        for (int i = 0; i < count; i++)
        {
            Coctail_Class cls = new Coctail_Class();
            cls.Zero();
            cls.LoadData(data, i + 1);
            fCoctails.Add(cls);
            OnProgress.Invoke((float)(i + 1) / count);
        }

        OnProgress.Invoke(1);
    }

    public void SaveData(INIParser data)
    {
        OnProgress.Invoke(0);
        data.WriteValue(INI_COCTAILS_INFO_SECTION_NAME, INI_COCTAILS_COUNT_VALUE, fCoctails.Count);

        for (int i = 0; i < fCoctails.Count; i++)
        {
            fCoctails[i].SaveData(data, i + 1);
            OnProgress.Invoke((float)(i + 1) / fCoctails.Count);
        }

        OnProgress.Invoke(1);
    }

    public void ShowCoctail(int index)
    {
        if (!IsValidIndex(index)) return;

        Debug.Log("show coctail index: " + index);
        CoctailUI.SetData = fCoctails[index];
        CoctailUI.UpdateUI(false);
    }

    public void ShowCoctail(string name)
    {
        int index = CoctailIndexByName(name);
        Debug.Log("coctail_index: " + index);
        ShowCoctail(index);
    }

    public void ZeroCoctailUI()
    {
        CoctailUI.Zero();
    }

    public void AddCoctail(Coctail_Class coctail)
    {
        if (CheckCoctailName(coctail.Name)) return;

        fCoctails.Add(coctail);
    }

    public void RemoveCoctail(Coctail_Class coctail)
    {
        fCoctails.Remove(coctail);
    }

    public void RemoveCoctail(string name)
    {
        int index = CoctailIndexByName(name);

        if (IsValidIndex(index))
        {
            fCoctails.RemoveAt(index);
        }
    }

    public int CoctailIndexByName(string name)
    {
        for (int i = 0; i < fCoctails.Count; i++)
        {
            if (fCoctails[i].Name.ToUpper() == name.ToUpper())
            {
                return i;
            }
        }

        return -1;
    }

    public int MatchCoctailByString(string s, bool TrimSpaces, out string[] matches)
    {
        List<string> match_list;
        MatchCoctailByString(s, TrimSpaces, out match_list);
        matches = match_list.ToArray();
        return match_list.Count;
    }

    public int MatchCoctailByString(string s, bool TrimSpaces, out List<string> matches)
    {
        List<string> match_list = new List<string>();
        string match_string = s.ToUpper();

        if (TrimSpaces)
            match_string = match_string.Trim();
        
        for (int i = 0; i < fCoctails.Count; i++)
        {
            string name = fCoctails[i].Name.ToUpper();

            if (name.StartsWith(match_string))
            {
                match_list.Add(name);
            }
        }

        matches = match_list;

        return match_list.Count;
    }

    public int CoctailsCount
    {
        get
        {
            return fCoctails.Count;
        }
    }

    public Coctail_Class Coctail(int index)
    {
        return fCoctails[index];
    }

    public bool CheckCoctailName(string name)
    {
        return CoctailIndexByName(name) != -1;
    }

    public bool IsValidIndex(int index)
    {
        return (index >= 0) && (index < fCoctails.Count);
    }

    public void Clear()
    {
        fCoctails.Clear();
        ZeroCoctailUI();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
