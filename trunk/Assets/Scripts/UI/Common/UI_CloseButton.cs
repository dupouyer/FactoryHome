/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Common
{
	public partial class UI_CloseButton : GButton
	{
		public GImage m_n0;

		public const string URL = "ui://qqw7du1hqp14ic";

		public static UI_CloseButton CreateInstance()
		{
			return (UI_CloseButton)UIPackage.CreateObject("Common","CloseButton");
		}

		public UI_CloseButton()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_n0 = (GImage)this.GetChild("n0");
		}
	}
}