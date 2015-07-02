using System.CodeDom.Compiler;

namespace Warhammer.Core.Entities
{
    [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0")]
    public partial class Page
    {
        public bool HasImage
        {
            get { return ImageData != null && ImageData.Length > 50 && !string.IsNullOrWhiteSpace(ImageMime); }
        }
    }
}
