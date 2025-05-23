#include "emscripten.h"

void (*VideoPlayerWebGL_canplayClbks_ref)(const char *);
void (*VideoPlayerWebGL_canplaythroughClbks_ref)(const char *);
void (*VideoPlayerWebGL_completeClbks_ref)(const char *);
void (*VideoPlayerWebGL_durationchangeClbks_ref)(const char *);
void (*VideoPlayerWebGL_emptiedClbks_ref)(const char *);
void (*VideoPlayerWebGL_endedClbks_ref)(const char *);
void (*VideoPlayerWebGL_errorClbks_ref)(const char *);
void (*VideoPlayerWebGL_loadeddataClbks_ref)(const char *);
void (*VideoPlayerWebGL_loadedmetadataClbks_ref)(const char *);
void (*VideoPlayerWebGL_pauseClbks_ref)(const char *);
void (*VideoPlayerWebGL_playClbks_ref)(const char *);
void (*VideoPlayerWebGL_playingClbks_ref)(const char *);
void (*VideoPlayerWebGL_progressClbks_ref)(const char *);
void (*VideoPlayerWebGL_ratechangeClbks_ref)(const char *);
void (*VideoPlayerWebGL_seekedClbks_ref)(const char *);
void (*VideoPlayerWebGL_seekingClbks_ref)(const char *);
void (*VideoPlayerWebGL_stalledClbks_ref)(const char *);
void (*VideoPlayerWebGL_suspendClbks_ref)(const char *);
void (*VideoPlayerWebGL_timeupdateClbks_ref)(const char *);
void (*VideoPlayerWebGL_volumechangeClbks_ref)(const char *);
void (*VideoPlayerWebGL_waitingClbks_ref)(const char *);

void VideoPlayerWebGL_setUnityFunctions(void (*canplayClbks) (const char *), void (*canplaythroughClbks) (const char *), void (*completeClbks) (const char *), void (*durationchangeClbks) (const char *), void (*emptiedClbks) (const char *), void (*endedClbks) (const char *), void (*errorClbks) (const char *), void (*loadeddataClbks) (const char *), void (*loadedmetadataClbks) (const char *), void (*pauseClbks) (const char *), void (*playClbks) (const char *), void (*playingClbks) (const char *), void (*progressClbks) (const char *), void (*ratechangeClbks) (const char *), void (*seekedClbks) (const char *), void (*seekingClbks) (const char *), void (*stalledClbks) (const char *), void (*suspendClbks) (const char *), void (*timeupdateClbks) (const char *), void (*volumechangeClbks) (const char *), void (*waitingClbks) (const char *)) {
	if (VideoPlayerWebGL_canplayClbks_ref) return;
	VideoPlayerWebGL_canplayClbks_ref        = canplayClbks;
	VideoPlayerWebGL_canplaythroughClbks_ref = canplaythroughClbks;
	VideoPlayerWebGL_completeClbks_ref       = completeClbks;
	VideoPlayerWebGL_durationchangeClbks_ref = durationchangeClbks;
	VideoPlayerWebGL_emptiedClbks_ref        = emptiedClbks;
	VideoPlayerWebGL_endedClbks_ref          = endedClbks;
	VideoPlayerWebGL_errorClbks_ref          = errorClbks;
	VideoPlayerWebGL_loadeddataClbks_ref     = loadeddataClbks;
	VideoPlayerWebGL_loadedmetadataClbks_ref = loadedmetadataClbks;
	VideoPlayerWebGL_pauseClbks_ref          = pauseClbks;
	VideoPlayerWebGL_playClbks_ref           = playClbks;
	VideoPlayerWebGL_playingClbks_ref        = playingClbks;
	VideoPlayerWebGL_progressClbks_ref       = progressClbks;
	VideoPlayerWebGL_ratechangeClbks_ref     = ratechangeClbks;
	VideoPlayerWebGL_seekedClbks_ref         = seekedClbks;
	VideoPlayerWebGL_seekingClbks_ref        = seekingClbks;
	VideoPlayerWebGL_stalledClbks_ref        = stalledClbks;
	VideoPlayerWebGL_suspendClbks_ref        = suspendClbks;
	VideoPlayerWebGL_timeupdateClbks_ref     = timeupdateClbks;
	VideoPlayerWebGL_volumechangeClbks_ref   = volumechangeClbks;
	VideoPlayerWebGL_waitingClbks_ref        = waitingClbks;
}

void EMSCRIPTEN_KEEPALIVE VideoPlayerWebGL_canplayClbks(const char *id) {VideoPlayerWebGL_canplayClbks_ref(id);}
void EMSCRIPTEN_KEEPALIVE VideoPlayerWebGL_canplaythroughClbks(const char *id) {VideoPlayerWebGL_canplaythroughClbks_ref(id);}
void EMSCRIPTEN_KEEPALIVE VideoPlayerWebGL_completeClbks(const char *id) {VideoPlayerWebGL_completeClbks_ref(id);}
void EMSCRIPTEN_KEEPALIVE VideoPlayerWebGL_durationchangeClbks(const char *id) {VideoPlayerWebGL_durationchangeClbks_ref(id);}
void EMSCRIPTEN_KEEPALIVE VideoPlayerWebGL_emptiedClbks(const char *id) {VideoPlayerWebGL_emptiedClbks_ref(id);}
void EMSCRIPTEN_KEEPALIVE VideoPlayerWebGL_endedClbks(const char *id) {VideoPlayerWebGL_endedClbks_ref(id);}
void EMSCRIPTEN_KEEPALIVE VideoPlayerWebGL_errorClbks(const char *id) {VideoPlayerWebGL_errorClbks_ref(id);}
void EMSCRIPTEN_KEEPALIVE VideoPlayerWebGL_loadeddataClbks(const char *id) {VideoPlayerWebGL_loadeddataClbks_ref(id);}
void EMSCRIPTEN_KEEPALIVE VideoPlayerWebGL_loadedmetadataClbks(const char *id) {VideoPlayerWebGL_loadedmetadataClbks_ref(id);}
void EMSCRIPTEN_KEEPALIVE VideoPlayerWebGL_pauseClbks(const char *id) {VideoPlayerWebGL_pauseClbks_ref(id);}
void EMSCRIPTEN_KEEPALIVE VideoPlayerWebGL_playClbks(const char *id) {VideoPlayerWebGL_playClbks_ref(id);}
void EMSCRIPTEN_KEEPALIVE VideoPlayerWebGL_playingClbks(const char *id) {VideoPlayerWebGL_playingClbks_ref(id);}
void EMSCRIPTEN_KEEPALIVE VideoPlayerWebGL_progressClbks(const char *id) {VideoPlayerWebGL_progressClbks_ref(id);}
void EMSCRIPTEN_KEEPALIVE VideoPlayerWebGL_ratechangeClbks(const char *id) {VideoPlayerWebGL_ratechangeClbks_ref(id);}
void EMSCRIPTEN_KEEPALIVE VideoPlayerWebGL_seekedClbks(const char *id) {VideoPlayerWebGL_seekedClbks_ref(id);}
void EMSCRIPTEN_KEEPALIVE VideoPlayerWebGL_seekingClbks(const char *id) {VideoPlayerWebGL_seekingClbks_ref(id);}
void EMSCRIPTEN_KEEPALIVE VideoPlayerWebGL_stalledClbks(const char *id) {VideoPlayerWebGL_stalledClbks_ref(id);}
void EMSCRIPTEN_KEEPALIVE VideoPlayerWebGL_suspendClbks(const char *id) {VideoPlayerWebGL_suspendClbks_ref(id);}
void EMSCRIPTEN_KEEPALIVE VideoPlayerWebGL_timeupdateClbks(const char *id) {VideoPlayerWebGL_timeupdateClbks_ref(id);}
void EMSCRIPTEN_KEEPALIVE VideoPlayerWebGL_volumechangeClbks(const char *id) {VideoPlayerWebGL_volumechangeClbks_ref(id);}
void EMSCRIPTEN_KEEPALIVE VideoPlayerWebGL_waitingClbks(const char *id) {VideoPlayerWebGL_waitingClbks_ref(id);}