﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Globalization;
using Revival.Common;
using ExcelOutputAttribute = Hellgate.ExcelFile.OutputAttribute;

namespace Hellgate
{
    public class TableDataSet : IDisposable
    {
        public DataSet XlsDataSet { get; private set; }
        public bool RegenerateRelations { get; private set; }
        private bool _excelDataTableLoaded;

        public TableDataSet()
        {
            RegenerateRelations = false;
            _excelDataTableLoaded = false;
            XlsDataSet = new DataSet("xlsDataSet")
            {
                Locale = new CultureInfo("en-us", true),
                RemotingFormat = SerializationFormat.Binary
            };
        }

        public void ClearDataSet()
        {
            XlsDataSet.Clear();
            XlsDataSet.Relations.Clear();
            XlsDataSet.Tables.Clear();
        }

        public DataTable LoadTable(object var, bool genRelations)
        {
            DataFile dataFile = var as DataFile;
            if (dataFile == null) return null;

            if (!_excelDataTableLoaded)
            {
                _excelDataTableLoaded = true; // need to set this before _LoadRelatedTable else stack overflow
                DataTable excelDataTable = _LoadRelatedTable("EXCELTABLES");
                if (excelDataTable == null) _excelDataTableLoaded = false;
            }

            DataTable dataTable = null;
            if (dataFile.IsStringsFile)
            {
                dataTable = _LoadStringsTable(dataFile as StringsFile);
            }
            else if (dataFile.IsExcelFile)
            {
                dataTable = _LoadExcelTable(dataFile as ExcelFile, genRelations);
            }

            return dataTable;
        }

        private DataTable _LoadExcelTable(DataFile dataFile, bool genRelations)
        {
            // load in main data table
            String mainTableName = dataFile.StringId;
            DataTable mainDataTable = XlsDataSet.Tables[mainTableName];
            if (mainDataTable != null) return mainDataTable;


            mainDataTable = XlsDataSet.Tables.Add(dataFile.StringId);
            mainDataTable.TableName = mainTableName;

            List<Object> array = dataFile.Rows;
            Type type = array[0].GetType();
            List<ExcelOutputAttribute> outputAttributes =
                new List<ExcelOutputAttribute>(type.GetFields().Length + 1);

            #region generate_columns

            DataColumn indexColumn = mainDataTable.Columns.Add("Index");
            indexColumn.AutoIncrement = true;
            indexColumn.Unique = true;
            mainDataTable.PrimaryKey = new[] { indexColumn };
            outputAttributes.Add(null);

            foreach (FieldInfo fieldInfo in type.GetFields())
            {
                ExcelOutputAttribute excelOutputAttribute = ExcelFile.GetExcelOutputAttribute(fieldInfo);

                if (excelOutputAttribute == null)
                {
                    outputAttributes.Add(null);
                    mainDataTable.Columns.Add(fieldInfo.Name, fieldInfo.FieldType);
                    continue;
                }

                outputAttributes.Add(excelOutputAttribute);


                if (excelOutputAttribute.IsStringOffset)
                {
                    DataColumn dataColumn = mainDataTable.Columns.Add(fieldInfo.Name, typeof(String));
                    dataColumn.ExtendedProperties.Add(ExcelFile.ColumnTypeKeys.IsStringOffset, true);
                    dataColumn.DefaultValue = String.Empty;
                    if (excelOutputAttribute.SortAscendingID > 0)
                    {
                        dataColumn.ExtendedProperties.Add(ExcelFile.ColumnTypeKeys.SortAscendingID, excelOutputAttribute.SortAscendingID);
                    }
                }
                else if (excelOutputAttribute.IsIntOffset)
                {
                    DataColumn dataColumn = mainDataTable.Columns.Add(fieldInfo.Name, typeof(String));
                    dataColumn.ExtendedProperties.Add(ExcelFile.ColumnTypeKeys.IsIntOffset, true);
                    dataColumn.DefaultValue = String.Empty;
                }
                else
                {
                    // Sorting Keys
                    DataColumn dataColumn = mainDataTable.Columns.Add(fieldInfo.Name, fieldInfo.FieldType);

                    if (excelOutputAttribute.RequiresDefault)
                    {
                        dataColumn.ExtendedProperties.Add(ExcelFile.ColumnTypeKeys.RequiresDefault, true);
                    }
                    if (excelOutputAttribute.SortAscendingID > 0)
                    {
                        dataColumn.ExtendedProperties.Add(ExcelFile.ColumnTypeKeys.SortAscendingID, excelOutputAttribute.SortAscendingID);
                    }
                    if (excelOutputAttribute.SortDistinctID > 0)
                    {
                        dataColumn.ExtendedProperties.Add(ExcelFile.ColumnTypeKeys.SortDistinctID, excelOutputAttribute.SortDistinctID);
                    }
                    if (excelOutputAttribute.SortPostOrderID > 0)
                    {
                        dataColumn.ExtendedProperties.Add(ExcelFile.ColumnTypeKeys.SortPostOrderID, excelOutputAttribute.SortPostOrderID);
                    }
                    if (!(String.IsNullOrEmpty(excelOutputAttribute.SortColumnTwo)))
                    {
                        dataColumn.ExtendedProperties.Add(ExcelFile.ColumnTypeKeys.SortColumnTwo, excelOutputAttribute.SortColumnTwo);
                    }
                    if (excelOutputAttribute.ExcludeZero == true)
                    {
                        dataColumn.ExtendedProperties.Add(ExcelFile.ColumnTypeKeys.ExcludeZero, true);
                    }

                    // Other types
                    if (excelOutputAttribute.IsStringIndex)
                    {
                        dataColumn.ExtendedProperties.Add(ExcelFile.ColumnTypeKeys.IsStringIndex, true);
                        outputAttributes.Add(null);
                        DataColumn dataColumnString = mainDataTable.Columns.Add(fieldInfo.Name + "_string",
                                                                                typeof(String));
                        dataColumnString.DefaultValue = String.Empty;
                    }
                    else if (excelOutputAttribute.IsStringID)
                    {
                        dataColumn.ExtendedProperties.Add(ExcelFile.ColumnTypeKeys.IsStringId, true);
                    }
                    else if (excelOutputAttribute.IsBitmask)
                    {
                        dataColumn.ExtendedProperties.Add(ExcelFile.ColumnTypeKeys.IsBitmask, true);
                    }
                    else if (excelOutputAttribute.IsBool)
                    {
                        dataColumn.ExtendedProperties.Add(ExcelFile.ColumnTypeKeys.IsBool, true);
                    }
                }
            }

            if (((ExcelFile)dataFile).AiData != null) // items, missiles, monsters, objects, players
            {
                DataColumn indiceDataCol = mainDataTable.Columns.Add("AiData");
                indiceDataCol.DataType = typeof(string);
                indiceDataCol.ExtendedProperties.Add(ExcelFile.ColumnTypeKeys.IsIndiceData, true);
                outputAttributes.Add(null);
            }

            if (((ExcelFile)dataFile).FinalData != null) // items, missiles, monsters, objects, players
            {
                DataColumn finalDataCol = mainDataTable.Columns.Add("FinalData");
                finalDataCol.DataType = typeof(string);
                finalDataCol.ExtendedProperties.Add(ExcelFile.ColumnTypeKeys.IsFinalData, true);
                outputAttributes.Add(null);
            }

            #endregion

            const int updateRate = 50;


            #region generate_rows

            int row = 1;
            object[] baseRow = new object[outputAttributes.Count];

            foreach (Object table in array)
            {
                int col = 1;

                foreach (FieldInfo fieldInfo in type.GetFields())
                {
                    Object value = fieldInfo.GetValue(table);
                    ExcelOutputAttribute excelOutputAttribute = outputAttributes[col];

                    /*
                    if (fieldInfo.Name == "code")
                    {
                        int val = 0;
                        if (fieldInfo.FieldType == typeof(Int16))
                        {
                            Int16 asdf = (Int16) value;
                            val = asdf;
                        }   
                        else
                        {
                            val = (Int32) value;
                        }
                        if (val == 26641 || val == 29201)
                        {
                            int bp = 0;
                        }
                    }*/

                    if (excelOutputAttribute == null)
                    {
                        baseRow[col++] = value;
                        continue;
                    }

                    if (excelOutputAttribute.IsStringOffset)
                    {
                        int valueInt = (int)value;

                        baseRow[col++] = dataFile.Strings[valueInt];
                    }
                    else if (excelOutputAttribute.IsStringIndex)
                    {
                        int valueInt = (int)value;
                        String stringValue = valueInt == -1 ? String.Empty : dataFile.SecondaryStrings[valueInt];

                        baseRow[col++] = value;
                        baseRow[col++] = stringValue;
                    }
                    else if (excelOutputAttribute.IsIntOffset)
                    {
                        int valueInt = (int)value;

                        if (valueInt > 0)
                        {
                            baseRow[col++] = dataFile.ParseIntOffset(valueInt);
                        }
                        else
                        {
                            baseRow[col++] = 0;
                        }
                    }
                    else
                    {
                        baseRow[col++] = value;
                    }
                }

                if (((ExcelFile)dataFile).AiData != null) // items, missiles, monsters, objects, players
                {
                    int i = row - 1;
                    baseRow[col++] = Export.ArrayToCSV(((ExcelFile)dataFile).AiData[i], "hex", sizeof(byte));
                }

                if (((ExcelFile)dataFile).FinalData != null) // unit types and states
                {
                    int i = row - 1;
                    baseRow[col++] = Export.ArrayToCSV(((ExcelFile)dataFile).FinalData[i], "hex", sizeof(int));
                }

                mainDataTable.Rows.Add(baseRow);
                row++;
            }

            #endregion

            if (genRelations)
            {
                _GenerateRelations(dataFile);
            }

            return mainDataTable;
        }

        private DataTable _LoadStringsTable(StringsFile stringsFile)
        {
            // if already done, can't do again
            DataTable dataTable = XlsDataSet.Tables[stringsFile.StringId];
            if (XlsDataSet.Tables.Contains(stringsFile.StringId)) return dataTable;

            // generate datatable
            DataTable dt = XlsDataSet.Tables.Add(stringsFile.StringId);
            foreach (StringsFile.StringBlock stringsBlock in stringsFile.StringsTable)
            {
                if (dt.Columns.Count == 0)
                {
                    dt.Columns.Add("ReferenceId", stringsBlock.ReferenceId.GetType());
                    dt.Columns.Add("Unknown1", stringsBlock.Unknown.GetType());
                    dt.Columns.Add("StringId", stringsBlock.StringId.GetType());
                    dt.Columns.Add("Unknown2", stringsBlock.Reserved.GetType());
                    dt.Columns.Add("String", stringsBlock.String.GetType());
                    dt.Columns.Add("Attribute1", stringsBlock.Attribute1.GetType());
                    dt.Columns.Add("Attribute2", stringsBlock.Attribute2.GetType());
                    dt.Columns.Add("Attribute3", stringsBlock.Attribute3.GetType());
                    dt.Columns.Add("Attribute4", stringsBlock.Attribute3.GetType());
                }

                dt.Rows.Add(stringsBlock.ReferenceId, stringsBlock.Unknown, stringsBlock.StringId, stringsBlock.Reserved,
                    stringsBlock.String, stringsBlock.Attribute1, stringsBlock.Attribute2, stringsBlock.Attribute3, stringsBlock.Attribute4);
            }

            return dt;
        }

        private DataTable _LoadRelatedTable(String tableId)
        {
            if (String.IsNullOrEmpty(tableId)) return null;

            DataTable dataTable = XlsDataSet.Tables[tableId];
            if (dataTable != null) return dataTable;

            DataFile dataFile = TableFiles.DataFiles[tableId] as DataFile;
            return dataFile == null ? null : LoadTable(progress, dataFile);
        }

        private void _GenerateRelations(DataFile dataFile)
        {
            // StringsFile doesn't have/need relations as yet, no point going further with them
            ExcelFile excelTable = dataFile as ExcelFile;
            if (excelTable == null) return;

            List<Object> array = dataFile.Rows;
            Type type = array[0].GetType();
            int col;

            String mainTableName = excelTable.StringId;
            DataTable mainDataTable = XlsDataSet.Tables[mainTableName];

            // remove all extra generated columns on this table
            for (col = 0; col < mainDataTable.Columns.Count; col++)
            {
                DataColumn dc = mainDataTable.Columns[col];

                if (!dc.ExtendedProperties.Contains(ExcelFile.ColumnTypeKeys.IsRelationGenerated)) continue;

                if ((bool)dc.ExtendedProperties[ExcelFile.ColumnTypeKeys.IsRelationGenerated])
                {
                    mainDataTable.Columns.Remove(dc);
                }
            }

            col = 1;
            // regenerate relations
            foreach (FieldInfo fieldInfo in type.GetFields())
            {
                ExcelFile.ExcelOutputAttribute excelOutputAttribute = null;

                foreach (Attribute attribute in fieldInfo.GetCustomAttributes(typeof(ExcelFile.ExcelOutputAttribute), true))
                {
                    excelOutputAttribute = attribute as ExcelFile.ExcelOutputAttribute;
                    if (excelOutputAttribute != null)
                    {
                        break;
                    }
                }

                if (excelOutputAttribute != null)
                {
                    DataColumn dcChild = mainDataTable.Columns[col];

                    if (excelOutputAttribute.IsStringID)
                    {
                        DataTable dtStrings = XlsDataSet.Tables[excelOutputAttribute.TableStringId] ??
                                              _LoadRelatedTable(progress, excelOutputAttribute.TableStringId);

                        if (dtStrings != null)
                        {
                            DataColumn dcParent = dtStrings.Columns["ReferenceId"];

                            String relationName = String.Format("{0}_{1}_{2}", excelTable.StringId, dcChild.ColumnName, ExcelFile.ColumnTypeKeys.IsStringId);
                            // parent and child are swapped here for StringId relations
                            DataRelation relation = new DataRelation(relationName, dcChild, dcParent, false);
                            XlsDataSet.Relations.Add(relation);

                            DataColumn dcString = mainDataTable.Columns.Add(dcChild.ColumnName + "_string", typeof(String));
                            dcString.SetOrdinal(col + 1);
                            dcString.ExtendedProperties.Add(ExcelFile.ColumnTypeKeys.IsRelationGenerated, true);
                            // need to use MIN (and thus Child) for cases of strings with same reference id (e.g. Items; SINGULAR and PLURAL, etc)
                            dcString.Expression = "MIN(Child(" + relationName + ").String)";
                            col++;
                        }
                    }
                    else if (!String.IsNullOrEmpty(excelOutputAttribute.TableStringId) || excelOutputAttribute.TableIndex >= 0)
                    {
                        String tableStringId = excelOutputAttribute.TableStringId;
                        if (excelOutputAttribute.TableIndex >= 0)
                        {
                            tableStringId = GetExcelTableStringIdFromIndex(excelOutputAttribute.TableIndex);
                        }

                        DataTable dt = XlsDataSet.Tables[tableStringId] ?? _LoadRelatedTable(progress, tableStringId);

                        if (dt != null)
                        {
                            DataColumn dcParent = dt.Columns["Index"];

                            // if no column name set, assume first column (n.b. not index=0 as that's the "Index" column)
                            if (String.IsNullOrEmpty(excelOutputAttribute.Column))
                            {
                                excelOutputAttribute.Column = dt.Columns[1].ColumnName;
                            }

                            String relationName = excelTable.StringId + dcChild.ColumnName + ExcelFile.ColumnTypeKeys.IsTableIndex;
                            DataRelation relation = new DataRelation(relationName, dcParent, dcChild, false);
                            XlsDataSet.Relations.Add(relation);

                            DataColumn dcString = mainDataTable.Columns.Add(dcChild.ColumnName + "_string", typeof(String), String.Format("Parent({0}).{1}", relationName, excelOutputAttribute.Column));
                            dcString.SetOrdinal(col + 1);
                            dcString.ExtendedProperties.Add(ExcelFile.ColumnTypeKeys.IsRelationGenerated, true);
                            col++;
                        }
                    }
                }

                col++;
            }
        }

        /// <summary>
        /// Gets an excel DataTable from the DataSet.<br />
        /// If it has not been generated, it will be generated and stored.
        /// </summary>
        /// <param name="code">The excel table code (see EXCELTABLE "Code" column).</param>
        /// <returns>The excel as a DataTable or null if not valid Code.</returns>
        public DataTable GetExcelTableFromCode(UInt32 code)
        {
            String stringId = GetExcelTableStringIdFromCode(code);
            return GetExcelTableFromStringId(stringId);
        }

        /// <summary>
        /// Gets an excel DataTable from the DataSet.<br />
        /// If it has not been generated, it will be generated and stored.
        /// </summary>
        /// <param name="stringId">The excel table string name (see EXCELTABLE "StringId" column).</param>
        /// <returns>The excel as a DataTable or null if not valid StringId.</returns>
        public DataTable GetExcelTableFromStringId(String stringId)
        {
            if (String.IsNullOrEmpty(stringId)) return null;
            return XlsDataSet.Tables[stringId] ?? _LoadRelatedTable(null, stringId);
        }

        public String GetExcelTableStringIdFromCode(UInt32 code)
        {
            DataTable excelTables = GetExcelTableFromStringId("EXCELTABLES");
            if (excelTables == null) return null;

            DataRow[] rows = excelTables.Select(String.Format("code = '{0}'", code));
            return rows.Length == 0 ? null : rows[0][1].ToString();
        }

        public String GetExcelTableStringIdFromIndex(int index)
        {
            DataTable excelTables = GetExcelTableFromStringId("EXCELTABLES");
            if (excelTables == null) return null;

            DataRow[] rows = excelTables.Select(String.Format("index = {0}", index));
            return rows.Length == 0 ? null : rows[0][1].ToString();
        }

        public int LoadedTableCount
        {
            get { return XlsDataSet.Tables.Count; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (XlsDataSet != null)
            {
                XlsDataSet.Dispose();
            }
        }

        #endregion
    }
}