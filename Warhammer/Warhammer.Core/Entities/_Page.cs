using System;
using System.CodeDom.Compiler;

namespace Warhammer.Core.Entities
{
    [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0")]
    public partial class Page
    {

        public double ActivityBonus
        {
            get
            {
                return AgeInMonths < 1 ? 1 : 1/AgeInMonths;
            } 
        }

        public double BaseScore
        {
            get { return 0; }
        }

        public double AgeInMonths
        {
            get { return DateTime.Now.Subtract(Created).Days/(365.25/12); }           
        }
        public double AgeInDays
        {
            get
            {
                TimeSpan span = DateTime.Now - Created;
                double days = span.TotalDays;
                return days;
            }
        }

        public double DaysSinceModified
        {
            get
            {
                TimeSpan span = DateTime.Now - Modified;
                double days = span.TotalDays;
                return days;
            }
        }

        public bool HasImage
        {
            get { return ImageData != null && ImageData.Length > 50 && !string.IsNullOrWhiteSpace(ImageMime); }
        }

        public int PointsValue
        {
            get { return (int) (BaseScore + ActivityBonus); }
        }
    }
}
