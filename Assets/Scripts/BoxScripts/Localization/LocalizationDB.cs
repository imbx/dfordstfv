using System;
using System.Data;
using UnityEngine;
using BoxScripts;

namespace  BoxScripts.Localization{
    public class LocalizationDB
    {

        public string dataPath = "";
        private DataSet localDB;
        // private LocAsset[] db;

        public void BuildColumns()
        {
            DataTable dialogues = new DataTable("Dialogues");
            DataTable thoughts = new DataTable("Thoughts");
            DataTable UI = new DataTable("UI");
        }

        private DataColumn CreateColumn(string Name  = "", bool isUnique = false)
        {
            DataColumn col = new DataColumn();
            col.DataType = Type.GetType("System.String");
            col.ColumnName = Name;
            col.ReadOnly = isUnique;
            col.Unique = isUnique;
            return col;
        }

        private void CreateColumns(DataTable dt, string[] lang, bool autoAssign = false)
        {
            dt.Columns.Add(CreateColumn("identifier", true));

            foreach(string l in lang)
            {
                dt.Columns.Add(CreateColumn(l));
            }

            if(autoAssign) localDB.Tables.Add(dt);
        }

        private void CreateRow(DataTable dt, string idName, string[] lang, string[] textRows)
        {
            DataRow row = dt.NewRow();
            row["identifier"] = idName;
            for(int i = 0; i < lang.Length; i++)
            {
                string baseString = i < textRows.Length ? textRows[i] : "";
                row[lang[i]] = baseString;
            }
            dt.Rows.Add(row);
        }

    }
}