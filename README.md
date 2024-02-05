# Time Rewind

A simple system to record actions and then rewind them. The base system contains recording: **position** `PositionRecorder.cs` and **material color** `MaterialColorRecorder.cs`. 
If you want to add more, you need only inherit from `TimeRecorder.cs` class and implement the required methods: 

<pre>
  protected abstract void InitializeAction();

  protected abstract void StartRecordingAction();
  protected abstract void RecordingAction(int frame);
  protected abstract void StopRecordingAction();

  protected abstract void StartRewindAction();
  protected abstract void RewindAction(int frame, bool frameByFrame);
  protected abstract void StopRewindAction();
</pre>

Then be sure your script is attached to the specified GameObject and that it's a child of `TimeReverseController.cs`. (Alternatively, change the way of filling the time recorder list). 



Used assets and libraries:
* Icons: https://www.iconfinder.com/search/icons?family=feather
