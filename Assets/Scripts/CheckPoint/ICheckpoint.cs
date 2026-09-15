internal interface ICheckpoint
{
    string CheckpointID { get; }

    void Activate();
}