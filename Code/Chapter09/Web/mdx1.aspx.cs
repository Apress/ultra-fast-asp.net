using System;
using System.Web.UI;
using Microsoft.AnalysisServices.AdomdClient;

public partial class mdx1 : Page
{
    private const string connStr = "data source=.;initial catalog=SampleCube";

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        using (AdomdConnection conn = new AdomdConnection(connStr))
        {
            const string mdx = "select " +
	            "[Measures].[Votes Count] on columns, " +
	            "[Items].[Item Category].&[Health] on rows " +
	            "from [Sample] " +
	            "where [Time].[Month].[January 2009]";
            using (AdomdCommand cmd = new AdomdCommand(mdx, conn))
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    this.RowName.Text = reader[0].ToString();
                    this.TotHealthVotes.Text = reader[1].ToString();
                }
            }
        }
    }
}
