/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Common
{
	public partial class UI_ObjItem : GButton
	{
		public GImage m_bg;
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

			m_bg = (GImage)this.GetChildAt(0);
			m_icon = (GLoader)this.GetChildAt(1);
			m_num = (GTextField)this.GetChildAt(2);
		}
	}
}