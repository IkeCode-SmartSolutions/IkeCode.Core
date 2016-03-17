using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IkeCode.Core.Helpers
{
    public static class TextHelpers
    {
        public static string SpecialCharacterToHtmlEntities(string text)
        {
            text = text.Replace("¡", "&iexcl;");
            text = text.Replace("¿", "&iquest;");

            text = text.Replace("á", "&aacute;");
            text = text.Replace("é", "&eacute;");
            text = text.Replace("í", "&iacute;");
            text = text.Replace("ó", "&oacute;");
            text = text.Replace("ú", "&uacute;");
            text = text.Replace("ñ", "&ntilde;");
            text = text.Replace("ç", "&ccedil;");

            text = text.Replace("Á", "&Aacute;");
            text = text.Replace("É", "&Eacute;");
            text = text.Replace("Í", "&Iacute;");
            text = text.Replace("Ó", "&Oacute;");
            text = text.Replace("Ú", "&Uacute;");
            text = text.Replace("Ñ", "&Ntilde;");
            text = text.Replace("Ç", "&Ccedil;");

            text = text.Replace("à", "&agrave;");
            text = text.Replace("è", "&egrave;");
            text = text.Replace("ì", "&igrave;");
            text = text.Replace("ò", "&ograve;");
            text = text.Replace("ù", "&ugrave;");

            text = text.Replace("À", "&Agrave;");
            text = text.Replace("È", "&Egrave;");
            text = text.Replace("Ì", "&Igrave;");
            text = text.Replace("Ò", "&Ograve;");
            text = text.Replace("Ù", "&Ugrave;");

            text = text.Replace("ä", "&auml;");
            text = text.Replace("ë", "&euml;");
            text = text.Replace("ï", "&iuml;");
            text = text.Replace("ö", "&ouml;");
            text = text.Replace("ü", "&uuml;");

            text = text.Replace("Ä", "&Auml;");
            text = text.Replace("Ë", "&Euml;");
            text = text.Replace("Ï", "&Iuml;");
            text = text.Replace("Ö", "&Ouml;");
            text = text.Replace("Ü", "&Uuml;");

            text = text.Replace("â", "&acirc;");
            text = text.Replace("ê", "&ecirc;");
            text = text.Replace("î", "&icirc;");
            text = text.Replace("ô", "&ocirc;");
            text = text.Replace("û", "&ucirc;");

            text = text.Replace("Â", "&Acirc;");
            text = text.Replace("Ê", "&Ecirc;");
            text = text.Replace("Î", "&Icirc;");
            text = text.Replace("Ô", "&Ocirc;");
            text = text.Replace("Û", "&Ucirc;");

            return text;
        }

        public static string ToJsonString(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
