using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProgressStage
{
    public string Name = "";
    public float Progress = 0f;
    public bool Finished = false;

    public ProgressStage(string name)
    {
        Name = name;
        Progress = 0f;
        Finished = false;
    }
}

public class ProgressUI_Script : MonoBehaviour {

        public GameObject Unit = null;
        public ProgressBar ProgressBar = null;

        public float test = 0f;

        public UnityEvent OnFullProgress = new UnityEvent();

    private int fProgrCount = 0;
        private List<ProgressStage> fProgressStages = new List<ProgressStage>();
        private int fStagesFinishedCount = 0;


    public void AddProgressStage(string stage_name)
    {
        ProgressStage cls = new ProgressStage(stage_name);
        fProgressStages.Add(cls);
    }

    public void RemoveProgressStage(string stage_name)
    {
        int index = StageIndexByName(stage_name);

        if (index != -1)
            fProgressStages.RemoveAt(index);
    }

    public int StageIndexByName(string stage_name)
    {
        for (int i = 0; i < fProgressStages.Count; i++)
        {
            if (fProgressStages[i].Name == stage_name)
                return i;
        }

        return -1;
    }

    public ProgressStage StageByName(string stage_name)
    {
        int index = StageIndexByName(stage_name);

        if (index != -1)
            return fProgressStages[index];

        return null;
    }

    public void SetProgress(string stage_name, float progress)
    {
        ProgressStage cls = StageByName(stage_name);

        if (cls == null) return;

        cls.Progress = progress;

        if (progress == 1f)
        {
            cls.Finished = true;
            fStagesFinishedCount++;

            if (fStagesFinishedCount == fProgressStages.Count)
                OnFullProgress.Invoke();
        }
        
        if (fProgressStages.Count > 0)
        {
            float cd = 1f / fProgressStages.Count;
            ProgressBar.BarValue = (fStagesFinishedCount / fProgressStages.Count + progress * cd) * 100f;
        }
        else
        {
            ProgressBar.BarValue = 0;
        }
    }

    public float GetProgress(string stage_name)
    {
        ProgressStage cls = StageByName(stage_name);

        if (cls != null)
            return cls.Progress;

        return -1f;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
}
