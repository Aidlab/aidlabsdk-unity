namespace Aidlab
{
    public enum ActivityType
    {
        Automotive,
        Walking,
        Running,
        Cycling,
        Still,
        Stilll
    }

    public enum WearState
    {
        PlacedProperly,
        Loose,
        PlacedUpsideDown,
        Detached
    }

    public enum Exercise
    {
        PushUp,
        Jump,
        SitUp,
        Burpee,
        PullUp,
        Squat,
        PlankStart,
        PlankEnd
    }

    public enum SyncState
    {
        Start,
        End,
        Stop,
        Empty
    }

    public enum BodyPosition
    {
        Unknown,
        Front,
        Back,
        LeftSide,
        RightSide
    }

    public enum Signal
    {
        Ecg,
        Respiration,
        Temperature,
        Motion,
        Battery,
        Activity,
        Orientation,
        Steps,
        HeartRate,
        HealthThermometer,
        SoundVolume
    }
}
