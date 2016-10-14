using System;
using System.Data;
using System.Web.UI;
using Microsoft.AnalysisServices.AdomdClient;

public partial class mdx2 : Page
{
    private const string connStr = "data source=.;initial catalog=SampleCube";

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        using (AdomdConnection conn = new AdomdConnection(connStr))
        {
            const string mdx = "select " +
                               "[Measures].[Votes Count] on columns, " +
                               "[Items].[Item Category].Members on rows " +
                               "from [Sample] " +
                               "where [Time].[Month].[January 2009]";
            using (AdomdCommand cmd = new AdomdCommand(mdx, conn))
            {
                conn.Open();
                CellSet cs = cmd.ExecuteCellSet();
                DataTable dt = new DataTable();
                dt.Columns.Add(" ");
                Axis columns = cs.Axes[0];
                TupleCollection columnTuples = columns.Set.Tuples;
                for (int i = 0; i < columnTuples.Count; i++)
                {
                    dt.Columns.Add(columnTuples[i].Members[0].Caption);
                }
                Axis rows = cs.Axes[1];
                TupleCollection rowTuples = rows.Set.Tuples;
                int rowNum = 0;
                foreach (Position rowPos in rows.Positions)
                {
                    DataRow dtRow = dt.NewRow();
                    int colNum = 0;
                    dtRow[colNum++] = rowTuples[rowNum].Members[0].Caption;
                    foreach (Position colPos in columns.Positions)
                    {
                        dtRow[colNum++] = cs.Cells[colPos.Ordinal, rowPos.Ordinal].FormattedValue;
                    }
                    dt.Rows.Add(dtRow);
                    rowNum++;
                }
                this.MdxGrid.DataSource = dt;
                this.MdxGrid.DataBind();
            }
        }
    }
}
