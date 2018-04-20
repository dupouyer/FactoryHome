/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Common
{
	public partial class UI_ObjItem : GButton
	{
		public Controller m_button;
		public GImage m_n4;
		public GImage m_n3;
		public GImage m_n5;
		public GLoader m_icon;
		public GTextField m_num;

		public const string URL = "ui://qqw7du1hpkm4ib";

		public static UI_ObjItem CreateInstance()
		{
			return (UI_ObjItem)UIPackage.CreateObject("Common","ObjItem");
		}

		public UI_ObjItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_button = this.GetController("button");
			m_n4 = (GImage)this.GetChild("n4");
			m_n3 = (GImage)this.GetChild("n3");
			m_n5 = (GImage)this.GetChild("n5");
			m_icon = (GLoader)this.GetChild("icon");
			m_num = (GTextField)this.GetChild("num");
		}
	}
}