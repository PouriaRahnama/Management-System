namespace Management_System.ConfigApp
{
    public static class Extensions
    {
        public static string ToPersian(this DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(date) + "/" + pc.GetMonth(date) + "/" + pc.GetDayOfMonth(date);
        }

        public static string SubStringCustom(this string text, int length)
        {
            if (text.Length > length)
            {
                return text.Substring(0, length - 3) + "...";
            }

            return text;
        }

        public static string ConvertHtmlToText(this string text)
        {
            return Regex.Replace(text, "<.*?>", " ")
                .Replace("&zwnj;", " ")
                .Replace(";&zwnj", " ")
                .Replace("&nbsp;", " ");
        }
    }
}
