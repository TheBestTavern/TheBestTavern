public enum TtrState
{
    NotStarted, // 아직 튜토리얼을 한번도 받지 않은 상태
    InProgress, // 튜토리얼을 진행 중
    Completed, // 튜토리얼 모두 완료
    Cancelled // 튜토리얼을 받지 않기로 함.
}

public enum TtrInstanceState
{
    InProgress,
    ReadyClear,
}

public enum ObvState
{
    InProgress,
    Completed,
}

public enum ObvDoType
{ 
    SceneMove,
    GainItem,
    OpenPopup,
    CompleteSubmit,
    AcceptQuest,
    OpenProgressInLetter,
}

public enum ObvCountType
{
    Cumulative,
    Renew,
}