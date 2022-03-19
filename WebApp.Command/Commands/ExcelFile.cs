using ClosedXML.Excel;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Command.Commands
{
    /// <summary>
    /// Diyagrama baktığımız zaman Receiver kısmına yani asıl işi yapıcak olan kısım, bizim ExcelFile'ımız ve PdfFile'ımız
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExcelFile<T>
    {
        public readonly List<T> _list;
        public string FileName => $"{typeof(T).Name}.xlsx";

        //Download edilecek dosyayının tipi:
        public string FileType => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        public ExcelFile(List<T> list)
        {
            _list = list;
        }
        

        //Excel dosyasını memoryde bir byte array olarak tutuyoruz.
        public MemoryStream Create()
        {
            var workbook = new XLWorkbook();

            //Datatable ı tutan veritabanı gibi düşünebiliriz..
            var dataSet = new DataSet();
            dataSet.Tables.Add(GetTable());

            workbook.Worksheets.Add(dataSet);

            //Memory oluşturuyoruz
            var excelMemory = new MemoryStream();
            workbook.SaveAs(excelMemory);
            return excelMemory;
        }

        //XML'e vereceğimiz DataTable ı oluşturuyoruz. 
        private DataTable GetTable()
        {
            //Memoryde tablo oluşturuyoruz.
            var table = new DataTable();
            
            var type = typeof(T);
            
            type.GetProperties().ToList().ForEach(x => { table.Columns.Add(x.Name, x.PropertyType); });
            //Yukarıdaki tabloyu dolduruyoruz
            _list.ForEach(x =>
            {
                //Dinamik olarak T den gelen değerleri almamız gerekiyor.
                //Dinamik olarak verdiği için object olarak alıyoruz.
                var values = type.GetProperties().Select(propertyInfo => propertyInfo.GetValue(x, null)).ToArray();
                
                table.Rows.Add(values);
            });
            return table;

        }

    }
}
