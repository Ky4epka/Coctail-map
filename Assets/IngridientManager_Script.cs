using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class IngridientManager_ProgressEvent: UnityEvent<float>
{
};

public class IngridientManager_Script : MonoBehaviour {

        public RectTransform Body = null;
        public GameObject Unit = null;

        public RectTransform IngridientParent = null;
        public Ingridient_Script Ingridient_Sample = null;

        public IngridientManager_ProgressEvent OnProgress = new IngridientManager_ProgressEvent();

        private List<Ingridient_Class> fIngridients = new List<Ingridient_Class>();
        private List<Ingridient_Script> fIngridientsUI = new List<Ingridient_Script>();
    
        private string INI_INGRIDIENTS_INFO_SECTION = "INGRIDIENTS_INFO";
        private string INI_INGRIDIENTS_COUNT        = "Count";

    public void SetIngridientsUICount(int count)
    {
        ClearUI();
        Ingridient_Script ingr;
        int sibling_ind = Ingridient_Sample.Body.GetSiblingIndex();

        for (int i=0; i<count; i++)
        {
            sibling_ind++;
            ingr = Ingridient_Script.Instantiate(Ingridient_Sample, IngridientParent);
            ingr.Body.SetSiblingIndex(sibling_ind);
            fIngridientsUI.Add(ingr);
        }
    }

    public void ShowIngridientsUIList(List<CoctailIngridient_Class> list)
    {
        int count = list.Count;

        if (list.Count > fIngridientsUI.Count)
            count = fIngridientsUI.Count;

        for (int i = 0; i < fIngridientsUI.Count; i++)
        {
            fIngridientsUI[i].Unit.SetActive(false);
        }

        int e = 0;

        for (int i=0; i<count; i++)
        {
            Ingridient_Class cls = IngridientByName(list[i].IngridientName);

            if (cls != null)
            {
                cls.Volume = list[i].Volume;
                fIngridientsUI[i].Unit.SetActive(true);
                fIngridientsUI[e].SetData = cls;
                e++;
            }
        }
    }

    public bool AddIngridient(Ingridient_Class s)
    {
        if (CheckIngridientName(s.Name)) return false;

        fIngridients.Add(s);
        return true;
    }

    public void RemoveIngridient(Ingridient_Class ingridient)
    {
        RemoveIngridient(ingridient);
    }

    public int IngridientIndexByName(string name)
    {
        for (int i=0;i<fIngridients.Count; i++)
        {
            if (fIngridients[i].Name.ToUpper() == name.ToUpper())
            {
                return i;
            }
        }

        return -1;
    }

    public Ingridient_Class IngridientByName(string name)
    {
        int index = IngridientIndexByName(name);

        if (CheckIndex(index))
        {
            return fIngridients[index];
        }

        return null;
    }

    public bool CheckIngridientName(string name)
    {
        return IngridientIndexByName(name) != -1;
    }

    public int IngridientsCount
    {
        get
        {
            return fIngridients.Count;
        }
    }

    public void Clear()
    {
        fIngridients.Clear();
    }

    public void ClearUI()
    {
        for (int i = 0; i<fIngridientsUI.Count; i++)
        {
            Ingridient_Script.Destroy(fIngridientsUI[i].Unit);
        }

        fIngridientsUI.Clear();
    }

    public int MatchIngridientsByString(string s, bool TrimSpaces, out string [] matches)
    {
        List<string> match_list = new List<string>();
        string match_string = s.ToUpper();

        if (TrimSpaces)
            match_string = s.Trim();

        for (int i=0; i<fIngridients.Count; i++)
        {
            string name = fIngridients[i].Name.ToUpper();

            if (name.StartsWith(match_string))
            {
                match_list.Add(name);
            }
        }


        matches = match_list.ToArray();
        return match_list.Count;
    }

    public void LoadData(INIParser data)
    {
        Clear();
        int count = data.ReadValue(INI_INGRIDIENTS_INFO_SECTION, INI_INGRIDIENTS_COUNT, 0);
        OnProgress.Invoke(0);

        for (int i = 0; i<count; i++)
        {
            Ingridient_Class cls = new Ingridient_Class();
            cls.Zero();
            cls.LoadData(data, i + 1);
            fIngridients.Add(cls);
            OnProgress.Invoke((float)(i + 1) / count);
        }

        OnProgress.Invoke(1);
    }

    public void SaveData(INIParser data)
    {
        data.WriteValue(INI_INGRIDIENTS_INFO_SECTION, INI_INGRIDIENTS_COUNT, fIngridients.Count);

        OnProgress.Invoke(0);

        for (int i = 0; i<fIngridients.Count; i++)
        {
            fIngridients[i].SaveData(data, i + 1);
            OnProgress.Invoke((float)(i + 1) / fIngridients.Count);
        }

        OnProgress.Invoke(1);
    }

    bool CheckIndex(int index)
    {
        return (index >= 0) && (index < fIngridients.Count);
    }

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
