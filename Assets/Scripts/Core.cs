using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using dkstlzu.Utility;

namespace Docsa
{
    public class Core : Singleton<Core>
    {
        public string UzuhamaTwitchNickName = "우주하마";

        public SceneLoadCallbackSetter SCB;

        void Awake()
        {
            SCB = new SceneLoadCallbackSetter(DocsaCarrierScenes.SceneNameList);
        }

        public void GotoCave()
        {
            StageManager.instance.GotoStage(StageName.Cave);
        }

        public void GameStart()
        {
            StageManager.instance.GotoStage(StageName.Cave);
        }

        public void StageStart()
        {
            StageManager.instance.GotoStage(StageName.Stage1);
        }

        public void GameClear()
        {

        }
    }
}