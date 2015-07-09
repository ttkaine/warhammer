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

        public new double ActivityBonus
        {
            get
            {
                double bonus = base.ActivityBonus * 5;
                bonus = bonus + Sessions.Sum(s => s.ActivityBonus);
                bonus = bonus + SessionLogs.Sum(s => s.ActivityBonus);
                return bonus;
            }
        }

        public new double BaseScore
        {
            get
            {
                double score = base.BaseScore;
                score = score + Related.Where(s => !SessionLogs.Contains(s)).Sum(l => l.BaseScore);
                score = score + Related1.Where(s => !SessionLogs.Contains(s)).Sum(l => l.BaseScore);
                score = score + SessionLogs.Sum(l => l.BaseScore);
                return score;
            }
        }

        public new int PointsValue
        {
            get { return (int) BaseScore + (int) ActivityBonus; }
        }
    }
}
