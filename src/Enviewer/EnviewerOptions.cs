namespace Enviewer
{
    /// <summary>
    /// The option of Enviewer.
    /// </summary>
    public class EnviewerOptions
    {
        private static readonly string DEFAULT_ROUTE = "/enviewer";

        /// <summary>
        /// The constructor of Enviewer.
        /// </summary>
        public EnviewerOptions()
        {
        }

        /// <summary>
        /// Sets a custom route for the Enviewer endpoint(s).
        /// </summary>
        public string Route
        {
            get { return route; }
            set { route = string.IsNullOrWhiteSpace(value) ? DEFAULT_ROUTE : value; }
        }
        private string route = DEFAULT_ROUTE;
    }
}
