using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Utility;

namespace Docsa
{
    public class Core : Singleton<Core>
    {
        public string UzuhamaTwitchNickName = "우주하마";
        public HamaInput InputAsset;

        public HamaInput AdjustInputAsset()
        {
            if (!DocsaSakkiManager.instance.CorrectlyAssigned)
            {
                InputAsset.Player.Disable();
                InputAsset.UI.Enable();
            } else if (DocsaSakkiManager.instance.CorrectlyAssigned && !ESCUIManager.instance.isOn)
            {
                InputAsset.Player.Enable();
                InputAsset.UI.Disable();
            } else if (DocsaSakkiManager.instance.CorrectlyAssigned && ESCUIManager.instance.isOn)
            {
                InputAsset.Player.Disable();
                InputAsset.UI.Enable();
            }

            return InputAsset;
        }

        public SceneLoadCallbackSetter SCB;

        void Awake()
        {
            SCB = new SceneLoadCallbackSetter(DocsaCarrierScenes.SceneNameList);
            
            InputAsset = new HamaInput();
            InputAsset.Player.Enable();
        }

        public void GotoCave()
        {
            InputAsset.Disable();
            StageManager.instance.GotoStage(StageName.Cave);
            InputAsset.Enable();
        }

        public void GameStart()
        {
            InputAsset.Disable();
            StageManager.instance.GotoStage(StageName.Cave);
            InputAsset.Enable();
        }

        public void StageStart()
        {
            InputAsset.Disable();
            StageManager.instance.GotoStage(StageName.Stage1);
            InputAsset.Enable();
        }

        public void GameClear()
        {

        }
    }
}