using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;

namespace Warhammer.Core.Entities
{
    [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0")]
    public partial class Person
    {
        public IEnumerable<Session> Sessions
        {
            get { return Related.OfType<Session>(); }
        }

        public new int PointsValue
        {
            get
            {
                int logValue = SessionLogs.Sum(p => p.PointsValue);
                double recentBonus = AgeInMonths < 10 ? 10 - AgeInMonths : 0;
                double linkValue = (Related.Count + Related1.Count) / 3.0;

                return (int) (logValue + recentBonus + linkValue);
            }
        }
    }
}
