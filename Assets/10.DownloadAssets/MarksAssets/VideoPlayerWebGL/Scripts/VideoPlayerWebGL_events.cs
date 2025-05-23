using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;
using System.Reflection;

namespace MarksAssets.VideoPlayerWebGL {
	public partial class VideoPlayerWebGL : MonoBehaviour
	{
#if UNITY_WEBGL && !UNITY_EDITOR
		[DllImport("__Internal", EntryPoint="VideoPlayerWebGL_setUnityFunctions")]
		private static extern void setUnityFunctions(Action<string>canplayClbks, Action<string>canplaythroughClbks, Action<string>completeClbks, Action<string>durationchangeClbks, Action<string>emptiedClbks, Action<string>endedClbks, Action<string>errorClbks, Action<string>loadeddataClbks, Action<string>loadedmetadataClbks, Action<string>pauseClbks, Action<string>playClbks, Action<string>playingClbks, Action<string>progressClbks, Action<string>ratechangeClbks, Action<string>seekedClbks, Action<string>seekingClbks, Action<string>stalledClbks, Action<string>suspendClbks, Action<string>timeupdateClbks, Action<string>volumechangeClbks, Action<string>waitingClbks);
		[DllImport("__Internal", EntryPoint="VideoPlayerWebGL_RegisterEvent")]
		private static extern void registerEvent(string id, string evt);
		[DllImport("__Internal", EntryPoint="VideoPlayerWebGL_UnregisterEvent")]
		private static extern void unregisterEvent(string id, string evt);
#endif

        [Flags]
		public enum evnts {canplay = 1, canplaythrough = 2, complete = 4, durationchange = 8, emptied = 16, ended = 32, error = 64, loadeddata = 128, loadedmetadata = 256, pause = 512, play = 1024, playing = 2048, progress = 4096, ratechange = 8192, seeked = 16384, seeking = 32768, stalled = 65536, suspend = 131072, timeupdate = 262144, volumechange = 524288, waiting = 1048576 };

        [SerializeField]
		private evnts events = evnts.ended;
		[ShowIf("HasCanplayFlag")]
		public UnityEvent canplay;
		[ShowIf("HasCanplaythroughFlag")]
		public UnityEvent canplaythrough;
		[ShowIf("HasCompleteFlag")]
		public UnityEvent complete;
		[ShowIf("HasDurationchangeFlag")]
		public UnityEvent durationchange;
		[ShowIf("HasEmptiedFlag")]
		public UnityEvent emptied;
		[ShowIf("HasEndedFlag")]
		public UnityEvent ended;
        [ShowIf("HasErrorFlag")]
        public UnityEvent error;
        [ShowIf("HasLoadeddataFlag")]
		public UnityEvent loadeddata;
		[ShowIf("HasLoadedmetadataFlag")]
		public UnityEvent loadedmetadata;
		[ShowIf("HasPauseFlag")]
		public UnityEvent pause;
		[ShowIf("HasPlayFlag")]
		public UnityEvent play;
		[ShowIf("HasPlayingFlag")]
		public UnityEvent playing;
		[ShowIf("HasProgressFlag")]
		public UnityEvent progress;
		[ShowIf("HasRatechangeFlag")]
		public UnityEvent ratechange;
		[ShowIf("HasSeekedFlag")]
		public UnityEvent seeked;
		[ShowIf("HasSeekingFlag")]
		public UnityEvent seeking;
		[ShowIf("HasStalledFlag")]
		public UnityEvent stalled;
		[ShowIf("HasSuspendFlag")]
		public UnityEvent suspend;
		[ShowIf("HasTimeupdateFlag")]
		public UnityEvent timeupdate;
		[ShowIf("HasVolumechangeFlag")]
		public UnityEvent volumechange;
		[ShowIf("HasWaitingFlag")]
		public UnityEvent waiting;
		
		private static Dictionary<string, UnityEvent> canplayDict = new Dictionary<string, UnityEvent>();
		private static Dictionary<string, UnityEvent> canplaythroughDict = new Dictionary<string, UnityEvent>();
		private static Dictionary<string, UnityEvent> completeDict = new Dictionary<string, UnityEvent>();
		private static Dictionary<string, UnityEvent> durationchangeDict = new Dictionary<string, UnityEvent>();
		private static Dictionary<string, UnityEvent> emptiedDict = new Dictionary<string, UnityEvent>();
		private static Dictionary<string, UnityEvent> endedDict = new Dictionary<string, UnityEvent>();
        private static Dictionary<string, UnityEvent> errorDict = new Dictionary<string, UnityEvent>();
        private static Dictionary<string, UnityEvent> loadeddataDict = new Dictionary<string, UnityEvent>();
		private static Dictionary<string, UnityEvent> loadedmetadataDict = new Dictionary<string, UnityEvent>();
		private static Dictionary<string, UnityEvent> pauseDict = new Dictionary<string, UnityEvent>();
		private static Dictionary<string, UnityEvent> playDict = new Dictionary<string, UnityEvent>();
		private static Dictionary<string, UnityEvent> playingDict = new Dictionary<string, UnityEvent>();
		private static Dictionary<string, UnityEvent> progressDict = new Dictionary<string, UnityEvent>();
		private static Dictionary<string, UnityEvent> ratechangeDict = new Dictionary<string, UnityEvent>();
		private static Dictionary<string, UnityEvent> seekedDict = new Dictionary<string, UnityEvent>();
		private static Dictionary<string, UnityEvent> seekingDict = new Dictionary<string, UnityEvent>();
		private static Dictionary<string, UnityEvent> stalledDict = new Dictionary<string, UnityEvent>();
		private static Dictionary<string, UnityEvent> suspendDict = new Dictionary<string, UnityEvent>();
		private static Dictionary<string, UnityEvent> timeupdateDict = new Dictionary<string, UnityEvent>();
		private static Dictionary<string, UnityEvent> volumechangeDict = new Dictionary<string, UnityEvent>();
		private static Dictionary<string, UnityEvent> waitingDict = new Dictionary<string, UnityEvent>();
		
		[MonoPInvokeCallback(typeof(Action<string>))]
		private static void canplayClbks(string id) {if (canplayDict.ContainsKey(id)) canplayDict[id].Invoke();}
		[MonoPInvokeCallback(typeof(Action<string>))]
		private static void canplaythroughClbks(string id) { if (canplaythroughDict.ContainsKey(id)) canplaythroughDict[id].Invoke();}
		[MonoPInvokeCallback(typeof(Action<string>))]
		private static void completeClbks(string id) { if (completeDict.ContainsKey(id)) completeDict[id].Invoke(); ; }
		[MonoPInvokeCallback(typeof(Action<string>))]
		private static void durationchangeClbks(string id) { if (durationchangeDict.ContainsKey(id))  durationchangeDict[id].Invoke();}
		[MonoPInvokeCallback(typeof(Action<string>))]
		private static void emptiedClbks(string id) { if (emptiedDict.ContainsKey(id)) emptiedDict[id].Invoke();}
		[MonoPInvokeCallback(typeof(Action<string>))]
		private static void endedClbks(string id) { if (endedDict.ContainsKey(id)) endedDict[id].Invoke();}
        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void errorClbks(string id) { if (errorDict.ContainsKey(id)) errorDict[id].Invoke(); }
        [MonoPInvokeCallback(typeof(Action<string>))]
		private static void loadeddataClbks(string id) { if (loadeddataDict.ContainsKey(id)) loadeddataDict[id].Invoke();}
		[MonoPInvokeCallback(typeof(Action<string>))]
		private static void loadedmetadataClbks(string id) { if (loadedmetadataDict.ContainsKey(id)) loadedmetadataDict[id].Invoke();}
		[MonoPInvokeCallback(typeof(Action<string>))]
		private static void pauseClbks(string id) { if (pauseDict.ContainsKey(id)) pauseDict[id].Invoke();}
		[MonoPInvokeCallback(typeof(Action<string>))]
		private static void playClbks(string id) { if (playDict.ContainsKey(id)) playDict[id].Invoke();}
		[MonoPInvokeCallback(typeof(Action<string>))]
		private static void playingClbks(string id) { if (playingDict.ContainsKey(id)) playingDict[id].Invoke();}
		[MonoPInvokeCallback(typeof(Action<string>))]
		private static void progressClbks(string id) { if (progressDict.ContainsKey(id)) progressDict[id].Invoke();}
		[MonoPInvokeCallback(typeof(Action<string>))]
		private static void ratechangeClbks(string id) { if (ratechangeDict.ContainsKey(id)) ratechangeDict[id].Invoke();}
		[MonoPInvokeCallback(typeof(Action<string>))]
		private static void seekedClbks(string id) { if (seekedDict.ContainsKey(id)) seekedDict[id].Invoke();}
		[MonoPInvokeCallback(typeof(Action<string>))]
		private static void seekingClbks(string id) { if (seekingDict.ContainsKey(id)) seekingDict[id].Invoke();}
		[MonoPInvokeCallback(typeof(Action<string>))]
		private static void stalledClbks(string id) { if (stalledDict.ContainsKey(id)) stalledDict[id].Invoke();}
		[MonoPInvokeCallback(typeof(Action<string>))]
		private static void suspendClbks(string id) { if (suspendDict.ContainsKey(id)) suspendDict[id].Invoke();}
		[MonoPInvokeCallback(typeof(Action<string>))]
		private static void timeupdateClbks(string id) { if (timeupdateDict.ContainsKey(id)) timeupdateDict[id].Invoke();}
		[MonoPInvokeCallback(typeof(Action<string>))]
		private static void volumechangeClbks(string id) { if (volumechangeDict.ContainsKey(id)) volumechangeDict[id].Invoke();}
		[MonoPInvokeCallback(typeof(Action<string>))]
		private static void waitingClbks(string id) { if (waitingDict.ContainsKey(id)) waitingDict[id].Invoke();}
		
		///registers one event, or multiple events.
		///For example: videoPlayerWebGLInstance.RegisterEvent(VideoPlayerWebGL.evnts.timeupdate | VideoPlayerWebGL.evnts.play) will register 2 events.
		///You can add and remove callbacks from the UnityEvents directly, but you can only subscribe to an event with this method. For example
		///first you add the callbacks that you want
		///myAction += myCallback;
		///videoPlayerWebGLInstance.play.AddListener(myAction);
		///then you call videoPlayerWebGLInstance.RegisterEvent(VideoPlayerWebGL.evnts.play) to make your play UnityEvent be invoked(and all of its callbacks) when the video plays
		///you only need to call this method if you destroyed the video and recreated it without registering the event again, or if you didn't subscribe to the event in the inspector, or if you unregistered the event using the UnregisterEvent method.
		///if you want to register all events, pass (VideoPlayerWebGL.evnts)(-1) as input
		public void RegisterEvent(evnts evt) {
			foreach (evnts x in Enum.GetValues(typeof(evnts))) {
				if (!events.HasFlag(x)) {//if the event is not registered
					if (evt.HasFlag(x)) {//and the user wants to register it
                        FieldInfo fi = typeof(VideoPlayerWebGL).GetField(x.ToString() + "Dict", BindingFlags.NonPublic | BindingFlags.Static);
                        if (fi != null) {
                            Dictionary<string, UnityEvent> dict = (Dictionary<string, UnityEvent>)fi.GetValue(null);
                            if (dict != null) {
                                FieldInfo fi2 = typeof(VideoPlayerWebGL).GetField(x.ToString(), BindingFlags.Instance | BindingFlags.Public);
                                if (fi2 != null) {
                                    UnityEvent ue = fi2.GetValue(this) as UnityEvent;
                                    if (ue != null)
                                        dict.Add(id.ToString(), ue);
                                }

                            }
                        }
                    }
				} else {//if the event is already registered
					evt &= ~x;//if events already has the flag, unset from input(if input doesn't have it, nothing changes. But if it does, unset)
				}
			}
			
			events |= evt;//set flag
			#if UNITY_WEBGL && !UNITY_EDITOR
			registerEvent(id.ToString(), evt.ToString());
			#endif
		}
		
		///unregisters one event, or multiple events.
		///For example: videoPlayerWebGLInstance.UnregisterEvent(VideoPlayerWebGL.evnts.timeupdate | VideoPlayerWebGL.evnts.play) will unregister 2 events.
		///So on this example it means that when the video plays or the timeupdate event is fired from javascript, the Unity Events subscribed to them won't be invoked.
		///if the second argument is true, in addition to unregistering the event, its runtime callbacks will be removed as well.
		///callbacks added from the inspector are not removed if the second argument is true.
		///if you want to unregister all events, pass (VideoPlayerWebGL.evnts)(-1) as input
		public void UnregisterEvent(evnts evt, bool removeAllNonPersistentListeners = false) {
			foreach (evnts x in Enum.GetValues(typeof(evnts))) {
				if (events.HasFlag(x)) {//if the event is registered
					if (evt.HasFlag(x)) {//and the user wants to unregister it
                        FieldInfo fi = typeof(VideoPlayerWebGL).GetField(x.ToString() + "Dict", BindingFlags.NonPublic | BindingFlags.Static);
                        if (fi != null) {
                            Dictionary<string, UnityEvent> dict = (Dictionary<string, UnityEvent>)fi.GetValue(null);
                            if (dict != null) {
                                dict.Remove(id.ToString());
                            }
                        }
						if (removeAllNonPersistentListeners) {
                            FieldInfo fi2 = typeof(VideoPlayerWebGL).GetField(x.ToString(), BindingFlags.Instance | BindingFlags.Public);
                            if (fi2 != null) {
                                UnityEvent ue = fi2.GetValue(this) as UnityEvent;
                                if (ue != null) {
                                    ue.RemoveAllListeners();//remove all non persistent listeners
                                }
                            }
						}
					}
				} else {//if the event is not registered
					evt &= ~x;//unset from input(if input doesn't have it, nothing changes. But if it does, unset)
				}
			}

			#if UNITY_WEBGL && !UNITY_EDITOR
			unregisterEvent(id.ToString(), evt.ToString());//unregister the events on the javascript side
			#endif
			events &= ~evt;//clear flag
		}
		
		//returns all the currently registered events
		public evnts GetCurrentEvents() {
			return events;
		}

		///all of the methods below are for the inspector only
		private bool HasCanplayFlag() {return events.HasFlag(evnts.canplay);}
		private bool HasCanplaythroughFlag() {return events.HasFlag(evnts.canplaythrough);}
		private bool HasCompleteFlag() {return events.HasFlag(evnts.complete);}
		private bool HasDurationchangeFlag() {return events.HasFlag(evnts.durationchange);}
		private bool HasEmptiedFlag() {return events.HasFlag(evnts.emptied);}
		private bool HasEndedFlag() {return events.HasFlag(evnts.ended);}
        private bool HasErrorFlag() { return events.HasFlag(evnts.error); }
        private bool HasLoadeddataFlag() {return events.HasFlag(evnts.loadeddata);}
		private bool HasLoadedmetadataFlag() {return events.HasFlag(evnts.loadedmetadata);}
		private bool HasPauseFlag() {return events.HasFlag(evnts.pause);}
		private bool HasPlayFlag() {return events.HasFlag(evnts.play);}
		private bool HasPlayingFlag() {return events.HasFlag(evnts.playing);}
		private bool HasProgressFlag() {return events.HasFlag(evnts.progress);}
		private bool HasRatechangeFlag() {return events.HasFlag(evnts.ratechange);}
		private bool HasSeekedFlag() {return events.HasFlag(evnts.seeked);}
		private bool HasSeekingFlag() {return events.HasFlag(evnts.seeking);}
		private bool HasStalledFlag() {return events.HasFlag(evnts.stalled);}
		private bool HasSuspendFlag() {return events.HasFlag(evnts.suspend);}
		private bool HasTimeupdateFlag() {return events.HasFlag(evnts.timeupdate);}
		private bool HasVolumechangeFlag() {return events.HasFlag(evnts.volumechange);}
		private bool HasWaitingFlag() {return events.HasFlag(evnts.waiting);}
	}
}
