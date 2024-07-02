using System.Collections.Generic;

namespace Nexus
{
    public class Root
    {
        public List<Mod> Mods { get; set; }

        public List<Texture> Texturas { get; set; }
        public List<Version> Versions { get; set; }
        public List<Herramienta> Herramientas { get; set; }
        public List<ModPack> ModPacks { get; set; }
    }
}
