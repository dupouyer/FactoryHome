/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Common
{
	public partial class UI_Button : GButton
	{
		public Controller m_button;
		public GImage m_n1;
		public GTextField m_title;

		public const string URL = "ui://qqw7du1hv2bxi9";

		public static UI_Button CreateInstance()
		{
			return (UI_Button)UIPackage.CreateObject("Common","Button");
		}

		public UI_Button()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_button = this.GetControllerAt(0);
			m_n1 = (GImage)this.GetChildAt(0);
			m_title = (GTextField)this.GetChildAt(1);
		}
	}
}