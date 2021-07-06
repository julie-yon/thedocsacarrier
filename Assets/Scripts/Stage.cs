using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public static List<GameObject> SStageList = new List<GameObject>();
    public static Stage SCurrentStage;
    public List<Chunk> ChunkList = new List<Chunk>();
    public float CameraMoveSpeed = 1;
    public Vector3 StartCameraPosition;
    [SerializeField] GameObject RightChunkTrigger;
    [SerializeField] GameObject LeftChunkTrigger;
    private int _maxChunkNum;
    private int _currentChunkNum = 1;
    private int _targetChunkNum;
    private int _previousChunkNum;
    public Chunk CurrentChunk
    {
        get{return ChunkList[_currentChunkNum-1];}
    }
    
    public Chunk NextChunk
    {
        get{return _currentChunkNum < ChunkList.Count ? ChunkList[_currentChunkNum] : null;}
    }
    
    public Chunk PreviousChunk
    {
        get{return _currentChunkNum > 1 ? ChunkList[_currentChunkNum-2] : null;}
    }

    void Awake()
    {
        _maxChunkNum = ChunkList.Count;

        Chunk[] chunks = GetComponentsInChildren<Chunk>();
        foreach(Chunk chunk in chunks)
        {
            ChunkList.Add(chunk);
        }

        if (ChunkList.Count > 0)
        {
            RightChunkTrigger = ChunkList[0].RightChunkTriggerObject;
            LeftChunkTrigger = ChunkList[0].LeftChunkTriggerObject;
        }
    }

    public void ChunkTriggerEnter(Collider collider)
    {
        if (collider.gameObject == RightChunkTrigger)
        {
            GotoNextChunk();
        } else
        {
            GotoPreviousChunk();
        }
    }
    
    public bool GotoNextChunk()
    {
        if (NextChunk == null)
        {
            return false;
        }
        _targetChunkNum = _currentChunkNum+1;

        MoveCameraToTargetChunk();

        return true;
    }

    public bool GotoPreviousChunk()
    {
        if (PreviousChunk == null)
        {
            return false;
        }
        _targetChunkNum = _currentChunkNum-1;

        MoveCameraToTargetChunk();

        return true;
    }

    private void StopFieldObjects()
    {
        Time.timeScale = 0;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    private void UnStopFieldObjects()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    private void MoveCameraToTargetChunk()
    {
        StartCoroutine(CameraMove());
    }

    IEnumerator CameraMove()
    {
        ChunkList[_targetChunkNum].gameObject.SetActive(true);
        StopFieldObjects();

        bool moveFinished = false;

        while(!moveFinished)
        {
            yield return new WaitForSecondsRealtime(0.02f);

            Camera.main.transform.Translate(_targetChunkNum > _currentChunkNum ? Vector3.right : Vector3.left);
        }

        _currentChunkNum++;
        RightChunkTrigger = CurrentChunk.RightChunkTriggerObject;
        LeftChunkTrigger = CurrentChunk.LeftChunkTriggerObject;
        UnStopFieldObjects();
        ChunkList[_previousChunkNum].gameObject.SetActive(false);
    }

    public void Dispose()
    {

    }

    public static Stage MakeStage(int stageNum)
    {
        GameObject obj = Instantiate(SStageList[stageNum-1]);

        Camera.main.transform.position = obj.GetComponent<Stage>().ChunkList[0].DefaultCameraPosition;

        return obj.GetComponent<Stage>();
    }
}
