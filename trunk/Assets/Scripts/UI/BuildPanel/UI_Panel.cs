/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace BuildPanel
{
	public partial class UI_Panel : GComponent
	{
		public GImage m_n0;
		public GImage m_n4;
		public GTextField m_n2;
		public GButton m_n3;

		public const string URL = "ui://qhjxum9hugok0";

		public static UI_Panel CreateInstance()
		{
			return (UI_Panel)UIPackage.CreateObject("BuildPanel","Panel");
		}

		public UI_Panel()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_n0 = (GImage)this.GetChildAt(0);
			m_n4 = (GImage)this.GetChildAt(1);
			m_n2 = (GTextField)this.GetChildAt(2);
			m_n3 = (GButton)this.GetChildAt(3);
		}
	}
}