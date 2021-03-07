using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeaponMaker.Source.Utils
{
    public static class ClipboardUtils
    {
        public static void CopyToClipboard(Weapon weapon)
        {
            //register my custom data format with Windows
            //or get it if it's already registered
            DataFormats.Format format = DataFormats.GetFormat(typeof(Weapon).FullName);

            //now copy to clipboard
            IDataObject dataObj = new DataObject();
            dataObj.SetData(format.Name, false, weapon);
            Clipboard.SetDataObject(dataObj, false);

            //that's it
        }

        public static Weapon GetFromClipboard()
        {
            Weapon doc = null;
            IDataObject dataObj = Clipboard.GetDataObject();
            string format = typeof(Weapon).FullName;

            if (dataObj.GetDataPresent(format))
            {
                var doc2 = dataObj.GetData(format, false);
                doc = doc2 as Weapon;
            }
            return doc;
        }
    }
}
