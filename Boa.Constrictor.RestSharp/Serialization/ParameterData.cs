using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace Boa.Constrictor.RestSharp
{
    /// <summary>
    /// Serialization class for parameters.
    /// </summary>
    public class ParameterData
    {
        #region Properties

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value.
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Type.
        /// </summary>
        public string Type { get; set; }

        #endregion

        #region Class Methods

        #pragma warning disable 0618

        /// <summary>
        /// Converts a list of parameters to a serializable object.
        /// </summary>
        /// <param name="parameters">The list of parameters.</param>
        /// <returns></returns>
        public static IList<ParameterData> GetParameterDataList(IList<Parameter> parameters) =>
            parameters.Select(p => new ParameterData
            {
                Name = p.Name,
                Value = p.Value,
                Type = p.Type.ToString()
            }).ToList();

        #pragma warning restore 0618

        #endregion
    }
}
