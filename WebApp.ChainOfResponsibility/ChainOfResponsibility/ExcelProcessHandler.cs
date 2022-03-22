using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace WebApp.ChainOfResponsibility.ChainOfResponsibility
{
    public class ExcelProcessHandler<T> :ProcessHandler
    {
        private DataTable GetTable(Object o)
        {
            var table = new DataTable();

            var type = typeof(T);

            type.GetProperties().ToList().ForEach(p => table.Columns.Add(p.Name,p.PropertyType));

            var list = o as List<T>; //Tipini belirledik.

            list.ForEach(x =>
            {
                var values = type.GetProperties().Select(propertyInfo => propertyInfo.GetValue(x, null)).ToArray();

                table.Rows.Add(values);

            });
            return table;
        }
        public override object handle(object o)
        {
            //Asıl İşlemi Yapan Metod

            var workbook = new XLWorkbook();
            var ds = new DataSet();
            ds.Tables.Add(GetTable(o));

            workbook.Worksheets.Add(ds);

            var excelMemoryStream = new MemoryStream();

            workbook.SaveAs(excelMemoryStream);

            return base.handle(excelMemoryStream); //Object olduğu için istediğimiz datayı verebildik.
        }
    }
}
