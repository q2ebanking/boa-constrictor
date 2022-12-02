using Newtonsoft.Json;
using System.IO;

namespace Boa.Constrictor.Dumping
{
    /// <summary>
    /// Dumps JSON data to a file in a dump directory.
    /// </summary>
    public class JsonDumper : AbstractDumper
    {
        #region Constants

        /// <summary>
        /// The JSON file extension.
        /// </summary>
        public const string JsonExtension = ".json";

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">A descriptive name for the dumper.</param>
        /// <param name="dumpDir">The output directory for dumping requests and responses.</param>
        /// <param name="fileToken">The token for the file name.</param>
        public JsonDumper(string name, string dumpDir, string fileToken) :
            base(name, dumpDir, fileToken)
        { }

        #endregion

        #region Methods

        /// <summary>
        /// Writes the JSON dump file to the dump directory using the given data.
        /// Returns the dumped file's path.
        /// </summary>
        /// <param name="jsonData">The JSON data object.</param>
        /// <returns></returns>
        public string Dump(object jsonData)
        {
            // Get the path for the file
            string path = GetDumpFilePath(JsonExtension);

            // Write the JSON file
            using (var file = new StreamWriter(path))
                file.Write(JsonConvert.SerializeObject(jsonData, Formatting.Indented));

            // Return the path to the file
            return path;
        }

        #endregion
    }
}
