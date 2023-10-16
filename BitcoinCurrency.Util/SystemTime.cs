namespace BitcoinCurrency.Util
{
    public class SystemTime
    {
        public static DateTime Now => TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
    }
}
