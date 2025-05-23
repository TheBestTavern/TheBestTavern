mergeInto(LibraryManager.library, {
	VideoPlayerWebGL_CreateVideo: function (url, cors, id, autoplay, loop, muted, volume, pan, forceMono, playbackSpeed, texturePtr, events, canUpdateTexture, useMipMap) {
		Module['VideoPlayerWebGL'].VideoPlayerWebGL_CreateVideo(url, cors, id, autoplay, loop, muted, volume, pan, forceMono, playbackSpeed, texturePtr, events, canUpdateTexture, useMipMap)
    },
	VideoPlayerWebGL_unlockVideoPlayback: function(id) {
		Module['VideoPlayerWebGL'].VideoPlayerWebGL_unlockVideoPlayback(id);
	},
	VideoPlayerWebGL_UpdateTexture: function(id) {
		Module['VideoPlayerWebGL'].VideoPlayerWebGL_UpdateTexture(id);
	},
	VideoPlayerWebGL_PlayVideoPointerDown: function(id) { 
		Module['VideoPlayerWebGL'].VideoPlayerWebGL_PlayVideoPointerDown(id);
	},
	VideoPlayerWebGL_PlayVideo: function(id) {
		Module['VideoPlayerWebGL'].VideoPlayerWebGL_PlayVideo(id);
	},
	VideoPlayerWebGL_PauseVideo: function(id) {
		Module['VideoPlayerWebGL'].VideoPlayerWebGL_PauseVideo(id);
	},
	VideoPlayerWebGL_StopVideo: function(id) {
		Module['VideoPlayerWebGL'].VideoPlayerWebGL_StopVideo(id);
	},
	VideoPlayerWebGL_RestartVideo: function(id) {
		Module['VideoPlayerWebGL'].VideoPlayerWebGL_RestartVideo(id);
	},
	VideoPlayerWebGL_GetTimeVideo: function(id) {
		return Module['VideoPlayerWebGL'].VideoPlayerWebGL_GetTimeVideo(id);
	},
	VideoPlayerWebGL_SetTimeVideo: function(id, time) {
		Module['VideoPlayerWebGL'].VideoPlayerWebGL_SetTimeVideo(id, time);
	},
	VideoPlayerWebGL_LengthVideo: function(id) {
		return Module['VideoPlayerWebGL'].VideoPlayerWebGL_LengthVideo(id);
	},
	VideoPlayerWebGL_SetLoopVideo: function(id, loop) {
		Module['VideoPlayerWebGL'].VideoPlayerWebGL_SetLoopVideo(id, loop);
	},
	VideoPlayerWebGL_SetAutoplayVideo: function(id, autoplay) {
		Module['VideoPlayerWebGL'].VideoPlayerWebGL_SetAutoplayVideo(id, autoplay);
	},
	VideoPlayerWebGL_SetMuteVideo: function(id, mute) {
		Module['VideoPlayerWebGL'].VideoPlayerWebGL_SetMuteVideo(id, mute);
	},
	VideoPlayerWebGL_PlayBackSpeedVideo: function(id, speed) {
		Module['VideoPlayerWebGL'].VideoPlayerWebGL_PlayBackSpeedVideo(id, speed);
	},
	VideoPlayerWebGL_SourceVideo: function(id, url, cors) {
		Module['VideoPlayerWebGL'].VideoPlayerWebGL_SourceVideo(id, url, cors);
	},
	VideoPlayerWebGL_IsPlayingVideo: function(id) {
		return Module['VideoPlayerWebGL'].VideoPlayerWebGL_IsPlayingVideo(id);
	},
	VideoPlayerWebGL_IsPausedVideo: function(id) {
		return Module['VideoPlayerWebGL'].VideoPlayerWebGL_IsPausedVideo(id);
	},
	VideoPlayerWebGL_WidthVideo: function(id) {
		return Module['VideoPlayerWebGL'].VideoPlayerWebGL_WidthVideo(id);
	},
	VideoPlayerWebGL_HeightVideo: function(id) {
		return Module['VideoPlayerWebGL'].VideoPlayerWebGL_HeightVideo(id);
	},
	VideoPlayerWebGL_SetVolumeVideo: function(id, volume) {
		Module['VideoPlayerWebGL'].VideoPlayerWebGL_SetVolumeVideo(id, volume);
	},
	VideoPlayerWebGL_SetPanVideo: function(id, pan) {
		Module['VideoPlayerWebGL'].VideoPlayerWebGL_SetPanVideo(id, pan);
	},
	VideoPlayerWebGL_SetForceMonoVideo: function(id, forceMono) {
		Module['VideoPlayerWebGL'].VideoPlayerWebGL_SetForceMonoVideo(id, forceMono);
	},
	VideoPlayerWebGL_SetCorsVideo: function(id, cors) {
		Module['VideoPlayerWebGL'].VideoPlayerWebGL_SetCorsVideo(id, cors);
	},
	VideoPlayerWebGL_IsReadyVideo: function(id) {
		return Module['VideoPlayerWebGL'].VideoPlayerWebGL_IsReadyVideo(id);
	},
	VideoPlayerWebGL_DestroyVideo: function(id) {
		Module['VideoPlayerWebGL'].VideoPlayerWebGL_DestroyVideo(id);
	},
	VideoPlayerWebGL_RegisterEvent: function(id, events) {
		Module['VideoPlayerWebGL'].VideoPlayerWebGL_RegisterEvent(id, events);
	},
	VideoPlayerWebGL_UnregisterEvent: function(id, events) {
		Module['VideoPlayerWebGL'].VideoPlayerWebGL_UnregisterEvent(id, events);
	},
	VideoPlayerWebGL_GetError: function(id) {
		return Module['VideoPlayerWebGL'].VideoPlayerWebGL_GetError(id);
	},
	VideoPlayerWebGL_TimeRangesConstructor: function(id, property) {
		return Module['VideoPlayerWebGL'].VideoPlayerWebGL_TimeRangesConstructor(id, property);
	},
	VideoPlayerWebGL_TimeRangesStart: function(index, id, property) {
		return Module['VideoPlayerWebGL'].VideoPlayerWebGL_TimeRangesStart(index, id, property);
	},
	VideoPlayerWebGL_TimeRangesEnd: function(index, id, property) {
		return Module['VideoPlayerWebGL'].VideoPlayerWebGL_TimeRangesEnd(index, id, property);
	},
	VideoPlayerWebGL_networkState: function(id) {
		return Module['VideoPlayerWebGL'].VideoPlayerWebGL_networkState(id);
	},
	VideoPlayerWebGL_readyState: function(id) {
		return Module['VideoPlayerWebGL'].VideoPlayerWebGL_readyState(id);
	},
	VideoPlayerWebGL_setPreservesPitch: function(id, preservesPitch) {
		Module['VideoPlayerWebGL'].VideoPlayerWebGL_setPreservesPitch(id, preservesPitch);
	},
	VideoPlayerWebGL_getPreservesPitch: function(id) {
		return Module['VideoPlayerWebGL'].VideoPlayerWebGL_getPreservesPitch(id);
	}
});