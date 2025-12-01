// Decompiled with JetBrains decompiler
// Type: PCSYS_PPG_Info.ImportTools
// Assembly: PCSYS PPG Info, Version=1.0.6.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 1DA98507-3547-4F13-89C5-8F8721C43394
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PPG Info.dll

using Infragistics.Documents.Excel;
using PCSYS_Event_Log;
using PCSYS_PPG_LPS_ProxyConnector;
using PCSYS_PPG_LPS_ProxyConnector.DataService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

#nullable disable
namespace PCSYS_PPG_Info;

public class ImportTools
{
  public void UpDataMapper(string fileName, List<LpsDataLink> links)
  {
    byte[] buffer = File.ReadAllBytes(fileName);
    MemoryStream memoryStream = new MemoryStream();
    memoryStream.Write(buffer, 0, buffer.Length);
    Worksheet worksheet = Workbook.Load((Stream) memoryStream).Worksheets["Total list"];
    int num = worksheet.Rows.Count<WorksheetRow>() + 1;
    for (int index = 1; index < num; ++index)
    {
      if (worksheet.Rows[index].Cells[0].Value != null && !(worksheet.Rows[index].Cells[0].Value.ToString() == string.Empty))
      {
        string materialNumber = worksheet.Rows[index].Cells[0].Value != null ? worksheet.Rows[index].Cells[0].Value.ToString() : string.Empty;
        if (!(materialNumber == string.Empty))
        {
          int productInfo = new ExcelTools().GetProductInfo(materialNumber, "DK", links);
          int columnIndex1 = worksheet.GetCell("C1").ColumnIndex;
          int columnIndex2 = worksheet.GetCell("Z1").ColumnIndex;
          string labelName = worksheet.Rows[index].Cells[columnIndex2].Value != null ? worksheet.Rows[index].Cells[columnIndex2].Value.ToString() : string.Empty;
          if (labelName != "Empty" && labelName != string.Empty)
            this.UpdateMapping(productInfo, 6, labelName, links);
        }
      }
    }
  }

  private void UpdateLabelNames(string labelName, List<LpsDataLink> links)
  {
    string sqlString = $"IF NOT EXISTS (SELECT id FROM tppglabelnames WHERE labelname = '{labelName}') BEGIN INSERT INTO tppglabelnames (labelname,printable) VALUES ('{labelName}',1) SELECT SCOPE_IDENTITY() END ELSE SELECT id FROM tppglabelnames WHERE labelname = '{labelName}'";
    new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) links).ExecuteScalarString(sqlString);
  }

  private void UpdateMapping(
    int materialId,
    int labelTypeId,
    string labelName,
    List<LpsDataLink> links)
  {
    List<IBSExternalParameter> externalParameterList = new List<IBSExternalParameter>()
    {
      new IBSExternalParameter()
      {
        Name = "@labelname",
        Value = (object) labelName
      }
    };
    try
    {
      string sqlString1 = "SELECT tppgprintlabeltypes.Id FROM dbo.tppgprintlabeltypes INNER JOIN tppglabelnames ON tppgprintlabeltypes.labelnameid = tppglabelnames.id  WHERE(dbo.tppglabelnames.labelname = @labelname)";
      string str = new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) links).ExecuteScalarStringWithParameters(sqlString1, externalParameterList.ToArray());
      if (str == string.Empty)
        LogTools.WriteToEventLog($"Labelname {labelName} is missing", EventLogEntryType.Error, 0, LogTools.Operation.Plc, links);
      string sqlString2 = $"{$"IF NOT EXISTS (SELECT id FROM tppgproductmapping WHERE productid = {materialId} "}{$" AND labeltypeid = {labelTypeId} AND printlabeltypeid = {str})"} INSERT INTO tppgproductmapping (productid,labeltypeid,printlabeltypeid) VALUES ({$" {materialId},{labelTypeId},{str})"}";
      new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) links).ExecuteNonQuery(sqlString2);
    }
    catch (Exception ex)
    {
      LogTools.WriteToEventLog($"Productid {materialId}, Labelname {labelName}, LabelTypeId {labelTypeId} has an error: {ex.Message}", EventLogEntryType.Error, 0, LogTools.Operation.Plc, links);
    }
  }

  private void UpdateLabelMapping(string labelName, string templateName, List<LpsDataLink> links)
  {
    try
    {
      int num1 = 0;
      int num2 = 0;
      if (templateName != "BLANK" && templateName != "Empty" && templateName != string.Empty)
      {
        num1 = 1;
        string sqlString = $"SELECT documentid FROM tdocuments WHERE itemname LIKE '%{templateName}'";
        num2 = new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) links).ExecuteScalarInt(sqlString);
        if (num2 == -1)
        {
          LogTools.WriteToEventLog(labelName + " is missing", EventLogEntryType.Error, 0, LogTools.Operation.Plc, links);
          return;
        }
      }
      string sqlString1 = $"IF NOT EXISTS (SELECT id FROM tppglabelnames WHERE labelname = '{labelName}') BEGIN INSERT INTO tppglabelnames (labelname,printable) VALUES ('{labelName}',{$"{num1}) SELECT SCOPE_IDENTITY() END ELSE "}SELECT id FROM tppglabelnames WHERE labelname = '{labelName}'";
      string str = new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) links).ExecuteScalarString(sqlString1);
      string sqlString2 = $"IF NOT EXISTS (SELECT id FROM tppgprintlabeltypes WHERE labelnameid = {str} AND {$"documentid = {num2}) INSERT INTO tppgprintlabeltypes (labelnameid,documentid) VALUES ("}{$"{str},{num2})"}";
      new IBSExchangeData().GetConnection((IEnumerable<LpsDataLink>) links).ExecuteNonQuery(sqlString2);
    }
    catch (Exception ex)
    {
      LogTools.WriteToEventLog($"Error for labelnme {labelName} and {templateName} {ex.Message}", EventLogEntryType.Error, 0, LogTools.Operation.Plc, links);
    }
  }

  public void InsertData(string fileName, List<LpsDataLink> links)
  {
    byte[] buffer = File.ReadAllBytes(fileName);
    MemoryStream memoryStream = new MemoryStream();
    memoryStream.Write(buffer, 0, buffer.Length);
    Worksheet worksheet = Workbook.Load((Stream) memoryStream).Worksheets["Total list"];
    int num = worksheet.Rows.Count<WorksheetRow>() + 1;
    for (int index = 1; index < num; ++index)
    {
      if (worksheet.Rows[index].Cells[0].Value != null && !(worksheet.Rows[index].Cells[0].Value.ToString() == string.Empty))
      {
        string materialNumber = worksheet.Rows[index].Cells[0].Value != null ? worksheet.Rows[index].Cells[0].Value.ToString() : string.Empty;
        if (!(materialNumber == string.Empty))
        {
          int productInfo = new ExcelTools().GetProductInfo(materialNumber, "DK", links);
          int columnIndex1 = worksheet.GetCell("B1").ColumnIndex;
          string text1 = worksheet.Rows[index].Cells[columnIndex1].Value != null ? worksheet.Rows[index].Cells[columnIndex1].Value.ToString() : string.Empty;
          new ExcelTools().UpdateValue(productInfo, 13, text1, links);
          int columnIndex2 = worksheet.GetCell("G1").ColumnIndex;
          string text2 = worksheet.Rows[index].Cells[columnIndex2].Value != null ? worksheet.Rows[index].Cells[columnIndex2].Value.ToString() : string.Empty;
          new ExcelTools().UpdateValue(productInfo, 14, text2, links);
          int columnIndex3 = worksheet.GetCell("I1").ColumnIndex;
          string text3 = worksheet.Rows[index].Cells[columnIndex3].Value != null ? worksheet.Rows[index].Cells[columnIndex3].Value.ToString() : string.Empty;
          new ExcelTools().UpdateValue(productInfo, 1, text3, links);
          int columnIndex4 = worksheet.GetCell("T1").ColumnIndex;
          string text4 = worksheet.Rows[index].Cells[columnIndex4].Value != null ? worksheet.Rows[index].Cells[columnIndex4].Value.ToString() : string.Empty;
          new ExcelTools().UpdateValue(productInfo, 3, text4, links);
          int columnIndex5 = worksheet.GetCell("U1").ColumnIndex;
          string text5 = worksheet.Rows[index].Cells[columnIndex5].Value != null ? worksheet.Rows[index].Cells[columnIndex5].Value.ToString() : string.Empty;
          new ExcelTools().UpdateValue(productInfo, 6, text5, links);
          int columnIndex6 = worksheet.GetCell("AK1").ColumnIndex;
          string text6 = worksheet.Rows[index].Cells[columnIndex6].Value != null ? worksheet.Rows[index].Cells[columnIndex6].Value.ToString() : string.Empty;
          new ExcelTools().UpdateValue(productInfo, 15, text6, links);
          int columnIndex7 = worksheet.GetCell("Y1").ColumnIndex;
          string text7 = worksheet.Rows[index].Cells[columnIndex7].Value != null ? worksheet.Rows[index].Cells[columnIndex7].Value.ToString() : string.Empty;
          new ExcelTools().UpdateValue(productInfo, 18, text7, links);
          int columnIndex8 = worksheet.GetCell("X1").ColumnIndex;
          string text8 = worksheet.Rows[index].Cells[columnIndex8].Value != null ? worksheet.Rows[index].Cells[columnIndex8].Value.ToString() : string.Empty;
          new ExcelTools().UpdateValue(productInfo, 2, text8, links);
          int columnIndex9 = worksheet.GetCell("AE1").ColumnIndex;
          string text9 = worksheet.Rows[index].Cells[columnIndex9].Value != null ? worksheet.Rows[index].Cells[columnIndex9].Value.ToString() : string.Empty;
          new ExcelTools().UpdateValue(productInfo, 4, text9, links);
          int columnIndex10 = worksheet.GetCell("AL1").ColumnIndex;
          string text10 = worksheet.Rows[index].Cells[columnIndex10].Value != null ? worksheet.Rows[index].Cells[columnIndex10].Value.ToString() : string.Empty;
          new ExcelTools().UpdateValue(productInfo, 16 /*0x10*/, text10, links);
        }
      }
    }
  }
}
