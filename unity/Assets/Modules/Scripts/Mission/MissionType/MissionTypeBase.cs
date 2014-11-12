using System;

[Serializable]
public class MissionTypeBase
{
	virtual public bool IsMissionTypeCompleted(MissionProgression a_MissionProgression)
	{
		return true;
	}
}
