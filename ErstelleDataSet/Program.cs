using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace CreateDataSet
{
    internal class Program
    {
        #region -- class TDWD --
        class TDWD
        {
            public TDWD(string produkt_code, string sdo_id, string zeitstempel, string wert, string qualitaet_byte, string qualitaet_niveau)
            {
                this.Produkt_Code = produkt_code.Trim('"');
                this.SDO_ID = sdo_id.Trim('"');
                this.Zeitstempel = zeitstempel.Trim('"');
                this.Wert = wert.Trim('"');
                this.Qualitaet_Byte = qualitaet_byte.Trim('"');
                this.Qualitaet_Niveau = qualitaet_niveau.Trim('"');
            }

            public string Produkt_Code { get; set; }
            public string SDO_ID { get; set; }
            public string Zeitstempel { get; set; }
            public string Wert { get; set; }
            public string Qualitaet_Byte { get; set; }
            public string Qualitaet_Niveau { get; set; }
            public override string ToString() => $"({Produkt_Code},{SDO_ID},{Zeitstempel},{Wert},{Qualitaet_Byte},{Qualitaet_Niveau})";
        }
        #endregion

        #region -- clas TMBR --
        class TMBR
        {
            public TMBR(string typ, string station, DateTime datum, float wert, int status = 0)
            {
                this.Typ = typ; // Kennzahl Temperatur, Wind, Bewölkung usw.
                this.Station = station;
                this.Datum = datum;
                this.Wert = wert;
                this.Status = status; // 0 = nicht angefaßt, 1 = fehlender Wert wurde aus Durchschnitt ergänzt
            }

            public string Typ { get; set; }
            public string Station { get; set; }
            public DateTime Datum { get; set; }
            public float Wert { get; set; }
            public int Status { get; set; }
            public override string ToString() => $"({Typ}|{Station}|{Datum}|{Wert}|{Status})";
        }
        #endregion

        #region -- Konstanten --
        const string _pfadData = @"c:\Projekte\DWDChart\ErstelleDataSet\_data\";
        #endregion

        #region -- Variablen --
        static List<string> _lstStationen = new List<string>();
        static List<string> _lstTypen = new List<string>();

        static List<TDWD> _lstDWD = new List<TDWD>();
        static List<TMBR> _lstMBRTag = new List<TMBR>();
        static List<TMBR> _lstMBRMonat = new List<TMBR>();
        #endregion


        #region == Main ==
        static void Main(string[] args)
        {
            DWDEinlesen();
            DWDToMBR();
            MBRKorrektur();
            MBRCheck();

            MBRDuplikateEntfernen();
            MBRLueckenAuffuellen();
            MBRCheckFinal();

            ErzeugeMonatsliste();
            ErzeugeDataSet();

            Console.WriteLine("Beenden mit Return-Taste...");
            Console.ReadLine();
        }
        #endregion


        #region -- DWDEinlesen --
        private static void DWDEinlesen()
        {
            Console.WriteLine("DWD-Dateien einlesen");

            DirectoryInfo di = new DirectoryInfo(Path.Combine(_pfadData, "DWD"));
            FileInfo[] files = di.GetFiles("*.csv");
            foreach (var file in files)
            {
                Console.WriteLine("- Datei " + file.Name);

                string line = String.Empty;
                int k = 0;
                using (StreamReader sr = File.OpenText(file.FullName))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (k > 0 && !string.IsNullOrEmpty(line.Trim()))
                        {
                            string[] token = line.Split(',');
                            // "Produkt_Code","SDO_ID","Zeitstempel","Wert","Qualitaet_Byte","Qualitaet_Niveau"
                            //  0              1        2             3      4                5
                            // "OBS_DEU_P1D_T5CM_N","1303","1936-02-01","6","0","5",
                            DateTime dtZeit = DateTime.Parse(token[2].Trim('"'));
                            if (dtZeit.Year >= 1947)
                            {
                                TDWD dwd = new TDWD(token[0], token[1], token[2], token[3], token[4], token[5]);
                                _lstDWD.Add(dwd);
                            }
                        }
                        k++;
                    }
                }
            }

            Console.WriteLine("DWD-Liste sortieren");
            _lstDWD = _lstDWD
                        .OrderBy(x => x.Produkt_Code)
                        .ThenBy(x => x.SDO_ID)
                        .ThenBy(x => x.Zeitstempel)
                        .ToList();
        }
        #endregion

        #region -- DWDToMBR --
        private static void DWDToMBR()
        {
            Console.WriteLine("DWD-Liste in MBR-Liste konvertieren");

            foreach (var dwd in _lstDWD)
            {
                string typ = dwd.Produkt_Code;
                string station = dwd.SDO_ID;
                float wert = float.Parse(dwd.Wert, System.Globalization.CultureInfo.InvariantCulture);
                DateTime dtZeit = DateTime.Parse(dwd.Zeitstempel);
                if (typ == "OBS_DEU_PT1H_F")
                {
                    continue;
                }
                _lstMBRTag.Add(new TMBR(typ, station, dtZeit.Date, wert));

                // Stationen und Typen einsammeln für spätere Verwendung
                if (_lstStationen.IndexOf(station) == -1) _lstStationen.Add(station);
                if (_lstTypen.IndexOf(typ) == -1) _lstTypen.Add(typ);
            }
        }
        #endregion

        #region -- MBRKorrektur --
        private static void MBRKorrektur()
        {
            Console.WriteLine("Temp-Listen für Windmessung in Trier anlegen");

            List<TMBR> trier = new List<TMBR>();
            foreach (var dwd in _lstDWD)
            {
                DateTime dt = DateTime.Parse(dwd.Zeitstempel).Date;
                if (dt.Year < 1940)
                {
                    continue;
                }
                if (dwd.Produkt_Code == "OBS_DEU_PT1H_F")
                {
                    trier.Add(new TMBR("OBS_DEU_P1D_F", "5100", dt, float.Parse(dwd.Wert, System.Globalization.CultureInfo.InvariantCulture), 0));
                }
            }
            if (trier.Count == 0)
            {
                return;
            }

            List<TMBR> trierMittelwert = new List<TMBR>();
            DateTime dtDatum = trier[0].Datum.Date;
            int anzahl = 0;
            float mittelwert = 0;
            foreach (var t in trier)
            {
                anzahl++;
                mittelwert += t.Wert;
                if (t.Datum.Date != dtDatum)
                {
                    mittelwert /= anzahl;
                    mittelwert = Convert.ToSingle(Math.Round(mittelwert, 1));
                    trierMittelwert.Add(new TMBR("OBS_DEU_P1D_F", "5100", dtDatum, mittelwert));
                    dtDatum = t.Datum.Date;
                    anzahl = 0;
                    mittelwert = 0;
                }
            }

            Console.WriteLine("MBR-Liste aus Temp-Listen vervollständigen");

            int n = 0;
            foreach (var tm in trierMittelwert)
            {
                bool ex = _lstMBRTag.Any(x => x.Typ == tm.Typ && x.Station == tm.Station && x.Datum == tm.Datum);
                if (!ex)
                {
                    TMBR mbr = new TMBR(tm.Typ, tm.Station, tm.Datum, tm.Wert, 1);
                    _lstMBRTag.Add(mbr);
                    Console.WriteLine(string.Format("- Satz {0} eingefügt, {1}/{2}", mbr, n, trierMittelwert.Count));
                }
                n++;
            }

            Console.WriteLine("MBR-Liste neu sortieren");
            _lstMBRTag = _lstMBRTag
                            .OrderBy(x => x.Typ)
                            .ThenBy(x => x.Station)
                            .ThenBy(x => x.Datum)
                            .ToList();
        }
        #endregion

        #region -- MBRCheck --
        private static void MBRCheck()
        {
            Console.WriteLine("MBR-Liste auf Datumslücken prüfen");

            int aktzeptiert = 10;
            foreach (string typ in _lstTypen)
            {
                foreach (string station in _lstStationen)
                {
                    int deltaDays;
                    for (int i = 0; i < _lstMBRTag.Count - 1; i++)
                    {
                        TMBR t1 = _lstMBRTag[i];
                        TMBR t2 = _lstMBRTag[i + 1];
                        if (t1.Typ == typ && t2.Typ == typ && t1.Station == station && t2.Station == station)
                        {
                            deltaDays = (t2.Datum.Date - t1.Datum.Date).Days;
                            if (deltaDays > aktzeptiert)
                            {
                                Console.WriteLine("- Lücke: " + t1.ToString() + " " + t2.ToString() + " " + deltaDays);
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Weiter mit Return-Taste...");
            Console.ReadLine();

        }
        #endregion

        #region -- MBRDuplikateEntfernen --
        private static void MBRDuplikateEntfernen()
        {
            Console.WriteLine("Dublikate entfernen");

            // zur Sicherheit, aktuell keine Duplikate gefunden
            for (int i = 0; i < _lstMBRTag.Count - 1; i++)
            {
                TMBR t1 = _lstMBRTag[i];
                TMBR t2 = _lstMBRTag[i + 1];
                if (t1.Typ == t2.Typ && t1.Station == t2.Station && t1.Datum == t2.Datum)
                {
                    _lstMBRTag.RemoveAt(i);
                    i--;
                }
            }
        }
        #endregion

        #region -- MBRLueckenAuffuellen --
        private static void MBRLueckenAuffuellen()
        {
            Console.WriteLine("Lücken mit Schnittwerten auffüllen");

            foreach (string typ in _lstTypen)
            {
                foreach (string station in _lstStationen)
                {
                    int deltaDays;
                    int n = 0;
                    while (n < _lstMBRTag.Count - 1)
                    {
                        TMBR t1 = _lstMBRTag[n];
                        TMBR t2 = _lstMBRTag[n + 1];
                        if (t1.Typ == typ && t2.Typ == typ && t1.Station == station && t2.Station == station)
                        {
                            deltaDays = (t2.Datum.Date - t1.Datum.Date).Days;
                            if (deltaDays > 1)
                            {
                                float wert = Convert.ToSingle(Math.Round((t1.Wert + t2.Wert) / 2, 1));
                                for (int i = 0; i < deltaDays - 1; i++)
                                {
                                    TMBR mbr = new TMBR(t1.Typ, t1.Station, t1.Datum.AddDays(i + 1), wert, 1);
                                    _lstMBRTag.Insert(n + 1 + i, mbr);
                                }
                            }
                        }
                        n++;
                    }
                }
            }
        }
        #endregion

        #region -- MBRCheckFinal --
        private static void MBRCheckFinal()
        {
            Console.WriteLine("MBR-Liste final prüfen. Jedes Datum muss einen korrekten Nachfolger haben.");
            foreach (string typ in _lstTypen)
            {
                foreach (string station in _lstStationen)
                {
                    for (int i = 0; i < _lstMBRTag.Count - 1; i++)
                    {
                        TMBR t1 = _lstMBRTag[i];
                        TMBR t2 = _lstMBRTag[i + 1];
                        if (t1.Typ == typ && t2.Typ == typ
                            && t1.Station == station && t2.Station == station
                            && t1.Datum.AddDays(1) != t2.Datum)
                        {
                            throw new Exception(string.Format("- Fehler in Zeile {0}: Item: {1}", i, t1));
                        }
                    }
                }
            }
        }
        #endregion

        #region -- Monatsliste erzeugen --
        private static void ErzeugeMonatsliste()
        {
            Console.WriteLine("MBR-Monatsliste erzeugen");

            int jahr = _lstMBRTag[0].Datum.Year;
            int monat = _lstMBRTag[0].Datum.Month;
            int anzahlTage = 0;
            float wert = 0;
            bool status1vorhanden = false;
            for (int i = 0; i < _lstMBRTag.Count; i++)
            {
                anzahlTage++;
                wert += _lstMBRTag[i].Wert;
                if (_lstMBRTag[i].Status == 1 && !status1vorhanden) status1vorhanden = true;
                if (_lstMBRTag[i].Datum.Year != jahr || _lstMBRTag[i].Datum.Month != monat)
                {
                    wert /= anzahlTage;
                    wert = Convert.ToSingle(Math.Round(wert, 1));
                    TMBR mbr = new TMBR(_lstMBRTag[i - 1].Typ, _lstMBRTag[i - 1].Station, new DateTime(jahr, monat, 1), wert, status1vorhanden ? 1 : 0);
                    _lstMBRMonat.Add(mbr);
                    wert = 0;
                    anzahlTage = 0;
                    jahr = _lstMBRTag[i].Datum.Year;
                    monat = _lstMBRTag[i].Datum.Month;
                    status1vorhanden = false;
                }
            }
        }
        #endregion

        #region -- ErzeugeDataSet --
        private static void ErzeugeDataSet()
        {
            Console.WriteLine("DataSet erzeugen und speichern");

            DataSet ds = new DataSet();
            DataTable dtable = new DataTable();
            dtable.Columns.Add("Typ", typeof(System.String));
            dtable.Columns.Add("Station", typeof(System.String));
            dtable.Columns.Add("Jahr", typeof(int));
            dtable.Columns.Add("Monat", typeof(int));
            dtable.Columns.Add("Tag", typeof(int));
            dtable.Columns.Add("Datum", typeof(System.DateTime));
            dtable.Columns.Add("Wert", typeof(float));
            dtable.Columns.Add("Status", typeof(int));
            ds.Tables.Add(dtable);
            DataRow row;
            foreach (var t in _lstMBRTag)
            {
                row = dtable.NewRow();
                row["Typ"] = t.Typ;
                row["Station"] = t.Station;
                row["Jahr"] = t.Datum.Year;
                row["Monat"] = t.Datum.Month;
                row["Tag"] = t.Datum.Day;
                row["Datum"] = t.Datum;
                row["Wert"] = t.Wert;
                row["Status"] = t.Status;
                dtable.Rows.Add(row);
            }
            foreach (var t in _lstMBRMonat)
            {
                row = dtable.NewRow();
                row["Typ"] = t.Typ;
                row["Station"] = t.Station;
                row["Jahr"] = t.Datum.Year;
                row["Monat"] = t.Datum.Month;
                row["Tag"] = -1;
                row["Datum"] = t.Datum;
                row["Wert"] = t.Wert;
                row["Status"] = t.Status;
                dtable.Rows.Add(row);
            }
            dtable.AcceptChanges();
            ds.AcceptChanges();
            ds.WriteXml(@"c:\Projekte\DWDChart\DWDChart\bin\Debug\MBRDataSet.xml", XmlWriteMode.WriteSchema);
        }
        #endregion
    }
}
