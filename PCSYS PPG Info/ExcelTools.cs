// Decompiled with JetBrains decompiler
// Type: PCSYS_PPG_Info.ExcelTools
// Assembly: PCSYS PPG Info, Version=1.0.6.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 1DA98507-3547-4F13-89C5-8F8721C43394
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PPG Info.dll

using Infragistics.Documents.Excel;
using PCSYS_PPG_LPS_ProxyConnector;
using PCSYS_PPG_LPS_ProxyConnector.DataService;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

#nullable disable
namespace PCSYS_PPG_Info;

public class ExcelTools
{
  public byte[] ExportExcel(List<LpsDataLink> links, List<int> itemIds)
  {
    Workbook workbook = new Workbook();
    Worksheet worksheet = workbook.Worksheets.Add("Product Data");
    DataTable table1 = new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) links).GetData("SELECT name,datatype,isfullpalletfield  FROM tppgfieldnames ORDER BY columnindex").Tables[0];
    worksheet.DisplayOptions.FrozenPaneSettings.FrozenRows = 1;
    worksheet.Rows[0].Cells[0].Value = (object) $"PCSYS Product data. Created {DateTime.Now:yy-MM-dd}";
    worksheet.Columns[0].Width = 5000;
    worksheet.Columns[1].Width = 3000;
    worksheet.Columns[2].Width = 5000;
    int index1 = 1;
    worksheet.Rows[index1].Cells[0].Value = (object) "Material number";
    worksheet.Rows[index1].Cells[1].Value = (object) "Language";
    int index2 = 2;
    foreach (DataRow row in (InternalDataCollectionBase) table1.Rows)
    {
      worksheet.Rows[index1].Cells[index2].Value = (object) row["name"].ToString();
      ++index2;
    }
    string str1 = string.Empty;
    string str2 = string.Empty;
    if (itemIds != null)
    {
      foreach (int itemId in itemIds)
        str1 += $"{itemId},";
      if (str1.EndsWith(","))
        str1 = str1.Substring(0, str1.Length - 1);
      str2 = $" WHERE tppgproducts.id IN ({str1})";
    }
    DataTable table2 = new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) links).GetData("SELECT tppgproducts.id,tppgproducts.materialnumber, tppglanguages.languagecode  FROM tppgproducts INNER JOIN tppglanguages ON tppgproducts.languageid = tppglanguages.id " + str2).Tables[0];
    int index3 = index1 + 1;
    foreach (DataRow row1 in (InternalDataCollectionBase) table2.Rows)
    {
      int index4 = 0;
      worksheet.Rows[index3].Cells[index4].Value = (object) row1["materialnumber"].ToString();
      int index5 = index4 + 1;
      worksheet.Rows[index3].Cells[index5].Value = (object) row1["languagecode"].ToString();
      foreach (DataRow row2 in (InternalDataCollectionBase) this.GetRowValues(Convert.ToInt64(row1["materialnumber"].ToString()), row1["languagecode"].ToString(), links).Rows)
      {
        for (int index6 = 2; index6 < table1.Rows.Count + 2; ++index6)
        {
          if (worksheet.Rows[1].Cells[index6].Value.ToString() == row2["name"].ToString())
            worksheet.Rows[index3].Cells[index6].Value = (object) row2["text"].ToString();
        }
      }
      ++index3;
    }
    Stream stream = (Stream) new MemoryStream();
    workbook.Save(stream);
    return ((MemoryStream) stream).ToArray();
  }

  public Dictionary<string, string> GetProductData(
    long materialNumber,
    string language,
    List<LpsDataLink> links)
  {
    return this.GetRowValues(materialNumber, language, links).Rows.Cast<DataRow>().ToDictionary<DataRow, string, string>((System.Func<DataRow, string>) (row => row["name"].ToString()), (System.Func<DataRow, string>) (row => row["text"].ToString()));
  }

  private DataTable GetRowValues(long materialNumber, string language, List<LpsDataLink> links)
  {
    string sqlString = "SELECT tppgproductdata.text, tppgfieldnames.name, tppgfieldnames.isfullpalletfield  FROM tppgproducts INNER JOIN tppgproductdata ON tppgproducts.id = tppgproductdata.productid INNER JOIN tppgfieldnames ON tppgproductdata.columnid = tppgfieldnames.id INNER JOIN tppglanguages ON tppgproducts.languageid = tppglanguages.id WHERE materialnumber = @materialnumber  AND languagecode = @languagecode";
    List<IBSExternalParameter> externalParameterList = new List<IBSExternalParameter>()
    {
      new IBSExternalParameter()
      {
        Name = "@materialnumber",
        Value = (object) materialNumber
      },
      new IBSExternalParameter()
      {
        Name = "@languagecode",
        Value = (object) language
      }
    };
    return new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) links).GetDataWithParameters(sqlString, externalParameterList.ToArray()).Tables[0];
  }

  public string GetRowValueByFieldNameId(
    long? materialNumber,
    string language,
    int fieldNameId,
    List<LpsDataLink> links)
  {
    string sqlString = "SELECT tppgproductdata.text FROM tppgproducts INNER JOIN tppgproductdata ON tppgproducts.id = tppgproductdata.productid INNER JOIN tppgfieldnames ON tppgproductdata.columnid = tppgfieldnames.id INNER JOIN tppglanguages ON tppgproducts.languageid = tppglanguages.id WHERE materialnumber = @materialnumber " + $" AND languagecode = @languagecode AND tppgfieldnames.id  = {fieldNameId}";
    List<IBSExternalParameter> externalParameterList = new List<IBSExternalParameter>()
    {
      new IBSExternalParameter()
      {
        Name = "@materialnumber",
        Value = (object) materialNumber
      },
      new IBSExternalParameter()
      {
        Name = "@languagecode",
        Value = (object) language
      }
    };
    return new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) links).ExecuteScalarStringWithParameters(sqlString, externalParameterList.ToArray());
  }

  public List<ExcelTools.ExcelImportResult> ImportExcel(byte[] excel, List<LpsDataLink> links)
  {
    List<ExcelTools.ExcelImportResult> excelImportResultList = new List<ExcelTools.ExcelImportResult>();
    MemoryStream memoryStream = new MemoryStream();
    memoryStream.Write(excel, 0, excel.Length);
    Worksheet worksheet = Workbook.Load((Stream) memoryStream).Worksheets["Product Data"];
    int num1 = worksheet.Rows.Count<WorksheetRow>() + 1;
    int num2 = 20 + 1;
    for (int index1 = 2; index1 < num1; ++index1)
    {
      if (worksheet.Rows[index1].Cells[0].Value != null && !(worksheet.Rows[index1].Cells[0].Value.ToString() == string.Empty))
      {
        string materialNumber = worksheet.Rows[index1].Cells[0].Value != null ? worksheet.Rows[index1].Cells[0].Value.ToString() : string.Empty;
        if (!(materialNumber == string.Empty))
        {
          string langaugeCode = worksheet.Rows[index1].Cells[1].Value == null || !(worksheet.Rows[index1].Cells[1].Value.ToString() != string.Empty) ? "DK" : worksheet.Rows[index1].Cells[1].Value.ToString();
          try
          {
            int productInfo = this.GetProductInfo(materialNumber, langaugeCode, links);
            for (int index2 = 2; index2 < num2; ++index2)
            {
              if (worksheet.Rows[1].Cells[index2].Value != null)
              {
                string columnName = worksheet.Rows[1].Cells[index2].Value.ToString();
                int fieldNameId = this.GetFieldNameId(columnName, links);
                if (fieldNameId < 0)
                {
                  excelImportResultList.Add(new ExcelTools.ExcelImportResult()
                  {
                    MaterialNumber = worksheet.Rows[index1].Cells[0].Value.ToString(),
                    Language = worksheet.Rows[index1].Cells[1].Value != null ? worksheet.Rows[index1].Cells[1].Value.ToString() : string.Empty,
                    Error = $"Column name {columnName} not found"
                  });
                }
                else
                {
                  string text = worksheet.Rows[index1].Cells[index2].Value != null ? worksheet.Rows[index1].Cells[index2].Value.ToString() : string.Empty;
                  this.UpdateValue(productInfo, fieldNameId, text, links);
                }
              }
            }
          }
          catch (Exception ex)
          {
            excelImportResultList.Add(new ExcelTools.ExcelImportResult()
            {
              MaterialNumber = worksheet.Rows[index1].Cells[0].Value.ToString(),
              Language = worksheet.Rows[index1].Cells[1].Value.ToString(),
              Error = ex.Message
            });
          }
        }
      }
    }
    return excelImportResultList;
  }

  public void UpdateValue(
    int materialNumberId,
    int columnId,
    string text,
    List<LpsDataLink> links)
  {
    if (text == string.Empty)
    {
      new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) links).ExecuteNonQuery($"DELETE FROM tppgproductdata WHERE productid = {materialNumberId} AND columnid = {columnId}");
    }
    else
    {
      List<IBSExternalParameter> externalParameterList = new List<IBSExternalParameter>()
      {
        new IBSExternalParameter()
        {
          Name = "@text",
          Value = (object) text
        }
      };
      string sqlString = $"IF EXISTS (SELECT id FROM tppgproductdata WHERE productid = {materialNumberId} AND columnid = {columnId}) " + $"UPDATE tppgproductdata SET text = @text WHERE productid = {materialNumberId} AND columnid = {columnId} ELSE " + $" INSERT INTO tppgproductdata (productid,columnid,text) VALUES ({materialNumberId},{columnId},@text)";
      new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) links).ExecuteNonQueryWithParameters(sqlString, externalParameterList.ToArray());
    }
  }

  private int GetFieldNameId(string columnName, List<LpsDataLink> links)
  {
    try
    {
      List<IBSExternalParameter> externalParameterList = new List<IBSExternalParameter>()
      {
        new IBSExternalParameter()
        {
          Name = "@name",
          Value = (object) columnName
        }
      };
      string str = new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) links).ExecuteScalarStringWithParameters("SELECT id FROM tppgfieldnames WHERE name = @name", externalParameterList.ToArray());
      return str == string.Empty ? -1 : Convert.ToInt32(str);
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message, ex);
    }
  }

  public int GetProductInfo(string materialNumber, string langaugeCode, List<LpsDataLink> links)
  {
    try
    {
      string sqlString = "IF EXISTS (SELECT id FROM tppgproducts WHERE materialnumber = @materialnumber AND languageid = (SELECT id FROM tppglanguages WHERE languagecode = @languagecode)) SELECT id FROM tppgproducts WHERE materialnumber = @materialnumber AND languageid = (SELECT id FROM tppglanguages WHERE languagecode = @languagecode) ELSE BEGIN INSERT INTO tppgproducts (materialnumber,languageid) VALUES (@materialnumber, (SELECT id FROM tppglanguages WHERE languagecode = @languagecode)) SELECT SCOPE_IDENTITY() END";
      List<IBSExternalParameter> externalParameterList = new List<IBSExternalParameter>()
      {
        new IBSExternalParameter()
        {
          Name = "@materialnumber",
          Value = (object) materialNumber
        },
        new IBSExternalParameter()
        {
          Name = "@languagecode",
          Value = (object) langaugeCode
        }
      };
      return Convert.ToInt32(new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) links).ExecuteScalarStringWithParameters(sqlString, externalParameterList.ToArray()));
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message, ex);
    }
  }

  public class ExcelImportResult
  {
    public string MaterialNumber { get; set; }

    public string Language { get; set; }

    public string Error { get; set; }
  }
}
