/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Common
{
	public partial class UI_ProgressBar : GProgressBar
	{
		public GImage m_n1;
		public GImage m_bar;
		public GImage m_n4;
		public GTextField m_title;

		public const string URL = "ui://qqw7du1hf6s9im";

		public static UI_ProgressBar CreateInstance()
		{
			return (UI_ProgressBar)UIPackage.CreateObject("Common","ProgressBar");
		}

		public UI_ProgressBar()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_n1 = (GImage)this.GetChild("n1");
			m_bar = (GImage)this.GetChild("bar");
			m_n4 = (GImage)this.GetChild("n4");
			m_title = (GTextField)this.GetChild("title");
		}
	}
}