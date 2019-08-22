using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController_Script : MonoBehaviour {


    public CoctailManager_Script CoctailManager = null;
    public IngridientManager_Script IngridientManager = null;
    public FindPanel_Script FindPanel = null;
    public ProgressUI_Script ProgressUI = null;

    public INIParser Data_Parser = null;
    public INIParser Config_Parser = null;


    public bool RememberCoctailName = true;
    public string LastCoctailName = "";
    public string CoctailsDBName = "";
    public string IngridientsDBName = "";

    private string CONFIG_FILE_NAME = "config.ini";
    private string INI_CONFIG_SECTION_NAME = "CONFIGURATION";
    private string INI_CONFIG_REMEMBER_COCTAIL_NAME_VALUE = "RememberCoctailName";
    private string INI_CONFIG_LAST_COCTAIL_NAME_VALUE = "LastCoctailName";
    private string INI_CONFIG_COCTAILS_DB_NAME_VALUE = "CoctailsDBName";
    private string INI_CONFIG_INGRIDIENTS_DB_NAME_VALUE = "IngridientsDBName";

    private bool DEFAULT_REMEMBER_COCTAIL_NAME = true;
    private string DEFAULT_LAST_COCTAIL_NAME = "";
    private string DEFAULT_COCTAILS_DB_NAME = "coctails.db";
    private string DEFAULT_INGRIDIENTS_DB_NAME = "ingridients.db";

    private string PROGRESS_STAGE_INGRIDIENTS_DB = "ingridients";
    private string PROGRESS_STAGE_COCTAILS_DB = "coctails";


    public void InitializeComponents()
    {
        Data_Parser = new INIParser();
        Config_Parser = new INIParser();
        LoadConfig();

        ShowProgressUI(true, false);
        ProgressUI.AddProgressStage(PROGRESS_STAGE_INGRIDIENTS_DB);
        ProgressUI.AddProgressStage(PROGRESS_STAGE_COCTAILS_DB);
        ProgressUI.OnFullProgress.AddListener(OnProgressUIFullProgress);

        ShowCoctailUI(false, false);
        ShowFindPanel(false, false);
        ShowProgressUI(true, false);

        FindPanel.OnSubmit.AddListener(OnFindPanelSubmit);
        CoctailManager.OnProgress.AddListener(OnLoadCoctailsDBProgress);
        IngridientManager.OnProgress.AddListener(OnLoadIngridientsDBProgress);
        Data_Parser.Open(IngridientsDBName);
        IngridientManager.LoadData(Data_Parser);
        Data_Parser.Close();
        Data_Parser.Open(CoctailsDBName);
        CoctailManager.LoadData(Data_Parser);
        Data_Parser.Close();

        Debug.Log("Ingridients count: " + IngridientManager.IngridientsCount);
        Debug.Log("Coctails count: " + CoctailManager.CoctailsCount);
    }

    public void PrepareComponents()
    {
    }

    public void ShowProgressUI(bool value, bool fromShow)
    {
        if (!fromShow)
        { 
            ShowFindPanel(false, true);
            ShowCoctailUI(false, true);
        }   

        ProgressUI.Unit.SetActive(value);
    }

    public void ShowFindPanel(bool value, bool fromShow)
    {
        if (!fromShow)
        {
            ShowProgressUI(false, true);
            ShowCoctailUI(false, true);
        } 

        FindPanel.Unit.SetActive(value);
    }

    public void ShowFindPanel()
    {
        Debug.Log("Show find panel");
        ShowFindPanel(true, false);
    }

    public void ShowCoctailUI(bool value, bool fromShow)
    {
        if (!fromShow)
        {
            ShowProgressUI(false, true);
            ShowFindPanel(false, true);
        }

        CoctailManager.UIUnit.SetActive(value);
    }


    public void OnLoadIngridientsDBProgress(float progress)
    {
        ProgressUI.SetProgress(PROGRESS_STAGE_INGRIDIENTS_DB, progress);
    }

    public void OnLoadCoctailsDBProgress(float progress)
    {
        Debug.Log("OnLoadCoctailProgress: " + progress);
        ProgressUI.SetProgress(PROGRESS_STAGE_COCTAILS_DB, progress);
    }

    public void OnProgressUIFullProgress()
    {
        ShowProgressUI(false, false);

        if (CheckSavedSession())
        {
            ShowCoctailUI(true, false);
            CoctailManager.ShowCoctail(LastCoctailName);
        }
        else
        {
            ShowFindPanel(true, false);
        }
    }

    public void OnFindPanelSubmit(string value)
    {
        FindPanel.FindText = value;
        ShowFindPanel(false, false);
        ShowCoctailUI(true, false);
        CoctailManager.ShowCoctail(value);
    }
    
    public bool CheckSavedSession()
    {
        return (RememberCoctailName) && (CoctailManager.CheckCoctailName(LastCoctailName));
    }

    public void LoadConfig()
    {
        Config_Parser.Open(CONFIG_FILE_NAME);
        RememberCoctailName = Config_Parser.ReadValue(INI_CONFIG_SECTION_NAME, INI_CONFIG_REMEMBER_COCTAIL_NAME_VALUE, DEFAULT_REMEMBER_COCTAIL_NAME);
        LastCoctailName = Config_Parser.ReadValue(INI_CONFIG_SECTION_NAME, INI_CONFIG_LAST_COCTAIL_NAME_VALUE, DEFAULT_LAST_COCTAIL_NAME);
        CoctailsDBName = Config_Parser.ReadValue(INI_CONFIG_SECTION_NAME, INI_CONFIG_COCTAILS_DB_NAME_VALUE, DEFAULT_COCTAILS_DB_NAME);
        IngridientsDBName = Config_Parser.ReadValue(INI_CONFIG_SECTION_NAME, INI_CONFIG_INGRIDIENTS_DB_NAME_VALUE, DEFAULT_INGRIDIENTS_DB_NAME);
        Config_Parser.Close();
    }

    public void SaveConfig()
    {
        Config_Parser.Open(CONFIG_FILE_NAME);
        Config_Parser.WriteValue(INI_CONFIG_SECTION_NAME, INI_CONFIG_REMEMBER_COCTAIL_NAME_VALUE, RememberCoctailName);
        Config_Parser.WriteValue(INI_CONFIG_SECTION_NAME, INI_CONFIG_LAST_COCTAIL_NAME_VALUE, LastCoctailName);
        Config_Parser.WriteValue(INI_CONFIG_SECTION_NAME, INI_CONFIG_COCTAILS_DB_NAME_VALUE, CoctailsDBName);
        Config_Parser.WriteValue(INI_CONFIG_SECTION_NAME, INI_CONFIG_INGRIDIENTS_DB_NAME_VALUE, IngridientsDBName);
        Config_Parser.Close();
    }

    public void FillTestDB()
    {
        /*
        for (int i = 0; i<CoctailManager.CoctailsCount; i++)
        {
            CoctailManager.Coctail(i).DisplayDebug();
        }
        */

        IngridientManager.Clear();
        CoctailManager.Clear();

        Ingridient_Class icls = new Ingridient_Class();

        icls.Name = "Водка";
        icls.Volume = 15;

        IngridientManager.AddIngridient(icls);

        icls = new Ingridient_Class();

        icls.Name = "Джин";
        icls.Volume = 15;

        IngridientManager.AddIngridient(icls);

        icls = new Ingridient_Class();

        icls.Name = "Текила";
        icls.Volume = 15;

        IngridientManager.AddIngridient(icls);

        icls = new Ingridient_Class();

        icls.Name = "Белый ром";
        icls.Volume = 15;

        IngridientManager.AddIngridient(icls);

        icls = new Ingridient_Class();

        icls.Name = "Трипл Сек";
        icls.Volume = 15;

        IngridientManager.AddIngridient(icls);

        icls = new Ingridient_Class();

        icls.Name = "Лимонный сок";
        icls.Volume = 25;

        IngridientManager.AddIngridient(icls);

        icls = new Ingridient_Class();

        icls.Name = "Сахарный сироп";
        icls.Volume = 30;

        IngridientManager.AddIngridient(icls);

        icls = new Ingridient_Class();

        icls.Name = "Кола";
        icls.Volume = 100;

        IngridientManager.AddIngridient(icls);

        Coctail_Class ccls = new Coctail_Class();
                
        ccls.Name = "Лонг айленд айс ти";
        ccls.Image_SpriteName = "LongIlendIceTea";
        ccls.Tare_Name = "Лонг дринк";
        ccls.Method_Name = "Билд";
        ccls.Total_Volume = 230;
        ccls.Cost = 300;
        ccls.Recipe = "Налить все ингредиенты в хайбол, наполненный кубиками льда.|" +
                      "Аккуратно перемешать коктейль барной ложкой.|" +
                      "Подавать с трубочкой";
        ccls.Coctail_History = "Тут типа история коктейля";
        CoctailIngridient_Class cicls = new CoctailIngridient_Class("Водка", 15);
        ccls.IngridientNames.Add(cicls);
        cicls = new CoctailIngridient_Class("Джин", 15);
        ccls.IngridientNames.Add(cicls);
        cicls = new CoctailIngridient_Class("Текила", 15);
        ccls.IngridientNames.Add(cicls);
        cicls = new CoctailIngridient_Class("Белый ром", 15);
        ccls.IngridientNames.Add(cicls);
        cicls = new CoctailIngridient_Class("Трипл сек", 15);
        ccls.IngridientNames.Add(cicls);
        cicls = new CoctailIngridient_Class("Лимонный сок", 25);
        ccls.IngridientNames.Add(cicls);
        cicls = new CoctailIngridient_Class("Сахарный сироп", 30);
        ccls.IngridientNames.Add(cicls);
        cicls = new CoctailIngridient_Class("Кола", 100);
        ccls.IngridientNames.Add(cicls);

        CoctailManager.AddCoctail(ccls);

        Data_Parser.Open(IngridientsDBName);
        IngridientManager.SaveData(Data_Parser);
        Data_Parser.Close();
        Data_Parser.Open(CoctailsDBName);
        CoctailManager.SaveData(Data_Parser);
        Data_Parser.Close();
    }

    // Use this for initialization
    void Start () {
        InitializeComponents();
        PrepareComponents();
	}

    private void OnDestroy()
    {
        SaveConfig();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
