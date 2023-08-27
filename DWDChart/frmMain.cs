#pragma warning disable CS0219
#pragma warning disable CS0168
#pragma warning disable CS8321
#pragma warning disable CS0414
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

namespace DWDChart
{
    public partial class frmMain : Form
    {
        #region -- enum --
        enum ChartModus
        {
            Jahre,
            Monat,
        }
        #endregion

        #region -- Variablen --
        private DataSet _data;
        private DataTable _table;
        private ChartModus _chartmodus;
        #endregion

        #region -- ctor --
        public frmMain()
        {
            InitializeComponent();

            string xmlname = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "MBRDataSet.xml");

            _data = new DataSet();
            _data.ReadXml(xmlname);
            _table = _data.Tables[0];
        }
        #endregion

        #region -- Events Form --
        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Text = System.IO.Path.GetFileNameWithoutExtension(Application.ExecutablePath);
            this.MinimumSize = new Size(850, 600);
            this.panTop.BorderStyle = BorderStyle.FixedSingle;
            this.tcMain.SelectedIndex = 0;
            this.chartDWD.Dock = DockStyle.Fill;
            this.gridDWD.Dock = DockStyle.Fill;
            this.splitter.MinExtra = 0;
            this.splitter.DoubleClick += Splitter_DoubleClick;
            this.picLogo.Cursor = Cursors.Hand;
            this.picLogo.Click += PicLogo_Click;
            this.txtInfo.Multiline = true;
            this.txtInfo.Dock = DockStyle.Fill;
            this.txtInfo.ScrollBars = ScrollBars.Vertical;
            this.txtInfo.WordWrap = true;
            this.txtInfo.Font = new Font("Courier New", 10);
            this.txtInfo.Text = "";
            this.statlblDWDLink.Click += delegate (object xsender, EventArgs xe) { GoToDWDURI(); };

            DataTable dtabDropDown;
            dtabDropDown = new DataTable();
            dtabDropDown.Columns.AddRange(new DataColumn[] 
            { 
                new DataColumn("Text", typeof(string)),
                new DataColumn("Value", typeof(string)),
                new DataColumn("KurzBez", typeof(string)),
            });
            dtabDropDown.Rows.Add("Rostock-Warnemünde (Mecklenburg-Vorpommern)", "4271", "hro");
            dtabDropDown.Rows.Add("Augsburg (Bayern)", "232", "a");
            dtabDropDown.Rows.Add("Köln/Bonn (Nordrhein-Westfalen)", "2667", "k");
            dtabDropDown.Rows.Add("Görlitz (Sachsen)", "1684", "gr");
            dtabDropDown.Rows.Add("Trier-Petrisberg (Rheinland-Pfalz)", "5100", "tr");
            dtabDropDown.Rows.Add("Magdeburg (Sachsen-Anhalt)", "3126", "md");
            this.cbStation.Items.Clear();
            this.cbStation.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbStation.DisplayMember = "Text";
            this.cbStation.ValueMember = "Value";
            this.cbStation.DataSource = dtabDropDown;
            this.cbStation.SelectedIndex = 0;
            this.cbStation.SelectedIndexChanged += CbStation_SelectedIndexChanged;

            dtabDropDown = new DataTable();
            dtabDropDown.Columns.AddRange(new DataColumn[] 
            {
                new DataColumn("Text", typeof(string)),
                new DataColumn("Value", typeof(string)),
                new DataColumn("KurzBez", typeof(string)),
                new DataColumn("Beschreibung", typeof(string)),
            });

            dtabDropDown.Rows.Add("Temperatur [°C]", "OBS_DEU_P1D_T2M",
                "temperatur", 
                "Tägliche Stationsmessungen der mittleren Lufttemperatur in 2 m Höhe in °C");

            dtabDropDown.Rows.Add("Niederschlagshöhe [mm]", "OBS_DEU_P1D_RR", 
                "niederschlag",
@"Tägliche Stationsmessungen der Niederschlagshöhe in mm
---------------------------------------------------------------------------------------------------
1 mm / m² = 1 Liter
Schnee, Graupel, Hagel und dergleichen feste Aggregatzustände des Niederschlags werden geschmolzen. 
Das Volumen das Schmelzwassers wird in die Niederschlagshöhe eingerechnet.");
            
            dtabDropDown.Rows.Add("Windgeschwindigkeit [m/s]", "OBS_DEU_P1D_F", 
                "wind",
@"Tagesmittel der Stationsmessungen der mittleren Windgeschwindigkeit in ca. 10 m Höhe in m/s
---------------------------------------------------------------------------------------------------
Umrechnung m/s in Beaufort (Bft):
    m/s       Bft  Beschreibung
------------  ---  --------------------------------------------------------------------------------  
       < 0.3   0   Windstille - Rauch steigt senkrecht auf
 0.3 - < 1.6   1   Leiser Zug - Windrichtung angezeigt durch den Zug des Rauches
 1.6 - < 3.4   2   Leichte Brise - Wind im Gesicht spürbar, Blätter und Windfahnen bewegen sich
 3.4 - < 5.5   3   Schwacher Wind - Wind bewegt dünne Zweige und streckt Wimpel
 5.5 - < 8.0   4   Mäßiger Wind - Wind bewegt Zweige und dünnere Äste, hebt Staub und loses Papier
 8.0 - <10.8   5   Frischer Wind - leine Laubbäume beginnen zu schwanken, Schaumkronen bilden sich auf Seen
10.8 - <13.9   6   Starker Wind - starke Äste schwanken, Regenschirme sind nur schwer zu halten, Stromleitungen pfeifen im Wind
13.9 - <17.2   7   Steifer Wind - fühlbare Hemmungen beim Gehen gegen den Wind, ganze Bäume bewegen sich
17.2 - <20.8   8   Stürmischer Wind - Zweige brechen von Bäumen, erschwert erheblich das Gehen im Freien
20.8 - <24.5   9   Sturm - Äste brechen von Bäumen, kleinere Schäden an Häusern (Dachziegel oder Rauchhauben abgehoben
24.5 - <28.5  10   Schwerer Sturm - Wind bricht Bäume, größere Schäden an Häusern
28.5 - <32.7  11   Orkanartiger Sturm - Wind entwurzelt Bäume, verbreitet Sturmschäden
32.7          12   Orkan - schwere Verwüstungen");
            
            dtabDropDown.Rows.Add("Bewölkung [n/8]", "OBS_DEU_P1D_N",
                "bewölkung",
@"Tägliche Stationsmessungen des mittleren Bedeckungsgrades in Achtel
---------------------------------------------------------------------------------------------------
Bezeichnung                             Erläuterung
--------------------------------------  -----------------------------------------------------------
wolkenlos, sonnig | klar (nur nachts)   Gesamtbedeckungsgrad 0/8
leicht bewölkt | heiter (nur tagsüber)  Bedeckungsgrad tiefer und mittelhoher Wolken 1 bis 3/8  (*)
wolkig	                                Bedeckungsgrad tiefer und mittelhoher Wolken 4 bis 6/8  (*)
stark bewölkt	                        Bedeckungsgrad tiefer und mittelhoher Wolken 7/8        (*)
bedeckt oder trüb                       Bedeckungsgrad tiefer und mittelhoher Wolken 8/8; trüb, wenn
                                        tiefe Bewölkung einen Bedeckungsgrad von 8/8 hat
---------------------------------------------------------------------------------------------------
(*) hohe Wolken bis 8/8 möglich");
            this.cbKennzahl.Items.Clear();
            this.cbKennzahl.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbKennzahl.DisplayMember = "Text";
            this.cbKennzahl.ValueMember = "Value";
            this.cbKennzahl.DataSource = dtabDropDown;
            this.cbKennzahl.SelectedIndex = 0;
            this.cbKennzahl.SelectedIndexChanged += CbKennzahl_SelectedIndexChanged;

            this.cbJahr.Items.Clear();
            this.cbJahr.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbJahr.Items.Add("");
            int jahrVon = int.MaxValue;
            int jahrBis = int.MinValue;
            foreach (DataRow row in _table.Rows)
            {
                int jahr = row.Field<DateTime>("Datum").Year;
                if (jahr < jahrVon) jahrVon = jahr;
                if (jahr > jahrBis) jahrBis = jahr;
            }
            for (int j = jahrVon; j <= jahrBis; j++)
            {
                this.cbJahr.Items.Add(j.ToString());
            }
            this.cbJahr.SelectedIndex = 0;
            this.cbJahr.SelectedIndexChanged += CbJahr_SelectedIndexChanged;

            this.cbMonat.Items.Clear();
            this.cbMonat.DropDownStyle = ComboBoxStyle.DropDownList;
            CultureInfo ci = new CultureInfo("de-DE");
            for (int m = 1; m <= 12; m++)
            {
                string monatname = ci.DateTimeFormat.MonthNames[m - 1];
                this.cbMonat.Items.Add(monatname);
            }
            this.cbMonat.SelectedIndex = 0;
            this.cbMonat.SelectedIndexChanged += CbMonat_SelectedIndexChanged;

            this.cbTag.Items.Clear();
            this.cbTag.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbTag.SelectedIndexChanged += CbTag_SelectedIndexChanged;
            CbMonat_SelectedIndexChanged(null, null);

            this.gridDWD.ReadOnly = true;
            this.gridDWD.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gridDWD.AllowUserToOrderColumns = false;
            this.gridDWD.AllowUserToResizeRows = false;
            this.gridDWD.AllowUserToResizeColumns = true;
            this.gridDWD.AllowUserToAddRows = false;
            this.gridDWD.AllowUserToDeleteRows = false;
            this.gridDWD.MultiSelect = false;
            this.gridDWD.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.gridDWD.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.gridDWD.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan;
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            this.tcMain.Height = GetTabcontrolFullHeight() - 64;
        }
        #endregion

        #region -- Events Filter Controls --
        private void CbStation_SelectedIndexChanged(object sender, EventArgs e)
        {
            DiagramZeichnen();
        }

        private void CbKennzahl_SelectedIndexChanged(object sender, EventArgs e)
        {
            DiagramZeichnen();
        }

        private void CbJahr_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Wenn ein Jahr vorgegeben wird, dann den Tag auf 0 setzen,
            // um den ganzen Monat im Diagramm anzuzeigen.
            if (this.cbJahr.SelectedIndex > 0)
            {
                this.cbTag.SelectedIndexChanged -= CbTag_SelectedIndexChanged;
                this.cbTag.SelectedIndex = 0;
                this.cbTag.SelectedIndexChanged += CbTag_SelectedIndexChanged;
            }
            _chartmodus = this.cbJahr.SelectedIndex == 0 ? ChartModus.Jahre : ChartModus.Monat;
            DiagramZeichnen();
        }

        private void CbMonat_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Nach Änderung des Monats die Tage aktualisieren. Dabei den aktuell eingestellten
            // Tag merken und für den neuen Monat wieder einstellen (wenn möglich).
            int aktuellerTag = this.cbTag.Items.Count > 0 ? this.cbTag.SelectedIndex : 0;
            int monat = this.cbMonat.SelectedIndex + 1;
            int anzahlTage = DateTime.DaysInMonth(1999, monat); // Schaltjahre außen vor lassen
            this.cbTag.SelectedIndexChanged -= CbTag_SelectedIndexChanged;
            this.cbTag.Items.Clear();
            this.cbTag.Items.Add("");
            for (int t = 1; t <= anzahlTage; t++)
            {
                this.cbTag.Items.Add(t.ToString());
            }
            this.cbTag.MaxDropDownItems = anzahlTage + 1;
            this.cbTag.SelectedIndex = aktuellerTag <= anzahlTage ? aktuellerTag : anzahlTage;
            this.cbTag.SelectedIndexChanged += CbTag_SelectedIndexChanged;
            _chartmodus = this.cbJahr.SelectedIndex == 0 ? ChartModus.Jahre : ChartModus.Monat;
            DiagramZeichnen();
        }

        private void CbTag_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Wenn ein konkreter Tag ausgewählt wurde, dann das Jahr zurücksetzen
            if (this.cbTag.SelectedIndex > 0 && this.cbJahr.SelectedIndex > 0)
            {
                this.cbJahr.SelectedIndexChanged -= CbJahr_SelectedIndexChanged;
                this.cbJahr.SelectedIndex = 0;
                this.cbJahr.SelectedIndexChanged += CbJahr_SelectedIndexChanged;
            }
            _chartmodus = this.cbJahr.SelectedIndex == 0 ? ChartModus.Jahre : ChartModus.Monat;
            DiagramZeichnen();
        }
        #endregion

        #region -- Events sonstige Controls --

        private void mnuDateiSpeichern_Click(object sender, EventArgs e)
        {
            DiagrammSpeichern();
        }

        private void mnuDateiBeenden_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Splitter_DoubleClick(object sender, EventArgs e)
        {
            int h = GetTabcontrolFullHeight();
            if (this.tcMain.Height < h - 10)
            {
                this.tcMain.Height = h;
            }
            else
            {
                this.tcMain.Height = h - 64; // DWD-Logo-Height
            }
        }

        private void PicLogo_Click(object sender, EventArgs e)
        {
            GoToDWDURI(@"https://cdc.dwd.de/portal/");
        }

        #endregion

        #region -- FillGrid --
        private void FillGrid()
        {
            string kennzahl = this.cbKennzahl.SelectedValue.ToString();
            string station = this.cbStation.SelectedValue.ToString();
            int jahr = this.cbJahr.SelectedIndex > 0 ? Convert.ToInt32(this.cbJahr.Text) : -1;
            int monat = this.cbMonat.SelectedIndex + 1;
            int tag = this.cbTag.SelectedIndex == 0 ? -1 : this.cbTag.SelectedIndex;
            DataTable dtable = new DataTable();
            dtable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Datum", typeof(DateTime)),
                new DataColumn("Wert", typeof(float)),
                new DataColumn("Status", typeof(int)),
            });
            DataRow row;
            if (_chartmodus == ChartModus.Monat)
            {
                // Zeitschiene: Monat
                DateTime dtRow;
                DateTime dtVon = new DateTime(jahr, monat, 1);
                DateTime dtBis = dtVon.AddMonths(1).AddDays(-1); // Monatsletzter
                foreach (DataRow dr in _table.Rows)
                {
                    dtRow = (DateTime)dr["Datum"];
                    if (dr["Typ"].ToString() == kennzahl
                        && dr["Station"].ToString() == station
                        && Convert.ToInt32(dr["Tag"]) != -1 // -1 = Monatswert
                        && dtRow >= dtVon
                        && dtRow <= dtBis)
                    {
                        row = dtable.NewRow();
                        row["Datum"] = dr["Datum"];
                        row["Wert"] = dr["Wert"];
                        row["Status"] = dr["Status"];
                        dtable.Rows.Add(row);
                    }
                }
            }
            else
            {
                // Zeitschiene: Jahre
                foreach (DataRow dr in _table.Rows)
                {
                    if (dr["Typ"].ToString() == kennzahl
                        && dr["Station"].ToString() == station
                        && Convert.ToInt32(dr["Monat"]) == monat
                        && Convert.ToInt32(dr["Tag"]) == tag)
                    {
                        row = dtable.NewRow();
                        row["Datum"] = dr["Datum"];
                        row["Wert"] = dr["Wert"];
                        row["Status"] = dr["Status"];
                        dtable.Rows.Add(row);
                    }
                }
            }
            dtable.AcceptChanges();
            this.gridDWD.DataSource = dtable;
            foreach (DataGridViewColumn col in this.gridDWD.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.Automatic; // Sortieren erlauben
            }
        }
        #endregion

        #region -- DiagramZeichnen --
        private void DiagramZeichnen()
        {
            ResetChart();
            FillGrid();

            DateTime dtDatum;
            DataTable dtable = (DataTable)this.gridDWD.DataSource;

            // Zur Sicherheit
            if (dtable == null || dtable.Rows.Count == 0)
            {
                return;
            }

            // Limits der X-Achse
            int x_von = 0;
            int x_bis = 0;
            switch (_chartmodus)
            {
                case ChartModus.Jahre:
                    x_von = dtable.Rows[0].Field<DateTime>("Datum").Year;
                    x_bis = dtable.Rows[dtable.Rows.Count - 1].Field<DateTime>("Datum").Year;
                    break;
                case ChartModus.Monat:
                    x_von = 0;
                    x_bis = dtable.Rows.Count - 1;
                    break;
                default:
                    break;
            }

            // Zur Sicherheit
            if (x_bis <= x_von)
            {
                return;
            }

            // Koordinaten und Intervalle einrichten
            int cnt = x_bis - x_von + 1;
            double[] x = new double[cnt];
            double[] y = new double[cnt];
            int [] stat = new int[cnt];
            int x_min = int.MaxValue;
            int x_max = int.MinValue;
            int y_min = int.MaxValue;
            int y_max = int.MinValue;
            for (int i = 0; i < cnt; i++)
            {
                x[i] = x_von + i;
                y[i] = Math.Round(Convert.ToDouble(dtable.Rows[i].ItemArray[1]), 1);
                stat[i] = Convert.ToInt32(dtable.Rows[i].ItemArray[2]);
                x_min = Convert.ToInt32(Math.Round(x[i], 0)) - 1 < x_min ? Convert.ToInt32(Math.Round(x[i], 0)) - 1 : x_min;
                x_max = Convert.ToInt32(Math.Round(x[i], 0)) + 1 > x_max ? Convert.ToInt32(Math.Round(x[i], 0)) + 1 : x_max;
                y_min = Convert.ToInt32(Math.Round(y[i], 0)) - 1 < y_min ? Convert.ToInt32(Math.Round(y[i], 0)) - 1 : y_min;
                y_max = Convert.ToInt32(Math.Round(y[i], 0)) + 1 > y_max ? Convert.ToInt32(Math.Round(y[i], 0)) + 1 : y_max;
            }
            for (int i = 1; i <= 4; i++)
            {
                if (x_min % 5 != 0) x_min--;
                if (x_max % 5 != 0) x_max++;
                if (y_min % 5 != 0) y_min--;
                if (y_max % 5 != 0) y_max++;
            }
            string s = GetKennzahlKurzBez();
            switch (s)
            {
                case "temperatur":
                    if (y_min > 0) y_min = 0;
                    break;
                case "niederschlag":
                    y_min = 0;
                    y_max += 2;
                    break;
                case "bewölkung":
                    y_min = 0;
                    y_max = 10;
                    break;
                default:
                    break;
            }
            this.txtInfo.Text = GetKennzahlBeschreibung();

            // Einfache linerare Regression
            double x_mittel = x.Sum() / x.Length;
            double y_mittel = y.Sum() / y.Length;
            double x_x_mittel_2 = 0;
            double y_y_mittel_2 = 0;
            double x_x_mittel_y_y_mittel = 0;
            for (int i = 0; i < x.Length; i++)
            {
                x_x_mittel_2 += (x[i] - x_mittel) * (x[i] - x_mittel);
                y_y_mittel_2 += (y[i] - y_mittel) * (y[i] - y_mittel);
                x_x_mittel_y_y_mittel += (x[i] - x_mittel) * (y[i] - y_mittel);
            }
            double b_x = x_x_mittel_y_y_mittel / x_x_mittel_2;
            double a_x = y_mittel - b_x * x_mittel;

            // Chart zeichnen
            EnableChart(true);

            this.chartDWD.Series.Add("Wert");
            this.chartDWD.Series["Wert"].ChartType = SeriesChartType.Line;
            this.chartDWD.Series["Wert"].IsVisibleInLegend = false;
            this.chartDWD.Series["Wert"].Color = Color.Blue;
            this.chartDWD.Series["Wert"].IsValueShownAsLabel = true;

            this.chartDWD.Series.Add("Regression");
            this.chartDWD.Series["Regression"].ChartType = SeriesChartType.Line;
            this.chartDWD.Series["Regression"].Color = Color.Green;
            this.chartDWD.Series["Regression"].IsVisibleInLegend = false;
            this.chartDWD.Series["Regression"].IsValueShownAsLabel = false;

            ChartArea area = this.chartDWD.ChartAreas[0];
            area.Position = new ElementPosition(0, 5, 100, 95);
            area.BorderColor = Color.Gray;
            area.BorderDashStyle = ChartDashStyle.Solid;
            area.Visible = true;

            area.AxisX.MajorGrid.Enabled = true;
            area.AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            area.AxisX.MajorGrid.LineColor = Color.Gray;
            area.AxisX.MajorGrid.Interval = 1;
            area.AxisX.LineColor = Color.Gray;
            area.AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            area.AxisX.Minimum = x_min;
            area.AxisX.Maximum = x_max;
            area.AxisX.Interval = 5;
            area.AxisX.TitleFont = new Font("Arial", 10, FontStyle.Bold);
            area.AxisX.Title = _chartmodus == ChartModus.Jahre ? "Jahr" : string.Format("{0} {1}", this.cbMonat.Text, this.cbJahr.Text);

            area.AxisY.MajorGrid.Enabled = true;
            area.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            area.AxisY.MajorGrid.LineColor = Color.Gray;
            area.AxisY.MajorGrid.Interval = 1;
            area.AxisY.LineColor = Color.Gray;
            area.AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;
            area.AxisY.Minimum = y_min;
            area.AxisY.Maximum = y_max;
            area.AxisY.Interval = 5;
            area.AxisY.IsStartedFromZero = false;
            area.AxisY.TitleFont = new Font("Arial", 10, FontStyle.Bold);
            area.AxisY.Title = this.cbKennzahl.Text;

            string caption = (_chartmodus == ChartModus.Jahre && this.cbTag.SelectedIndex == 0 ? "Mittlere " : "") + this.cbKennzahl.Text;
            switch (_chartmodus)
            {
                case ChartModus.Jahre:
                    caption += cbTag.SelectedIndex == 0 
                        ? string.Format(" im {0}", this.cbMonat.Text) 
                        : string.Format(" am {0}. {1}", this.cbTag.Text, this.cbMonat.Text);
                    caption += string.Format(" von {0} bis {1}", x_von, x_bis);
                    break;
                case ChartModus.Monat:
                    caption += string.Format(" vom {0:dd.MM.yyyy} bis {1:dd.MM.yyyy}",
                        dtable.Rows[0].Field<DateTime>("Datum"),
                        dtable.Rows[dtable.Rows.Count - 1].Field<DateTime>("Datum"));
                    break;
                default:
                    break;
            }
            caption += " - " + this.cbStation.Text;

            Title title = new Title();
            title.Font = new Font("Arial", 10, FontStyle.Bold);
            title.ForeColor = Color.Black;
            title.Text = caption;
            this.chartDWD.Titles.Add(title);

            dtDatum = dtable.Rows[0].Field<DateTime>("Datum");
            for (int i = 0; i < x.Length; i++)
            {
                double y_reg = a_x + b_x * x[i];
                this.chartDWD.Series["Wert"].Points.AddXY(x[i], y[i]);
                if (_chartmodus == ChartModus.Monat)
                {
                    this.chartDWD.Series["Wert"].Points[i].AxisLabel = dtDatum.AddDays(i).ToString("dd.MM.yyyy");
                }
                if (stat[i] == 1)
                {
                    this.chartDWD.Series["Wert"].Points[i].LabelForeColor = Color.Red;
                }
                this.chartDWD.Series["Regression"].Points.AddXY(x[i], y_reg);
            }
        }
        #endregion

        #region -- DiagrammSpeichern --
        private void DiagrammSpeichern()
        {
            DataTable dtable = (DataTable)this.gridDWD.DataSource;
            if (dtable == null || dtable.Rows.Count == 0)
            {
                return;
            }

            string s = @"dwdchart"
                + "_" + GetStationKurzBez()
                + "_" + GetKennzahlKurzBez();
            switch (_chartmodus)
            {
                case ChartModus.Jahre:
                    // JahrVon_JahrBis_Monat_Tag
                    s += "_"
                        + dtable.Rows[0].Field<DateTime>("Datum").Year.ToString() + "_"
                        + dtable.Rows[dtable.Rows.Count - 1].Field<DateTime>("Datum").Year.ToString() + "_"
                        + this.cbMonat.Text 
                        + (this.cbTag.SelectedIndex > 0 ? ("_" + this.cbTag.Text) : "");
                    break;
                case ChartModus.Monat:
                    // Jahr_Monat
                    s += "_"
                        + dtable.Rows[0].Field<DateTime>("Datum").Year.ToString() + "_"
                        + this.cbMonat.Text;
                    break;
                default:
                    break;
            }

            string fname = GetFileNameBySavedialog("Chart speichern", s.ToLower(), new string[] { "jpg", "png", "bmp" }, "jpg", true);
            if (!string.IsNullOrEmpty(fname))
            {
                ChartImageFormat format = new ChartImageFormat();
                switch (Path.GetExtension(fname).ToLower())
                {
                    case ".png":
                        format = ChartImageFormat.Png;
                        break;
                    case ".jpg":
                    case ".jpeg":
                        format = ChartImageFormat.Jpeg;
                        break;
                    case ".bmp":
                        format = ChartImageFormat.Bmp;
                        break;
                    default:
                        format = ChartImageFormat.Jpeg;
                        break;
                }
                this.chartDWD.SaveImage(fname, format);
            }
        }
        #endregion

        #region -- sonstiges privates -
        private void GoToDWDURI(string link = "")
        {
            if (string.IsNullOrEmpty(link))
            {
                Process.Start(@"https://www.dwd.de");
            }
            else
            {
                Process.Start(link);
            }
        }

        private string GetStationKurzBez()
        {
            DataTable dtabDropDown = (DataTable)this.cbStation.DataSource;
            string s = Convert.ToString(dtabDropDown.Rows[this.cbStation.SelectedIndex]["KurzBez"]);
            return s;
        }

        private string GetKennzahlKurzBez()
        {
            DataTable dtabDropDown = (DataTable)this.cbKennzahl.DataSource;
            string s = Convert.ToString(dtabDropDown.Rows[this.cbKennzahl.SelectedIndex]["KurzBez"]);
            return s;
        }

        private string GetKennzahlBeschreibung()
        {
            DataTable dtabDropDown = (DataTable)this.cbKennzahl.DataSource;
            string s = Convert.ToString(dtabDropDown.Rows[this.cbKennzahl.SelectedIndex]["Beschreibung"]);
            return s;
        }

        private void ResetChart()
        {
            this.chartDWD.Titles.Clear();
            this.chartDWD.Series.Clear();
        }

        private void EnableChart(bool enable)
        {
            this.chartDWD.Visible = enable;
            this.gridDWD.Visible = enable;
        }

        private int GetTabcontrolFullHeight()
        {
            return this.ClientRectangle.Height - this.splitter.Height - this.statusStrip1.Height - this.panTop.Height - this.menuMain.Height - 0;
        }

        /// <summary>
        /// Erzeugt eine starke Zufallszahl Z mit min >= Z <= max (für Testzwecke)
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private int GenerateRandomNumber(int min, int max)
        {
            System.Security.Cryptography.RNGCryptoServiceProvider c = new System.Security.Cryptography.RNGCryptoServiceProvider();
            byte[] randomNumber = new byte[sizeof(int)];
            c.GetBytes(randomNumber);
            int seed = Math.Abs(BitConverter.ToInt32(randomNumber, 0));
            return new Random(seed).Next(min, max + 1);
        }

        private string GetFileNameBySavedialog(string aTitle, string aDefaultFilename, string[] aExtensions, string aDefaultExt, bool aOverwritePrompt)
        {
            string sFile = String.Empty;
            StringBuilder sbFilter = new StringBuilder();
            int iFilterIndex = 0;
            int i = 0;

            foreach (string ext in aExtensions)
            {
                i++;
                if (ext.Equals(aDefaultExt))
                    iFilterIndex = i;
                sbFilter.Append(String.Format("{0}-Dateien (*.{0})|*.{0}|", ext));
            }
            sbFilter.Append("Alle Dateien (*.*)|*.*");

            SaveFileDialog dlgSaveFile = new SaveFileDialog();
            dlgSaveFile.DefaultExt = aDefaultExt;
            dlgSaveFile.Title = aTitle;
            dlgSaveFile.CheckPathExists = true;
            dlgSaveFile.OverwritePrompt = aOverwritePrompt;
            dlgSaveFile.ShowHelp = false;
            dlgSaveFile.RestoreDirectory = true;
            //dlgSaveFile.Filter = "sql-Dateien (*.sql)|*.sql|Alle Dateien (*.*)|*.*";
            dlgSaveFile.Filter = sbFilter.ToString();
            dlgSaveFile.FilterIndex = iFilterIndex > 0 ? iFilterIndex : 1;
            dlgSaveFile.FileName = String.IsNullOrEmpty(aDefaultFilename) ? String.Concat("*.", aDefaultExt) : aDefaultFilename;
            if (dlgSaveFile.ShowDialog() == DialogResult.OK)
                sFile = dlgSaveFile.FileName;
            return sFile;
        }
        #endregion
    }
}
