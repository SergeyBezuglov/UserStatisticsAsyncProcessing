namespace PIMS.Web
{
    /// <summary>
    /// ������� ������.
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// ����
        /// </summary>
        /// <value>������ ����.</value>
        public DateOnly Date { get; set; }

        /// <summary>
        /// ����������� �� ����� �������.
        /// </summary>
        /// <value>An int.</value>
        public int TemperatureC { get; set; }

        /// <summary>
        /// ����������� �� ����� ����������.
        /// </summary>
        /// <value>An int.</value>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// ������.
        /// </summary>
        /// <value>������? .</value>
        public string? Summary { get; set; }
    }
}