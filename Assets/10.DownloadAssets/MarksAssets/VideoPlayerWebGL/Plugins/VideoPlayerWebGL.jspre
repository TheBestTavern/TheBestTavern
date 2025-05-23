Module['VideoPlayerWebGL'] = Module['VideoPlayerWebGL'] || {};

Module['VideoPlayerWebGL'].setPan = function(video, pan) {
	if (!(window.AudioContext || window.webkitAudioContext).prototype.createStereoPanner) return;
	if (pan === 0.0 && !this.audioCtxInternal && !video.forceMono) return;

	try {
		this.audioCtxInternal = this.audioCtxInternal || new (window.AudioContext || window.webkitAudioContext)();
	} catch(e) {
		return;
	}
	
	this.audioCtxInternal.VideoPlayerWebGL = this.audioCtxInternal.VideoPlayerWebGL || {};
	
	if (!this.audioCtxInternal.VideoPlayerWebGL[video.id]) {
		this.audioCtxInternal.VideoPlayerWebGL[video.id] = {};
		this.audioCtxInternal.VideoPlayerWebGL[video.id].source = this.audioCtxInternal.createMediaElementSource(video);
		this.audioCtxInternal.VideoPlayerWebGL[video.id].panNode = this.audioCtxInternal.createStereoPanner();
		this.audioCtxInternal.VideoPlayerWebGL[video.id].source.connect(this.audioCtxInternal.VideoPlayerWebGL[video.id].panNode);
		this.audioCtxInternal.VideoPlayerWebGL[video.id].panNode.connect(this.audioCtxInternal.destination);
	}
	
	if (video.forceMono) this.audioCtxInternal.VideoPlayerWebGL[video.id].panNode.channelCount = 1;
	else this.audioCtxInternal.VideoPlayerWebGL[video.id].panNode.channelCount = this.audioCtxInternal.VideoPlayerWebGL[video.id].source.channelCount;
	
	video.pan = this.audioCtxInternal.VideoPlayerWebGL[video.id].panNode.pan.value = pan;
};

Module['VideoPlayerWebGL'].canplay = function(id) {
	this.canplayInternal = this.canplayInternal || Module.cwrap('VideoPlayerWebGL_canplayClbks', null, ['string']);
	this.canplayInternal(id);
};

Module['VideoPlayerWebGL'].canplaythrough = function(id) {
	this.canplaythroughInternal = this.canplaythroughInternal || Module.cwrap('VideoPlayerWebGL_canplaythroughClbks', null, ['string']);
	this.canplaythroughInternal(id);
};

Module['VideoPlayerWebGL'].complete = function(id) {
	this.completeInternal = this.completeInternal || Module.cwrap('VideoPlayerWebGL_completeClbks', null, ['string']);
	this.completeInternal(id);
};

Module['VideoPlayerWebGL'].durationchange = function(id) {
	this.durationchangeInternal = this.durationchangeInternal || Module.cwrap('VideoPlayerWebGL_durationchangeClbks', null, ['string']);
	this.durationchangeInternal(id);
};

Module['VideoPlayerWebGL'].emptied = function(id) {
	this.emptiedInternal = this.emptiedInternal || Module.cwrap('VideoPlayerWebGL_emptiedClbks', null, ['string']);
	this.emptiedInternal(id);
};

Module['VideoPlayerWebGL'].ended = function(id) {
	this.endedInternal = this.endedInternal || Module.cwrap('VideoPlayerWebGL_endedClbks', null, ['string']);
	this.endedInternal(id);
};

Module['VideoPlayerWebGL'].error = function(id) {
	this.errorInternal = this.errorInternal || Module.cwrap('VideoPlayerWebGL_errorClbks', null, ['string']);
	this.errorInternal(id);
};

Module['VideoPlayerWebGL'].loadeddata = function(id) {
	this.loadeddataInternal = this.loadeddataInternal || Module.cwrap('VideoPlayerWebGL_loadeddataClbks', null, ['string']);
	this.loadeddataInternal(id);
};

Module['VideoPlayerWebGL'].loadedmetadata = function(id) {
	this.loadedmetadataInternal = this.loadedmetadataInternal || Module.cwrap('VideoPlayerWebGL_loadedmetadataClbks', null, ['string']);
	this.loadedmetadataInternal(id);
};

Module['VideoPlayerWebGL'].pause = function(id) {
	this.pauseInternal = this.pauseInternal || Module.cwrap('VideoPlayerWebGL_pauseClbks', null, ['string']);
	this.pauseInternal(id);
};

Module['VideoPlayerWebGL'].play = function(id) {
	this.playInternal = this.playInternal || Module.cwrap('VideoPlayerWebGL_playClbks', null, ['string']);
	this.playInternal(id);
};

Module['VideoPlayerWebGL'].playing = function(id) {
	this.playingInternal = this.playingInternal || Module.cwrap('VideoPlayerWebGL_playingClbks', null, ['string']);
	this.playingInternal(id);
};

Module['VideoPlayerWebGL'].progress = function(id) {
	this.progressInternal = this.progressInternal || Module.cwrap('VideoPlayerWebGL_progressClbks', null, ['string']);
	this.progressInternal(id);
};

Module['VideoPlayerWebGL'].ratechange = function(id) {
	this.ratechangeInternal = this.ratechangeInternal || Module.cwrap('VideoPlayerWebGL_ratechangeClbks', null, ['string']);
	this.ratechangeInternal(id);
};

Module['VideoPlayerWebGL'].seeked = function(id) {
	this.seekedInternal = this.seekedInternal || Module.cwrap('VideoPlayerWebGL_seekedClbks', null, ['string']);
	this.seekedInternal(id);
};

Module['VideoPlayerWebGL'].seeking = function(id) {
	this.seekingInternal = this.seekingInternal || Module.cwrap('VideoPlayerWebGL_seekingClbks', null, ['string']);
	this.seekingInternal(id);
};

Module['VideoPlayerWebGL'].stalled = function(id) {
	this.stalledInternal = this.stalledInternal || Module.cwrap('VideoPlayerWebGL_stalledClbks', null, ['string']);
	this.stalledInternal(id);
};

Module['VideoPlayerWebGL'].suspend = function(id) {
	this.suspendInternal = this.suspendInternal || Module.cwrap('VideoPlayerWebGL_suspendClbks', null, ['string']);
	this.suspendInternal(id);
};

Module['VideoPlayerWebGL'].timeupdate = function(id) {
	this.timeupdateInternal = this.timeupdateInternal || Module.cwrap('VideoPlayerWebGL_timeupdateClbks', null, ['string']);
	this.timeupdateInternal(id);
};

Module['VideoPlayerWebGL'].volumechange = function(id) {
	this.volumechangeInternal = this.volumechangeInternal || Module.cwrap('VideoPlayerWebGL_volumechangeClbks', null, ['string']);
	this.volumechangeInternal(id);
};

Module['VideoPlayerWebGL'].waiting = function(id) {
	this.waitingInternal = this.waitingInternal || Module.cwrap('VideoPlayerWebGL_waitingClbks', null, ['string']);
	this.waitingInternal(id);
};

Module['VideoPlayerWebGL'].VideoPlayerWebGL_CreateVideo = function (url, cors, id, autoplay, loop, muted, volume, pan, forceMono, playbackSpeed, texturePtr, events, canUpdateTexture, useMipMap) {
	id = UTF8ToString(id);
		
	Module['VideoPlayerWebGL'].canUpdateTextureRef = Module['VideoPlayerWebGL'].canUpdateTextureRef || canUpdateTexture;
	Module['VideoPlayerWebGL'].canUpdateTextureArr = Module['VideoPlayerWebGL'].canUpdateTextureArr || new Uint32Array(HEAP8.buffer, canUpdateTexture, 1);

	//videos have the following custom property: VideoPlayerWebGL : {requestId, texturePtr, playingFlag, timeupdateFlag, createdVideoFlag, events: {}, createVideo:function, timeupdate:function, playing:function, pause:function}
	const video = document.getElementById('VideoPlayerWebGL_' + id) || document.querySelector('body').appendChild(document.createElement('video'));
	if (video.hasAttribute('src')) return;//if it has the src attribute set, the video was already created and set up. Don't do anything else, just return.
	url = UTF8ToString(url) + '#t=0.0000001';//this last part is a trick to get Safari to show the first frame of the video, even if it doesn't play automatically.
	cors = UTF8ToString(cors);
	events = UTF8ToString(events).replace(/\s/g, '').split(',');
	events = events[0] === '-1' ? ['canplay', 'canplaythrough', 'complete', 'durationchange', 'emptied', 'ended', 'error', 'loadeddata', 'loadedmetadata', 'pause', 'play', 'playing', 'progress', 'ratechange', 'seeked', 'seeking', 'stalled', 'suspend', 'timeupdate', 'volumechange', 'waiting'] : events[0] === '0' ? undefined : events;
	video.VideoPlayerWebGL = {};
	video.VideoPlayerWebGL.playingFlag = false;
	video.VideoPlayerWebGL.timeupdateFlag = false;
	video.VideoPlayerWebGL.createdVideoFlag = false;
	video.VideoPlayerWebGL.events = {};
	video.VideoPlayerWebGL.useMipMap = useMipMap;
	events && events.forEach((function(event) {
		video.VideoPlayerWebGL.events[event] = (function() {
			Module['VideoPlayerWebGL'][event](id)
		});
		video.addEventListener(event, video.VideoPlayerWebGL.events[event])
	}));
	video.setAttribute('id', 'VideoPlayerWebGL_' + id);
	video.setAttribute('name', /[^/]*$/.exec(url)[0].replace('#t=0.0000001', ''));
	video.setAttribute('src', url);
	video.setAttribute('playsinline', '');
	video.defaultPlaybackRate = playbackSpeed;
	video.playbackRate = playbackSpeed;
	video.volume = volume;
	video.pan = pan;
	video.forceMono = forceMono;
	video.VideoPlayerWebGL.texturePtr = texturePtr;
	video.preload === 'metadata' && (video.preload = 'auto');//it needs to be done like this because of Safari. On mobile Safari, the video.preload value is already set to auto. Setting the value again to auto makes the video not play for some reason. So I only touch the preload attribute if its value is not already auto, which happens on Android Chrome. 
	muted && (video.muted = true);//the muted attribute doesn't do anything. The property that needs to be changed. They are different things.
	autoplay && video.setAttribute('autoplay', '');//it will autoplay on chrome even if not muted, as long as the user touches the screen anywhere before the video loads. The user can do this even before the Unity scenes loads. On Safari, however, this won't work. The user will need to click on a button using a pointerdown event on the Unity side, so that the play method is called on the js side on a pointerup.
	loop && video.setAttribute('l00p', '');
	cors && video.setAttribute('crossorigin', cors);
	video.style.pointerEvents = 'none';
	video.style.zIndex = '-1000';
	video.style.position = 'fixed';
	video.style.top = '0';
	video.width = video.height = 1;//using style.display = 'none' makes the autoplay fail on all browsers, even if it's muted. Changing the opacity to 0 or visibility to hidden still shows the video on the HTML as a white plane. Setting the width and height to 0 makes the autoplay fail on Safari. But setting it to 1 makes the autoplay work if it's muted on both browsers and solves the problem of showing the video on the html.
		
	video.VideoPlayerWebGL.createVideo = function() {
		if (!video.VideoPlayerWebGL.createdVideoFlag && video.VideoPlayerWebGL.playingFlag && video.VideoPlayerWebGL.timeupdateFlag) {
			video.addEventListener('timeupdate', function() {//after it's playing and time update ran once, still need to check once more to guarantee there is data. All of this set up is because of firefox.
				video.VideoPlayerWebGL.createdVideoFlag = video.VideoPlayerWebGL.canUpdateTexture = true;
				(function(number, bitPosition) {
					if (Module['VideoPlayerWebGL'].canUpdateTextureArr.byteLength === 0)//buffer resized, need to assign array again
						Module['VideoPlayerWebGL'].canUpdateTextureArr = new Uint32Array(HEAP8.buffer, Module['VideoPlayerWebGL'].canUpdateTextureRef, 1);
					
					Module['VideoPlayerWebGL'].canUpdateTextureArr[0] = number | (1 << bitPosition);
				})(Module['VideoPlayerWebGL'].canUpdateTextureArr[0], parseInt(id));
			}, {once: true});
		}
	}
	
	video.VideoPlayerWebGL.pause = (function() {
		video.VideoPlayerWebGL.canUpdateTexture = false;
		(function(number, bitPosition) {
			if (Module['VideoPlayerWebGL'].canUpdateTextureArr.byteLength === 0)//buffer resized, need to assign array again
				Module['VideoPlayerWebGL'].canUpdateTextureArr = new Uint32Array(HEAP8.buffer, Module['VideoPlayerWebGL'].canUpdateTextureRef, 1);
				
			const mask = ~(1 << bitPosition);
			Module['VideoPlayerWebGL'].canUpdateTextureArr[0] = number & mask;
		})(Module['VideoPlayerWebGL'].canUpdateTextureArr[0], parseInt(id));
	});
		
	video.VideoPlayerWebGL.playing = function() {
		if (video.VideoPlayerWebGL.createdVideoFlag) {
			video.VideoPlayerWebGL.canUpdateTexture = true;
			(function(number, bitPosition) {
				if (Module['VideoPlayerWebGL'].canUpdateTextureArr.byteLength === 0)//buffer resized, need to assign array again
					Module['VideoPlayerWebGL'].canUpdateTextureArr = new Uint32Array(HEAP8.buffer, Module['VideoPlayerWebGL'].canUpdateTextureRef, 1);
				
				Module['VideoPlayerWebGL'].canUpdateTextureArr[0] = number | (1 << bitPosition);
			})(Module['VideoPlayerWebGL'].canUpdateTextureArr[0], parseInt(id));
		} else {
			video.VideoPlayerWebGL.playingFlag = true;
			video.VideoPlayerWebGL.createVideo();
		}
	};
		
	video.VideoPlayerWebGL.timeupdate = function() {
		if (!video.VideoPlayerWebGL.createdVideoFlag) {
			video.VideoPlayerWebGL.timeupdateFlag = true;
			video.VideoPlayerWebGL.createVideo();
		}
	};
		
	video.VideoPlayerWebGL.seeked = function() {
		video.VideoPlayerWebGL.canUpdateTexture = true;
		Module['VideoPlayerWebGL'].VideoPlayerWebGL_UpdateTexture(id);
		if (!video ? false : !!(video.currentTime > 0 && !video.paused && !video.ended && video.readyState > 2)) return;//if it's playing, return. Because of Safari.
		video.VideoPlayerWebGL.canUpdateTexture = false;
	};

	video.VideoPlayerWebGL.ended = function () {
		if (video.hasAttribute('l00p')) {
			video.currentTime = 0;
			video.addEventListener('seeked', function () {
				video.play();
			}, {
				once: true
			});
		}
	};
		
	video.addEventListener('playing', video.VideoPlayerWebGL.playing);
	video.addEventListener('timeupdate', video.VideoPlayerWebGL.timeupdate);
	video.addEventListener('pause', video.VideoPlayerWebGL.pause);
	video.addEventListener('seeked', video.VideoPlayerWebGL.seeked);
	video.addEventListener('ended', video.VideoPlayerWebGL.ended);
};
	
Module['VideoPlayerWebGL'].VideoPlayerWebGL_unlockVideoPlayback = function(id) { 
	const video = document.getElementById('VideoPlayerWebGL_' + UTF8ToString(id));
	if (!video)
		return;
		
	document.documentElement.addEventListener('pointerup', function() {
		const curTime = video.currentTime;
		video.play();
		video.pause();
		video.currentTime = curTime;//just in case this function is called more than once by accident, it won't do anything.
		Module['VideoPlayerWebGL'].setPan(video, video.pan);
	}, { once: true });
		
};
Module['VideoPlayerWebGL'].VideoPlayerWebGL_UpdateTexture = function(id) {
	const video = document.getElementById('VideoPlayerWebGL_' + id);
	if (!video) return false;
	if (!video.VideoPlayerWebGL.canUpdateTexture) return false;
	if (!(video.videoWidth > 0 && video.videoHeight > 0)) return false;
	
	if (video.previousUploadedWidth != video.videoWidth || video.previousUploadedHeight != video.videoHeight) {
		GLctx.deleteTexture(GL.textures[video.VideoPlayerWebGL.texturePtr]);
		GL.textures[video.VideoPlayerWebGL.texturePtr] = GLctx.createTexture();
		GL.textures[video.VideoPlayerWebGL.texturePtr].name = video.VideoPlayerWebGL.texturePtr;
		var prevTex = GLctx.getParameter(GLctx.TEXTURE_BINDING_2D);
		GLctx.bindTexture(GLctx.TEXTURE_2D, GL.textures[video.VideoPlayerWebGL.texturePtr]);
		GLctx.pixelStorei(GLctx.UNPACK_FLIP_Y_WEBGL, true);
		GLctx.texParameteri(GLctx.TEXTURE_2D, GLctx.TEXTURE_WRAP_S, GLctx.CLAMP_TO_EDGE);
		GLctx.texParameteri(GLctx.TEXTURE_2D, GLctx.TEXTURE_WRAP_T, GLctx.CLAMP_TO_EDGE);
		!video.VideoPlayerWebGL.useMipMap ? GLctx.texParameteri(GLctx.TEXTURE_2D, GLctx.TEXTURE_MIN_FILTER, GLctx.LINEAR) : GLctx.texParameteri(GLctx.TEXTURE_2D, GLctx.TEXTURE_MIN_FILTER, GLctx.LINEAR_MIPMAP_NEAREST);
		GLctx.texImage2D(GLctx.TEXTURE_2D, 0, GLctx.RGBA, GLctx.RGBA, GLctx.UNSIGNED_BYTE, video);
		video.VideoPlayerWebGL.useMipMap && GLctx.generateMipmap(GLctx.TEXTURE_2D);
		GLctx.pixelStorei(GLctx.UNPACK_FLIP_Y_WEBGL, false);
		GLctx.bindTexture(GLctx.TEXTURE_2D, prevTex);
		video.previousUploadedWidth = video.videoWidth;
		video.previousUploadedHeight = video.videoHeight
	} else {
		GLctx.pixelStorei(GLctx.UNPACK_FLIP_Y_WEBGL, true);
		var prevTex = GLctx.getParameter(GLctx.TEXTURE_BINDING_2D);
		GLctx.bindTexture(GLctx.TEXTURE_2D, GL.textures[video.VideoPlayerWebGL.texturePtr]);
		GLctx.texImage2D(GLctx.TEXTURE_2D, 0, GLctx.RGBA, GLctx.RGBA, GLctx.UNSIGNED_BYTE, video);
		video.VideoPlayerWebGL.useMipMap && GLctx.generateMipmap(GLctx.TEXTURE_2D);
		GLctx.pixelStorei(GLctx.UNPACK_FLIP_Y_WEBGL, false);
		GLctx.bindTexture(GLctx.TEXTURE_2D, prevTex);
	}
};
Module['VideoPlayerWebGL'].VideoPlayerWebGL_PlayVideoPointerDown = function(id) { 
	const video = document.getElementById('VideoPlayerWebGL_' + UTF8ToString(id));
	if (!video)
		return;
		
	document.documentElement.addEventListener('pointerup', function() {
		video.play();
		Module['VideoPlayerWebGL'].setPan(video, video.pan);
	}, { once: true });
		
};
Module['VideoPlayerWebGL'].VideoPlayerWebGL_PlayVideo = function(id) {
	const video = document.getElementById('VideoPlayerWebGL_' + UTF8ToString(id));
	if (!video)
		return;
		
	video.play();
};
Module['VideoPlayerWebGL'].VideoPlayerWebGL_PauseVideo = function(id) {
	const video = document.getElementById('VideoPlayerWebGL_' + UTF8ToString(id));
	if (!video)
		return;
	video.pause();
};
Module['VideoPlayerWebGL'].VideoPlayerWebGL_StopVideo = function(id) {
	const video = document.getElementById('VideoPlayerWebGL_' + UTF8ToString(id));
	if (!video)
		return;
	video.pause();
	video.currentTime = 0;
};
Module['VideoPlayerWebGL'].VideoPlayerWebGL_RestartVideo = function(id) {
	const video = document.getElementById('VideoPlayerWebGL_' + UTF8ToString(id));
	if (!video) return;
	video.currentTime = 0;
	video.addEventListener('seeked', function() {
		video.play();
		video.VideoPlayerWebGL.canUpdateTexture = true;
	}, {once: true});
};
Module['VideoPlayerWebGL'].VideoPlayerWebGL_GetTimeVideo = function(id) {
	const video = document.getElementById('VideoPlayerWebGL_' + UTF8ToString(id));
	return !video ? -1.0 : video.currentTime;
};
Module['VideoPlayerWebGL'].VideoPlayerWebGL_SetTimeVideo = function(id, time) {
	const video = document.getElementById('VideoPlayerWebGL_' + UTF8ToString(id));
	if (!video) return;
	var isPlaying = !video ? false : !!(video.currentTime > 0 && !video.paused && !video.ended && video.readyState > 2);
	if (isPlaying) {
		video.pause();
		video.addEventListener('pause', function () {
			video.currentTime = time;
			video.addEventListener('seeked', function () {
				video.removeEventListener('seeking', video.VideoPlayerWebGL.events['seeking']);
				video.removeEventListener('seeked', video.VideoPlayerWebGL.events['seeked']);
				video.removeEventListener('timeupdate', video.VideoPlayerWebGL.events['timeupdate']);
	
				video.currentTime = time + 1e-5;
				video.addEventListener('seeked', function () {
					video.addEventListener('seeked', video.VideoPlayerWebGL.events['seeked']);
					video.addEventListener('seeking', video.VideoPlayerWebGL.events['seeking']);
					video.addEventListener('timeupdate', video.VideoPlayerWebGL.events['timeupdate']);
				}, { once: true });
			}, {
				once: true
			})
		}, {
			once: true
		})
	} else {
		video.currentTime = time;

		video.addEventListener('seeked', function () {
			video.removeEventListener('seeking', video.VideoPlayerWebGL.events['seeking']);
			video.removeEventListener('seeked', video.VideoPlayerWebGL.events['seeked']);
			video.removeEventListener('timeupdate', video.VideoPlayerWebGL.events['timeupdate']);

			video.currentTime = time + 1e-5;
			video.addEventListener('seeked', function () {
				video.addEventListener('seeked', video.VideoPlayerWebGL.events['seeked']);
				video.addEventListener('seeking', video.VideoPlayerWebGL.events['seeking']);
				video.addEventListener('timeupdate', video.VideoPlayerWebGL.events['timeupdate']);
			}, { once: true });

		}, {
			once: true
		})
	}
};
Module['VideoPlayerWebGL'].VideoPlayerWebGL_LengthVideo = function(id) {
	const video = document.getElementById('VideoPlayerWebGL_' + UTF8ToString(id));
	return !video ? -1.0 : video.duration;
};
Module['VideoPlayerWebGL'].VideoPlayerWebGL_SetLoopVideo = function(id, loop) {
	const video = document.getElementById('VideoPlayerWebGL_' + UTF8ToString(id));
	if (!video)
		return;
	if (loop && !video.hasAttribute('l00p'))
		video.setAttribute('l00p', '');
	else if (!loop && video.hasAttribute('l00p'))
		video.removeAttribute('l00p');
};
Module['VideoPlayerWebGL'].VideoPlayerWebGL_SetAutoplayVideo = function(id, autoplay) {
	const video = document.getElementById('VideoPlayerWebGL_' + UTF8ToString(id));
	if (!video)
		return;
	if (autoplay && !video.hasAttribute('autoplay'))
		video.setAttribute('autoplay', '');
	else if (!autoplay && video.hasAttribute('autoplay'))
		video.removeAttribute('autoplay');
};
Module['VideoPlayerWebGL'].VideoPlayerWebGL_SetMuteVideo = function(id, mute) {
	const video = document.getElementById('VideoPlayerWebGL_' + UTF8ToString(id));
	if (!video)
		return;
	video.muted = mute;
};
Module['VideoPlayerWebGL'].VideoPlayerWebGL_PlayBackSpeedVideo = function(id, speed) {
	const video = document.getElementById('VideoPlayerWebGL_' + UTF8ToString(id));
	if (!video)
		return;
	video.defaultPlaybackRate = speed;
	video.playbackRate = speed;
};
Module['VideoPlayerWebGL'].VideoPlayerWebGL_SourceVideo = function(id, url, cors) {
	const video = document.getElementById('VideoPlayerWebGL_' + UTF8ToString(id));
	if (!video) return;
	url = UTF8ToString(url);
	cors = UTF8ToString(cors);
	video.setAttribute('src', url);
	video.setAttribute('name', /[^/]*$/.exec(url));
	video.VideoPlayerWebGL.createdVideoFlag = video.VideoPlayerWebGL.playingFlag = video.VideoPlayerWebGL.timeupdateFlag = false;
	cors ? video.setAttribute('crossorigin', cors) : video.removeAttribute('crossorigin');
	video.VideoPlayerWebGL.playedFirstTimeAfterLoading = false;
	video.load();
	video.addEventListener('loadeddata', function() {video.currentTime = 0.0001;}, {once: true});
};
Module['VideoPlayerWebGL'].VideoPlayerWebGL_IsPlayingVideo = function(id) {
	const video = document.getElementById('VideoPlayerWebGL_' + UTF8ToString(id));
	return !video ? false : !!(video.currentTime > 0 && !video.paused && !video.ended && video.readyState > 2);
};
Module['VideoPlayerWebGL'].VideoPlayerWebGL_IsPausedVideo = function(id) {
	const video = document.getElementById('VideoPlayerWebGL_' + UTF8ToString(id));
	return !video ? false : video.paused;
};
Module['VideoPlayerWebGL'].VideoPlayerWebGL_WidthVideo = function(id) {
	const video = document.getElementById('VideoPlayerWebGL_' + UTF8ToString(id));
	return !video ? 0 : video.videoWidth;
},
Module['VideoPlayerWebGL'].VideoPlayerWebGL_HeightVideo = function(id) {
	const video = document.getElementById('VideoPlayerWebGL_' + UTF8ToString(id));
	return !video ? 0 : video.videoHeight;
};
Module['VideoPlayerWebGL'].VideoPlayerWebGL_SetVolumeVideo = function(id, volume) {
	const video = document.getElementById('VideoPlayerWebGL_' + UTF8ToString(id));
	if (!video)
		return;
	video.volume = volume;
};
Module['VideoPlayerWebGL'].VideoPlayerWebGL_SetPanVideo = function(id, pan) {
	const video = document.getElementById('VideoPlayerWebGL_' + UTF8ToString(id));
	if (!video)
		return;
	Module['VideoPlayerWebGL'].setPan(video, pan);
};
Module['VideoPlayerWebGL'].VideoPlayerWebGL_SetForceMonoVideo = function(id, forceMono) {
	const video = document.getElementById('VideoPlayerWebGL_' + UTF8ToString(id));
	if (!video)
		return;
			
	video.forceMono = forceMono;
};
Module['VideoPlayerWebGL'].VideoPlayerWebGL_SetCorsVideo = function(id, cors) {
	const video = document.getElementById('VideoPlayerWebGL_' + UTF8ToString(id));
	if (!video)
		return;
	cors = UTF8ToString(cors);
	cors ? video.setAttribute('crossorigin', cors) : video.removeAttribute('crossorigin');
};
Module['VideoPlayerWebGL'].VideoPlayerWebGL_IsReadyVideo = function(id) {
	const video = document.getElementById('VideoPlayerWebGL_' + UTF8ToString(id));
	if (!video)
		return;
	return video.readyState >= 4;
};
Module['VideoPlayerWebGL'].VideoPlayerWebGL_DestroyVideo = function(id) {
	id = UTF8ToString(id);
	var video = document.getElementById('VideoPlayerWebGL_' + id);
	if (!video) return;
	video.VideoPlayerWebGL.canUpdateTexture = false;
	(function(number, bitPosition) {
		if (Module['VideoPlayerWebGL'].canUpdateTextureArr.byteLength === 0)//buffer resized, need to assign array again
			Module['VideoPlayerWebGL'].canUpdateTextureArr = new Uint32Array(HEAP8.buffer, Module['VideoPlayerWebGL'].canUpdateTextureRef, 1);
			
		const mask = ~(1 << bitPosition);
		Module['VideoPlayerWebGL'].canUpdateTextureArr[0] = number & mask;
	})(Module['VideoPlayerWebGL'].canUpdateTextureArr[0], parseInt(id));
	GLctx.bindTexture(GLctx.TEXTURE_2D, null);
	video.pause();
	video.removeAttribute('src');
	video.load();
	//just to be safe...remove event listeners manually. Although it is said that modern browsers take care of this.
	Object.keys(video.VideoPlayerWebGL.events).forEach((function(event) {video.removeEventListener(event, video.VideoPlayerWebGL.events[event])}));
	video.removeEventListener('playing', video.VideoPlayerWebGL.playing);
	video.removeEventListener('timeupdate', video.VideoPlayerWebGL.timeupdate);
	video.removeEventListener('pause', video.VideoPlayerWebGL.pause);
	video.removeEventListener('seeked', video.VideoPlayerWebGL.seeked);
	video.removeEventListener('ended', video.VideoPlayerWebGL.ended);
	video.remove();
	video = null
};
Module['VideoPlayerWebGL'].VideoPlayerWebGL_RegisterEvent = function(id, events) {
	id = UTF8ToString(id);
	const video = document.getElementById('VideoPlayerWebGL_' + id);
	if (!video) return;
	events = UTF8ToString(events).replace(/\s/g, '').split(',');//can be multiple events
	events = events[0] === '-1' ? ['canplay', 'canplaythrough', 'complete', 'durationchange', 'emptied', 'ended', 'error', 'loadeddata', 'loadedmetadata', 'pause', 'play', 'playing', 'progress', 'ratechange', 'seeked', 'seeking', 'stalled', 'suspend', 'timeupdate', 'volumechange', 'waiting'] : events[0] === '0' ? undefined : events;
	events && events.forEach(function(event) {
		video.removeEventListener(event, video.VideoPlayerWebGL.events[event]);//if a previous event was already being listened to, remove it.
		video.VideoPlayerWebGL.events[event] = function() {Module['VideoPlayerWebGL'][event](id);}//store new event
		video.addEventListener(event, video.VideoPlayerWebGL.events[event]);//and listen to it.
	});
};
Module['VideoPlayerWebGL'].VideoPlayerWebGL_UnregisterEvent = function(id, events) {
	const video = document.getElementById('VideoPlayerWebGL_' + UTF8ToString(id));
	if (!video) return;
	events = UTF8ToString(events).replace(/\s/g, '').split(',');//can be multiple events
	events = events[0] === '-1' ? ['canplay', 'canplaythrough', 'complete', 'durationchange', 'emptied', 'ended', 'error', 'loadeddata', 'loadedmetadata', 'pause', 'play', 'playing', 'progress', 'ratechange', 'seeked', 'seeking', 'stalled', 'suspend', 'timeupdate', 'volumechange', 'waiting'] : events[0] === '0' ? undefined : events;
	events && events.forEach(function(event) {
		video.removeEventListener(event, video.VideoPlayerWebGL.events[event]);//if a previous event was already being listened to, remove it.
		delete video.VideoPlayerWebGL.events[event];
	});
};
Module['VideoPlayerWebGL'].VideoPlayerWebGL_GetError = function(id) {
	id = UTF8ToString(id);
	const video = document.getElementById('VideoPlayerWebGL_' + id);

	const str = video.error ? video.error.code + ',,,' + video.error.message : ',,,';
	const bufferSize = lengthBytesUTF8(str) + 1;
	const pointer = _malloc(bufferSize);
	stringToUTF8(str, pointer, bufferSize);

	return pointer;

	if (!video) return -1.0;
	return video.duration;
};
Module['VideoPlayerWebGL'].VideoPlayerWebGL_TimeRangesConstructor = function(id, property) {
	const video = document.getElementById('VideoPlayerWebGL_' + UTF8ToString(id));
	if (!video) return;
	return video[UTF8ToString(property)].length;
};
Module['VideoPlayerWebGL'].VideoPlayerWebGL_TimeRangesStart = function(index, id, property) {
	const video = document.getElementById('VideoPlayerWebGL_' + UTF8ToString(id));
	property = UTF8ToString(property);
	if (!video || video[property].length === 0) return -1.0;
	return video[property].start(index);
};
Module['VideoPlayerWebGL'].VideoPlayerWebGL_TimeRangesEnd = function(index, id, property) {
	const video = document.getElementById('VideoPlayerWebGL_' + UTF8ToString(id));
	property = UTF8ToString(property);
	if (!video || video[property].length === 0) return -1.0;
	return video[property].end(index);
},
Module['VideoPlayerWebGL'].VideoPlayerWebGL_networkState = function(id) {
	const video = document.getElementById('VideoPlayerWebGL_' + UTF8ToString(id));
	if (!video) return 0;
	return video.networkState;
};
Module['VideoPlayerWebGL'].VideoPlayerWebGL_readyState = function(id) {
	const video = document.getElementById('VideoPlayerWebGL_' + UTF8ToString(id));
	if (!video) return 0;
	return video.readyState;
};
Module['VideoPlayerWebGL'].VideoPlayerWebGL_setPreservesPitch = function(id, preservesPitch) {
	const video = document.getElementById('VideoPlayerWebGL_' + UTF8ToString(id));
	if (!video) return;
	if (video.preservesPitch !== undefined)  video.preservesPitch = preservesPitch === 0 ? false : true;
	else if (video.webkitPreservesPitch !== undefined) video.webkitPreservesPitch = preservesPitch === 0 ? false : true;
	else if (video.mozPreservesPitch !== undefined) video.mozPreservesPitch = preservesPitch === 0 ? false : true;
};
Module['VideoPlayerWebGL'].VideoPlayerWebGL_getPreservesPitch = function(id) {
	const video = document.getElementById('VideoPlayerWebGL_' + UTF8ToString(id));
	if (!video) return true;
	const preservePitch = video.preservesPitch || video.webkitPreservesPitch || video.mozPreservesPitch;
	return preservePitch;
};

