using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ingridient_Class
{
        public string Name;
        public int Volume;

        public string Hover_Image_SpriteName;
        public string Hover_Description;

    private string INI_INGRIDIENT_SECTION_PREFIX_NAME = "INGRIDIENT_";
    private string INI_INGRIDIENT_NAME_VALUE = "Name";
    private string INI_INGRIDIENT_VOLUME_VALUE = "Volume";
    private string INI_INGRIDIENT_HOVER_IMAGE_SPRITE_NAME_VALUE = "HoverImageName";
    private string INI_INGRIDIENT_HOVER_DESCRIPTION_VALUE = "HoverDescription";

    public Ingridient_Class()
    {
        Zero();
    }

    public void Zero()
    {
        Name = "";
        Volume = 0;
        Hover_Image_SpriteName = "";
        Hover_Description = "";
    }

    public void LoadData(INIParser data, int key)
    {
        Name = data.ReadValue(INI_INGRIDIENT_SECTION_PREFIX_NAME + key, INI_INGRIDIENT_NAME_VALUE, "");
        Volume = data.ReadValue(INI_INGRIDIENT_SECTION_PREFIX_NAME + key, INI_INGRIDIENT_VOLUME_VALUE, 0);
        Hover_Image_SpriteName = data.ReadValue(INI_INGRIDIENT_SECTION_PREFIX_NAME + key, INI_INGRIDIENT_HOVER_IMAGE_SPRITE_NAME_VALUE, "");
        Coctail_Script.ReadStringListFromIni(data, INI_INGRIDIENT_SECTION_PREFIX_NAME + key, INI_INGRIDIENT_HOVER_DESCRIPTION_VALUE, out Hover_Description, false);
    }

    public void SaveData(INIParser data, int key)
    {
        data.WriteValue(INI_INGRIDIENT_SECTION_PREFIX_NAME + key, INI_INGRIDIENT_NAME_VALUE, Name);
        data.WriteValue(INI_INGRIDIENT_SECTION_PREFIX_NAME + key, INI_INGRIDIENT_VOLUME_VALUE, Volume);
        data.WriteValue(INI_INGRIDIENT_SECTION_PREFIX_NAME + key, INI_INGRIDIENT_HOVER_IMAGE_SPRITE_NAME_VALUE, Hover_Image_SpriteName);
        Coctail_Script.WriteStringListToIni(data, INI_INGRIDIENT_SECTION_PREFIX_NAME + key, INI_INGRIDIENT_HOVER_DESCRIPTION_VALUE, Hover_Description);
    }

}

public class Ingridient_Script : MonoBehaviour {

        public IngridientManager_Script Manager = null;

        public RectTransform Body = null;
        public GameObject Unit = null;

        public Text Name = null;
        public Text Volume = null;

        public string Hover_Image_SpriteName = "none";
        public string Hover_Description = "";


    public Ingridient_Class SetData
    {
        set
        {
            _Name = value.Name;
            _Volume = value.Volume;
            Hover_Image_SpriteName = value.Hover_Image_SpriteName;
            Hover_Description = value.Hover_Description;
        }
    }

    public string _Name
    {
        get
        {
            return Name.text;
        }

        set
        {
            Name.text = value;
        }
    }

    public int _Volume
    {
        get
        {
            return int.Parse(Volume.text);
        }

        set
        {
            Volume.text = "" + value;
        }
    }



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
