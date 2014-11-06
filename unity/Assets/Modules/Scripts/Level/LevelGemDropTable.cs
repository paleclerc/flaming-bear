using System.Collections.Generic;
using System;

[Serializable]
public class LevelGemDropTable
{
	public LevelGemDropTableInfo m_DefaultDropTable;
	public List<LevelGemDropTableInfo> m_OtherDropTable;
}

[Serializable]
public class LevelGemDropTableInfo
{
	public string m_DisplayName;
	public List<GemDropTableItem> m_DropTable;
}