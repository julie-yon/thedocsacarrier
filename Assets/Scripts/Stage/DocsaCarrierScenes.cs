using System.Collections.Generic;

namespace Docsa
{
    public static class DocsaCarrierScenes
    {
        public const string StartSceneName = "StartScene"; 
        public const string CaveSceneName = "Cave"; 
        public const string Stage1SceneName = "Stage1"; 
        public const string Stage2SceneName = "Stage2"; 
        public const string Stage3SceneName = "Stage3"; 
        public const string Stage4SceneName = "Stage4"; 

        public static List<string> SceneNameList = new List<string>(){StartSceneName, CaveSceneName, Stage1SceneName, Stage2SceneName, Stage3SceneName, Stage4SceneName};
    }
}