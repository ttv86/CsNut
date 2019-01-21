using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTTD;

namespace CsAi
{
    public class SignManager
    {
        private AISignList signs = null;
        private Dictionary<TileIndex, SignID> signDictionary;

        public SignManager()
        {
            this.signs = new AISignList();
            this.signDictionary = new Dictionary<TileIndex, SignID>();
        }
        
        internal void BuildSign(TileIndex tile, string text)
        {
            if (signDictionary.ContainsKey(tile))
            {
                AISign.RemoveSign(signDictionary[tile]);
            }

            if (!string.IsNullOrEmpty(text))
            {
                var signId = AISign.BuildSign(tile, "" + text);
                this.signs.AddItem(signId, 0);
                signDictionary[tile] = signId;
            }
        }

        internal void ClearSigns()
        {
            foreach (var (signId, _) in this.signs)
            {
                AISign.RemoveSign(signId);
            }

            this.signs.Clear();
            this.signDictionary.Clear();
        }
    }
}
