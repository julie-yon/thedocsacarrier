using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utility;
using Utility.Naming;

namespace Docsa
{
    public class DocsaSoundManager : SoundManager<DocsaSoundNaming>, ISerializationCallbackReceiver
    {
        [Serializable]
        public class DocsaSoundNamingAndAudioClip 
        {
            public List<DocsaSoundNaming> DocsaSoundNaming = new List<DocsaSoundNaming>(); 
            public List<AudioClip> AudioClip = new List<AudioClip>();
        }    
        [Serializable]
        public class DocsaSoundNamingAndAudioSource 
        {
            public List<DocsaSoundNaming> DocsaSoundNaming = new List<DocsaSoundNaming>(); 
            public List<AudioSource> AudioSource = new List<AudioSource>();
        }
        [Serializable]
        protected class SerializablePathAndSceneTuple : PathAndSceneTuple
        {

        }

        [SerializeField] private DocsaSoundNamingAndAudioClip SharedAudioClipDict = new DocsaSoundNamingAndAudioClip();
        [SerializeField] private DocsaSoundNamingAndAudioClip CurrentAudioClipDict = new DocsaSoundNamingAndAudioClip();
        [SerializeField] private DocsaSoundNamingAndAudioSource PlayingAudioSourceDict = new DocsaSoundNamingAndAudioSource();
        [SerializeField] private List<SerializablePathAndSceneTuple> ResourcePathsForEachScene = new List<SerializablePathAndSceneTuple>();

        public void OnBeforeSerialize()
        {
            SharedAudioClipDict.DocsaSoundNaming.Clear();
            SharedAudioClipDict.AudioClip.Clear();
            CurrentAudioClipDict.DocsaSoundNaming.Clear();
            CurrentAudioClipDict.AudioClip.Clear();
            PlayingAudioSourceDict.DocsaSoundNaming.Clear();
            PlayingAudioSourceDict.AudioSource.Clear();
            ResourcePathsForEachScene.Clear();

            foreach (KeyValuePair<DocsaSoundNaming, AudioClip> pair in sharedAudioClipDict)
            {
                SharedAudioClipDict.DocsaSoundNaming.Add(pair.Key);
                SharedAudioClipDict.AudioClip.Add(pair.Value);
            }
            foreach (KeyValuePair<DocsaSoundNaming, AudioClip> pair in currentAudioClipDict)
            {
                CurrentAudioClipDict.DocsaSoundNaming.Add(pair.Key);
                CurrentAudioClipDict.AudioClip.Add(pair.Value);
            }
            foreach (KeyValuePair<DocsaSoundNaming, AudioSource> pair in playingAudioSourceDict)
            {
                PlayingAudioSourceDict.DocsaSoundNaming.Add(pair.Key);
                PlayingAudioSourceDict.AudioSource.Add(pair.Value);
            }
            foreach (SerializablePathAndSceneTuple tuple in resourcePathsForEachScene)
            {
                ResourcePathsForEachScene.Add(tuple);
            }
        }

        public void OnAfterDeserialize()
        {
            sharedAudioClipDict.Clear();
            currentAudioClipDict.Clear();
            playingAudioSourceDict.Clear();
            resourcePathsForEachScene.Clear();

            if (SharedAudioClipDict.DocsaSoundNaming.Count != SharedAudioClipDict.AudioClip.Count)
                throw new Exception(string.Format("There are {0} keys and {1} values after deserialization, Make sure that both key and value types are serializable."));

            for (int i = 0; i < SharedAudioClipDict.DocsaSoundNaming.Count; i++)
            {
                sharedAudioClipDict.Add(SharedAudioClipDict.DocsaSoundNaming[i], SharedAudioClipDict.AudioClip[i]);
            }
            for (int i = 0; i < CurrentAudioClipDict.DocsaSoundNaming.Count; i++)
            {
                currentAudioClipDict.Add(CurrentAudioClipDict.DocsaSoundNaming[i], CurrentAudioClipDict.AudioClip[i]);
            }
            for (int i = 0; i < PlayingAudioSourceDict.DocsaSoundNaming.Count; i++)
            {
                playingAudioSourceDict.Add(PlayingAudioSourceDict.DocsaSoundNaming[i], PlayingAudioSourceDict.AudioSource[i]);
            }
            for (int i = 0; i < ResourcePathsForEachScene.Count; i++)
            {
                resourcePathsForEachScene.Add(ResourcePathsForEachScene[i]);
            }
        }
    }
}