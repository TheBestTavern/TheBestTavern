public enum TtrState
{
    NotStarted,
    InProgress,
    Completed,
    Cancelled
}

public enum TtrInstanceState
{
    InProgress,
    Completed,
    Failed,
}

public enum ObvState
{
    InProgress,
    Completed,
    Failed,
}

public enum ObvDoType
{ 
    SceneMove,
    GainItem,
    OpenPopup,
    EnterSubmissionMode,
    AcceptQuest,
    OpenProgressInLetter,
}

public enum ObvCountType
{
    Cumulative,
    Renew,
}