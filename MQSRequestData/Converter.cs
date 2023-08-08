using HtmlAgilityPack;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace MQSRequestData
{
    class ConverterHtmlToDt
    {

        public void htmlToDT(string htmlCode)
        {
            try
            {
                ApkMQS apk = new ApkMQS();
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(htmlCode);
                var headers = doc.DocumentNode.SelectNodes("//tr/th");
                DataTable table = new DataTable();
                foreach (HtmlNode header in headers)
                {
                    table.Columns.Add(header.InnerText);
                }

                foreach (var row in doc.DocumentNode.SelectNodes("//tr[td]"))
                {
                    if (!table.Rows.Contains("***Total***"))
                        table.Rows.Add(row.SelectNodes("td").Select(td => td.InnerText).ToArray());
                }


                StringBuilder sb = new StringBuilder();

                string[] columnNames = table.Columns.Cast<DataColumn>().
                                                  Select(column => column.ColumnName).
                                                  ToArray();
                sb.AppendLine(string.Join(",", columnNames));

                foreach (DataRow row in table.Rows)
                {
                    string[] fields = row.ItemArray.Select(field => field.ToString()).
                                                    ToArray();
                    sb.AppendLine(string.Join(",", fields));
                }

                File.WriteAllText(apk.textBoxSave.Text + @"\DailyMQSData.csv", sb.ToString());
            }
            catch { }



        }

    }
}
