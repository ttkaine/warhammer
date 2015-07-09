using System;
using System.CodeDom.Compiler;
using System.Linq;

namespace Warhammer.Core.Entities
{
       [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0")]
    public partial class SessionLog
    {
        public new double BaseScore
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Description))
                {
                    return -1;
                }
                string theContent = Description.Trim();
                while (theContent.Contains("  "))
                {
                    theContent = theContent.Replace("  ", " ");
                }
                int words = theContent.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Count();
                double baseScore = words / 250.0;
                return baseScore;
            }
        }

        public new int PointsValue
        {
            get
            {
                double recentBonus = AgeInMonths < 10 ? 10 - AgeInMonths : 0;
                double linkValue = (Related.Count + Related1.Count) / 5.0;

                return (int) (BaseScore + recentBonus + linkValue);
            }
        }
    }
}
