using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CoctailIngridient_Class
{
    public string IngridientName = "";
    public int Volume = 0;

    public CoctailIngridient_Class(string name, int volume)
    {
        IngridientName = name;
        Volume = volume;
    }
}

public class Coctail_Class
{
        public string Name = "";
        public string Image_SpriteName = "";
        public string Tare_Name = "";
        public string Method_Name = "";

        public int Total_Volume = 0;
        public bool Total_Volume_Auto = false;
        public int Cost = 0;

        public string Recipe = "";
        public string Coctail_History = "";

        public List<CoctailIngridient_Class> IngridientNames = new List<CoctailIngridient_Class>();
    
        private string INI_COCTAIL_SECTION_PREFIX_NAME = "COCTAIL_";
        private string INI_COCTAIL_NAME_VALUE = "Name";
        private string INI_COCTAIL_IMAGE_SPRITE_NAME_VALUE = "ImageName";
        private string INI_COCTAIL_TARE_NAME_VALUE = "TareName";
        private string INI_COCTAIL_METHOD_NAME_VALUE = "MethodName";
        private string INI_COCTAIL_TOTAL_VOLUME_VALUE = "TotalVolume";
        private string INI_COCTAIL_TOTAL_VOLUME_AUTO_VALUE = "TotalVolumeAuto";
        private string INI_COCTAIL_COST_VALUE = "Cost";
        private string INI_COCTAIL_RECIPE_VALUE = "Recipe";
        private string INI_COCTAIL_COCTAIL_HISTORY_VALUE = "CoctailHistory";
        private string INI_COCTAIL_INGRIDIENTS_COUNT_VALUE = "IngridientsCount";
        private string INI_COCTAIL_INGRIDIENT_MASK_NAME_VALUE = "Ingridient_{0:d}_Name";
        private string INI_COCTAIL_INGRIDIENT_MASK_VOLUME_VALUE = "Ingridient_{0:d}_Volume";


    public void DisplayDebug()
    {
        Debug.Log("---------------");
        Debug.Log("Coctail_Class sample:");
        Debug.Log("Name: " + Name);
        Debug.Log("Image_SpriteName: " + Image_SpriteName);
        Debug.Log("Tare_Name: " + Tare_Name);
        Debug.Log("Method_Name: " + Method_Name);
        Debug.Log("Total_Volume: " + Total_Volume);
        Debug.Log("Cost: " + Cost);
        Debug.Log("Recipe: " + Recipe);
        Debug.Log("Coctail_History: " + Coctail_History);
        Debug.Log("IngridientCount: " + IngridientNames.Count);

        for (int i=0; i<IngridientNames.Count; i++)
        {
            Debug.Log("Ingridient #" + (i+1) + ": " + IngridientNames[i].IngridientName);
        }

        Debug.Log("---------------");
    }

    public Coctail_Class()
    {
        Zero();
    }

    public void Assign(Coctail_Class data)
    {
        if (data == null)
        {
            Zero();
            return;
        }

        Name = data.Name;
        Image_SpriteName = data.Image_SpriteName;
        Tare_Name = data.Tare_Name;
        Method_Name = data.Method_Name;
        Total_Volume = data.Total_Volume;
        Total_Volume_Auto = data.Total_Volume_Auto;
        Cost = data.Cost;
        Recipe = data.Recipe;
        Coctail_History = data.Coctail_History;
        IngridientNames.Clear();
        IngridientNames.AddRange(data.IngridientNames);
    }

    public void Zero()
    {
        Name = "";
        Image_SpriteName = "";
        Tare_Name = "";
        Method_Name = "";
        Total_Volume = 0;
        Cost = 0;
        Recipe = "";
        Coctail_History = "";
        IngridientNames.Clear();
    }

    public void LoadData(INIParser data, int key)
    {
        Zero();

        Name = data.ReadValue(INI_COCTAIL_SECTION_PREFIX_NAME + key, INI_COCTAIL_NAME_VALUE, "");
        Image_SpriteName = data.ReadValue(INI_COCTAIL_SECTION_PREFIX_NAME + key, INI_COCTAIL_IMAGE_SPRITE_NAME_VALUE, "");
        Tare_Name = data.ReadValue(INI_COCTAIL_SECTION_PREFIX_NAME + key, INI_COCTAIL_TARE_NAME_VALUE, "");
        Method_Name = data.ReadValue(INI_COCTAIL_SECTION_PREFIX_NAME + key, INI_COCTAIL_METHOD_NAME_VALUE, "");
        Total_Volume = data.ReadValue(INI_COCTAIL_SECTION_PREFIX_NAME + key, INI_COCTAIL_TOTAL_VOLUME_VALUE, 0);
        Total_Volume_Auto = data.ReadValue(INI_COCTAIL_SECTION_PREFIX_NAME + key, INI_COCTAIL_TOTAL_VOLUME_AUTO_VALUE, false);
        Cost = data.ReadValue(INI_COCTAIL_SECTION_PREFIX_NAME + key, INI_COCTAIL_COST_VALUE, 0);
        Coctail_Script.ReadStringListFromIni(data, INI_COCTAIL_SECTION_PREFIX_NAME + key, INI_COCTAIL_RECIPE_VALUE, out Recipe, true);
        Coctail_Script.ReadStringListFromIni(data, INI_COCTAIL_SECTION_PREFIX_NAME + key, INI_COCTAIL_COCTAIL_HISTORY_VALUE, out Coctail_History, false);
        int count = data.ReadValue(INI_COCTAIL_SECTION_PREFIX_NAME + key, INI_COCTAIL_INGRIDIENTS_COUNT_VALUE, 0);

        for (int i = 0; i < count; i++)
        {
            string s = data.ReadValue(INI_COCTAIL_SECTION_PREFIX_NAME + key, string.Format(INI_COCTAIL_INGRIDIENT_MASK_NAME_VALUE, i + 1), "");
            int v = data.ReadValue(INI_COCTAIL_SECTION_PREFIX_NAME + key, string.Format(INI_COCTAIL_INGRIDIENT_MASK_VOLUME_VALUE, i + 1), 0);
            CoctailIngridient_Class cls = new CoctailIngridient_Class(s, v);
            IngridientNames.Add(cls);
        }
    }

    public void SaveData(INIParser data, int key)
    {
        data.WriteValue(INI_COCTAIL_SECTION_PREFIX_NAME + key, INI_COCTAIL_NAME_VALUE, Name);
        data.WriteValue(INI_COCTAIL_SECTION_PREFIX_NAME + key, INI_COCTAIL_IMAGE_SPRITE_NAME_VALUE, Image_SpriteName);
        data.WriteValue(INI_COCTAIL_SECTION_PREFIX_NAME + key, INI_COCTAIL_TARE_NAME_VALUE, Tare_Name);
        data.WriteValue(INI_COCTAIL_SECTION_PREFIX_NAME + key, INI_COCTAIL_METHOD_NAME_VALUE, Method_Name);
        data.WriteValue(INI_COCTAIL_SECTION_PREFIX_NAME + key, INI_COCTAIL_TOTAL_VOLUME_VALUE, Total_Volume);
        data.WriteValue(INI_COCTAIL_SECTION_PREFIX_NAME + key, INI_COCTAIL_TOTAL_VOLUME_AUTO_VALUE, Total_Volume_Auto);
        data.WriteValue(INI_COCTAIL_SECTION_PREFIX_NAME + key, INI_COCTAIL_COST_VALUE, Cost);
        Coctail_Script.WriteStringListToIni(data, INI_COCTAIL_SECTION_PREFIX_NAME + key, INI_COCTAIL_RECIPE_VALUE, Recipe);
        Coctail_Script.WriteStringListToIni(data, INI_COCTAIL_SECTION_PREFIX_NAME + key, INI_COCTAIL_COCTAIL_HISTORY_VALUE, Coctail_History);
        data.WriteValue(INI_COCTAIL_SECTION_PREFIX_NAME + key, INI_COCTAIL_INGRIDIENTS_COUNT_VALUE, IngridientNames.Count);

        for (int i = 0; i < IngridientNames.Count; i++)
        {
            data.WriteValue(INI_COCTAIL_SECTION_PREFIX_NAME + key, string.Format(INI_COCTAIL_INGRIDIENT_MASK_NAME_VALUE, i + 1), IngridientNames[i].IngridientName);
            data.WriteValue(INI_COCTAIL_SECTION_PREFIX_NAME + key, string.Format(INI_COCTAIL_INGRIDIENT_MASK_VOLUME_VALUE, i + 1), IngridientNames[i].Volume);
        }
    }
}

public class Coctail_Script : MonoBehaviour {

        public string Sources_Path = "\\Resources\\";

        public string Recipe_Line_Delimiter = "● ";

        public CoctailManager_Script Manager = null;
        public IngridientManager_Script IngridientManager = null;

        public RectTransform Body = null;
        public GameObject Unit = null;

        public RectTransform ContentBody = null;

        public Text UI_Name = null;
        public Image UI_Image = null;
        public Text UI_TareName = null;
        public Text UI_MethodName = null;
        public Text UI_TotalVolume = null;
        public Text UI_Cost = null;
        public VerticalLayoutGroup UI_Recipe_VLG = null;
        public GameObject UI_Recipe_Unit = null;
        public GameObject UI_Recipe_Header_Unit = null;
        public Text UI_Recipe = null;
        public Text UI_CoctailHistory = null;

        private Coctail_Class fData = new Coctail_Class();
    

    public Coctail_Class SetData
    {
        get
        {
            return fData;
        }

        set
        {
            fData.Assign(value);
        }
    }
    
    IEnumerator Num()
    {
        yield return new WaitForEndOfFrame();
        LayoutRebuilder.ForceRebuildLayoutImmediate(ContentBody);
        LayoutRebuilder.ForceRebuildLayoutImmediate(Body);
    }

    public void UpdateUI(bool fromUI)
    {
        if (fromUI) return;

        fData.DisplayDebug();

        UI_Name.text = fData.Name;

        UI_Image.sprite = Resources.Load<Sprite>(fData.Image_SpriteName);
        UI_TareName.text = fData.Tare_Name;
        UI_MethodName.text = fData.Method_Name;
        UI_TotalVolume.text = fData.Total_Volume.ToString();
        UI_Cost.text = fData.Cost.ToString();

        string buf = fData.Recipe;
        string[] lines = buf.Split(new char[1] { '\n' } );
        string push = "";
        
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i] != "")
                push += Recipe_Line_Delimiter + lines[i] + "\n";
        }
        
        UI_Recipe.text = push;
        UI_Recipe_Unit.transform.SetAsLastSibling();
        UI_Recipe_VLG.CalculateLayoutInputVertical();

        buf = fData.Coctail_History;
        lines = buf.Split(new char[1] { '\n' });
        push = "";

        for (int i=0; i<lines.Length; i++)
        {
            if (lines[i] != "")
            {
                push += '\t' + lines[i] + '\n';
            }
        }

        UI_CoctailHistory.text = push;
        IngridientManager.SetIngridientsUICount(fData.IngridientNames.Count);
        IngridientManager.ShowIngridientsUIList(fData.IngridientNames);
        StartCoroutine(Num());
    }

    public void Zero()
    {
        Coctail_Class cls = new Coctail_Class();
        cls.Zero();
        SetData = cls;
        UpdateUI(false);
    }

    public static void ReadStringListFromIni(INIParser AData, string ASection, string AKey_Prefix, out string AStringList, bool AIgnoreEmptyStrings)
    {
        int c = AData.ReadValue(ASection, AKey_Prefix + "COUNT", 0);
        AStringList = "";

        for (int i = 0; i < c; i++)
        {
            string line = AData.ReadValue(ASection, AKey_Prefix + (i + 1), "");

            if (!AIgnoreEmptyStrings || (line != ""))
                AStringList += line + '\n';
        }        
    }

    public static void WriteStringListToIni(INIParser AData, string ASection, string AKey_Prefix, string AStringList)
    {
        string[] aline = AStringList.Split(new char[1] { '\n' });
        AData.WriteValue(ASection, AKey_Prefix + "COUNT", aline.Length);
        
        for (int i=0; i<aline.Length; i++)
        {
            AData.WriteValue(ASection, AKey_Prefix + (i + 1), aline[i]);
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
