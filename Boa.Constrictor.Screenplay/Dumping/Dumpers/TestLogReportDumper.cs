using Boa.Constrictor.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Boa.Constrictor.Dumping
{
    /// <summary>
    /// Dumps an HTML report that combines all test logs.
    /// </summary>
    public class TestLogReportDumper : AbstractDumper
    {
        #region Constants

        /// <summary>
        /// The default title for the report.
        /// </summary>
        public const string DefaultTitle = "Test Log Report";

        /// <summary>
        /// The HTML file extension.
        /// </summary>
        public const string HtmlExtension = ".html";

        #endregion

        #region Properties

        /// <summary>
        /// The report title.
        /// </summary>
        public string Title { get; private set; }
        
        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">A descriptive name for the dumper.</param>
        /// <param name="dumpDir">The output directory for dumping requests and responses.</param>
        /// <param name="fileToken">The token for the file name.</param>
        /// <param name="title">The report title.</param>
        public TestLogReportDumper(string name, string dumpDir, string fileToken, string title = DefaultTitle) :
            base(name, dumpDir, fileToken) => Title = title;

        #endregion

        #region Private Methods
        
        /// <summary>
        /// Gets CSS text for a result's background color.
        /// String format: "background-color: #xxxxxx;".
        /// Returns an empty string if no color is designated for the result.
        /// </summary>
        /// <param name="result">The result string.</param>
        /// <returns></returns>
        private string GetBackgroundColor(string result)
        {
            string lowerResult = result.ToLower();
            string background = "";

            if (lowerResult.Contains("pass") || lowerResult.Contains("success") || lowerResult.Contains("ok"))
                background = " background-color: #B8F5B5;";   // light green
            else if (lowerResult.Contains("fail") || lowerResult.Contains("error") || lowerResult.Contains("fatal"))
                background = " background-color: #FFCCCB;";   // light red
            else if (lowerResult.Contains("skip") || lowerResult.Contains("ignore"))
                background = " background-color: #FFFFCC;";   // light yellow

            return background;
        }

        /// <summary>
        /// Converts a path to an embeddable URI for linking from the HTML report.
        /// If the absolute path isn't truly absolute, simply return it as a relative URI.
        /// If no relative path is given, then return the absolute path as a URI.
        /// If a relative path is given, then return a relative URI for the absolute path.
        /// </summary>
        /// <param name="absolute">The absolute file path.</param>
        /// <param name="relative">The relative directory. May be null or blank.</param>
        /// <returns></returns>
        public string ConvertPath(string absolute, string relative = null)
        {
            string newPath;

            if (!Path.IsPathRooted(absolute))
            {
                newPath = absolute;
            }
            else if (string.IsNullOrWhiteSpace(relative))
            {
                newPath = new Uri(absolute).ToString();
            }
            else
            {
                if (relative.Last() != '\\')
                    relative += '\\';

                Uri absoluteUri = new Uri(absolute);
                Uri relativeUri = new Uri(relative);
                Uri newUri = relativeUri.MakeRelativeUri(absoluteUri);

                newPath = newUri.ToString();
            }

            return newPath;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Writes the HTML report using the given TestLogData objects.
        /// Returns the report file's file path.
        /// </summary>
        /// <param name="logs">A list of log data objects.</param>
        /// <param name="relativePath">If provided, overwrites artifact absolute paths with this relative path.</param>
        /// <param name="maxScreenshotWidth">Maximum screenshot width in pixels. Defaults to 1500.</param>
        /// <returns></returns>
        public string Dump(IList<TestLogData> logs, string relativePath = null, int maxScreenshotWidth=1500)
        {
            // Make sure data is not null
            if (logs == null)
                throw new DumpingException($"Dumper \"{Name}\" cannot dump null data");

            // Create a string builder
            StringBuilder html = new StringBuilder();

            // Append HTML doc opener
            html.AppendLine("<!DOCTYPE html>");
            html.AppendLine("<html>");
            html.AppendLine("<head>");
            html.AppendLine("<title>Test Log Report</title>");
            html.AppendLine("</head>");
            html.AppendLine("<body style='font-family: Helvetica, sans-serif;'>");
            html.AppendLine("<div style='padding: 20px'>");
            html.AppendLine($"<h1>{Title}</h1>");

            // Append summary div
            html.AppendLine($"<hr />");
            html.AppendLine("<div style='padding-left: 25px; padding-bottom: 25px;'>");

            // Append test summary header
            html.AppendLine($"<h2 style='padding-top: 10px'>Test Summary</h2>");
            html.AppendLine("<table style='border: 1px solid lightgray; border-collapse: collapse;'>");
            html.AppendLine("<tr>");
            html.AppendLine("<th style='border: 1px solid lightgray; padding: 8px; background-color: #EEEEEE;'>Number</th>");
            html.AppendLine("<th style='border: 1px solid lightgray; padding: 8px; background-color: #EEEEEE;'>Result</th>");
            html.AppendLine("<th style='border: 1px solid lightgray; padding: 8px; background-color: #EEEEEE;'>Name</th>");
            html.AppendLine("</tr>");

            // Append test summary
            int count = 0;
            foreach (var log in logs)
            {
                count++;
                string background = GetBackgroundColor(log.Result);

                html.AppendLine("<tr>");
                html.AppendLine($"<td style='border: 1px solid lightgray; padding: 8px'><a href='#test{count}'>{count}</a></td>");
                html.AppendLine($"<td style='border: 1px solid lightgray; padding: 8px;{background}'>{log.Result}</td>");
                html.AppendLine($"<td style='border: 1px solid lightgray; padding: 8px'>{log.Name}</td>");
                html.AppendLine("</tr>");
            }
            html.AppendLine("</table>");

            // Append result summary header
            html.AppendLine($"<h2 style='padding-top: 10px'>Result Summary</h2>");
            html.AppendLine("<table style='border: 1px solid lightgray; border-collapse: collapse;'>");
            html.AppendLine("<tr>");
            html.AppendLine("<th style='border: 1px solid lightgray; padding: 8px; background-color: #EEEEEE;'>Result</th>");
            html.AppendLine("<th style='border: 1px solid lightgray; padding: 8px; background-color: #EEEEEE;'>Count</th>");
            html.AppendLine("</tr>");

            // Append result summary 
            foreach (string result in logs.Select(log => log.Result).Distinct())
            {
                int resultCount = logs.Where(log => log.Result == result).Count();
                string background = GetBackgroundColor(result);

                html.AppendLine("<tr>");
                html.AppendLine($"<td style='border: 1px solid lightgray; padding: 8px;{background}'>{result}</td>");
                html.AppendLine($"<td style='border: 1px solid lightgray; padding: 8px;'>{resultCount}</td>");
                html.AppendLine("</tr>");
            }

            // Append result summary total
            html.AppendLine("<tr>");
            html.AppendLine($"<td style='border: 1px solid lightgray; padding: 8px;'><b>Total</b></td>");
            html.AppendLine($"<td style='border: 1px solid lightgray; padding: 8px;'><b>{logs.Count}</b></td>");
            html.AppendLine("</tr>");
            html.AppendLine("</table>");

            // Append log content
            count = 0;
            foreach (var log in logs)
            {
                // Append horizontal break
                html.AppendLine($"</div>");
                html.AppendLine($"<hr />");
                html.AppendLine("<div style='padding-left: 25px; padding-bottom: 25px;'>");

                // Test name and result
                count++;
                html.AppendLine($"<h2 style='padding-top: 10px'><a name='test{count}'>{count}. {log.Name}</a></h2>");
                html.AppendLine($"<p><b>Result</b>: {log.Result}</p>");
                html.AppendLine("<table style='border: 1px solid lightgray; border-collapse: collapse;'>");

                foreach (var step in log.Steps)
                {
                    // Step name
                    html.AppendLine("<tr><td style='border: 1px solid lightgray; padding: 10px'>");
                    html.AppendLine($"<h3>{step.Name}</h3>");
                    html.AppendLine("<div style='padding-left: 10px; padding-right: 10px; padding-bottom: 10px'>");

                    // Step messages
                    html.AppendLine("<h4>Messages</h4>");
                    html.AppendLine("<div style='margin-left: 20px; margin-right: 20px; background-color: #F0F0F0'>");
                    html.AppendLine("<pre>");
                    foreach (var message in step.Messages)
                    {
                        html.AppendLine(message);
                    }
                    html.AppendLine("</pre>");
                    html.AppendLine("</div>");

                    // Step artifacts (other than screenshots)
                    var types = step.Artifacts.Keys.Where(t => t != ArtifactTypes.Screenshots);
                    if (types.Any())
                    {
                        html.AppendLine("<h4>Artifacts</h4>");
                        html.AppendLine("<div style='margin-left: 20px; margin-right: 20px'>");
                        foreach (var type in types)
                        {
                            html.AppendLine($"<p>{type}:</p>");
                            html.AppendLine("<ol>");
                            foreach (var artifact in step.Artifacts[type])
                            {
                                string artifactUri = ConvertPath(artifact, relativePath);
                                html.AppendLine($"<li><a href='{artifactUri}' target='_blank'/>{Path.GetFileName(artifact)}</a></li>");
                            }
                            html.AppendLine("</ol>");
                        }
                        html.AppendLine("</div>");
                    }

                    // Step screenshots
                    if (step.Artifacts.ContainsKey(ArtifactTypes.Screenshots))
                    {
                        html.AppendLine($"<h4>{ArtifactTypes.Screenshots}</h4>");
                        foreach (var screenshot in step.Artifacts[ArtifactTypes.Screenshots])
                        {
                            string screenshotUri = ConvertPath(screenshot, relativePath);
                            html.AppendLine($"<div><a href='{screenshotUri}' target='_blank'><img src='{screenshotUri}' style='border: 1px solid #555; max-width:{maxScreenshotWidth}px'/></a></div>");
                        }
                    }

                    html.AppendLine("</div>");
                    html.AppendLine("</td></tr>");
                }

                html.AppendLine("</table>");
            }

            // Append HTML doc closer
            html.AppendLine("</div>");
            html.AppendLine($"<hr />");
            html.AppendLine("</div>");
            html.AppendLine("</body>");
            html.AppendLine("</html>");

            // Get the path for the file
            string path = GetDumpFilePath(HtmlExtension);

            // Write the file
            File.WriteAllText(path, html.ToString());

            // Return the path to the file
            return path;
        }

        #endregion
    }
}
