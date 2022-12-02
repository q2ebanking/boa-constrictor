using System.IO;

namespace Boa.Constrictor.Dumping
{
    /// <summary>
    /// Dumps byte data to a file in a dump directory.
    /// </summary>
    public class ByteDumper : AbstractDumper
    {
        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">A descriptive name for the dumper.</param>
        /// <param name="dumpDir">The output directory for dumping requests and responses.</param>
        /// <param name="fileToken">The token for the file name.</param>
        public ByteDumper(string name, string dumpDir, string fileToken) :
            base(name, dumpDir, fileToken)
        { }

        #endregion

        #region Methods

        /// <summary>
        /// Writes the dump file to the dump directory using the given data.
        /// Returns the dumped file's path.
        /// </summary>
        /// <param name="data">The byte data.</param>
        /// <param name="extension">The file extension. (blank by default)</param>
        /// <returns></returns>
        public string Dump(byte[] data, string extension = "")
        {
            // Make sure data is not null
            if (data == null)
                throw new DumpingException($"Dumper \"{Name}\" cannot dump null data");

            // Get the path for the file
            string path = GetDumpFilePath(extension);

            // Write the file
            File.WriteAllBytes(path, data);

            // Return the path to the file
            return path;
        }

        #endregion
    }
}
