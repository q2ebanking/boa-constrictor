namespace Boa.Constrictor.Dumping
{
    /// <summary>
    /// A dumping interface for the Screenplay Pattern.
    /// It provides a way to manage directories for data dumps.
    /// </summary>
    public interface IDumper
    {
        /// <summary>
        /// A descriptive name for the dumper.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The output directory for dumped files.
        /// Dumping is inactive if this property is null.
        /// </summary>
        string DumpDir { get; }
    }
}