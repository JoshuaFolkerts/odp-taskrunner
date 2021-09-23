using System;

namespace ODP.Services.NamesGenerator
{
    /// <summary>
    /// Class for holding the lists of names from names.json
    /// </summary>
    internal class NameList
    {
        public string[] Boys { get; set; } = Array.Empty<string>();

        public string[] Girls { get; set; } = Array.Empty<string>();

        public string[] Last { get; set; } = Array.Empty<string>();

        public string[] EmailDomain { get; set; } = Array.Empty<string>();
    }
}