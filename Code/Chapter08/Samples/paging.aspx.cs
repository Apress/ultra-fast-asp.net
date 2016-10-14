using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class paging : Page
{
    public const string ConnString = 
        "Data Source=atlantis;Initial Catalog=Sample;Integrated Security=True;async=true";
    private int _count;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.RegisterRequiresControlState(this);
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        this.DataReady = new ManualResetEvent(false);
        this.Context.Items["dataready"] = this.DataReady;
        if (!this.IsPostBack)
        {
            this.Count = -1;
            this.NeedCount = true;
            this.NewPageIndex = 0;
            this.GetPage();
        }
        else
        {
            this.NeedCount = false;
        }
    }

    protected override void LoadControlState(object savedState)
    {
        if (savedState != null)
        {
            this.Count = (int)savedState;
        }
    }

    protected override object SaveControlState()
    {
        return this.Count;
    }

    public void PageIndexChanging(Object sender, GridViewPageEventArgs e)
    {
        this.NewPageIndex = e.NewPageIndex;
        this.GetPage();
    }

    private void GetPage()
    {
        BeginEventHandler beh = new BeginEventHandler(BeginAsync);
        EndEventHandler eeh = new EndEventHandler(EndAsync);
        PageAsyncTask pat = new PageAsyncTask(beh, eeh, null, null, true);
        this.RegisterAsyncTask(pat);
        this.ExecuteRegisteredAsyncTasks();
    }

    private IAsyncResult BeginAsync(object sender, EventArgs e, AsyncCallback cb, object state)
    {
        SqlConnection conn = new SqlConnection(ConnString);
        SqlCommand cmd = new SqlCommand("[Traffic].[PageViewRows]", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlParameterCollection p = cmd.Parameters;
        p.Add("startrow", SqlDbType.Int).Value = 
            (this.NewPageIndex * this.pvgrid.PageSize) + 1;
        p.Add("pagesize", SqlDbType.Int).Value = this.pvgrid.PageSize;
        p.Add("getcount", SqlDbType.Bit).Value = this.NeedCount;
        p.Add("count", SqlDbType.Int).Direction = ParameterDirection.Output;
        conn.Open();
        IAsyncResult ar = cmd.BeginExecuteReader(cb, cmd);
        return ar;
    }

    private void EndAsync(IAsyncResult ar)
    {
        using (SqlCommand cmd = (SqlCommand)ar.AsyncState)
        {
            using (cmd.Connection)
            {
                using (SqlDataReader reader = cmd.EndExecuteReader(ar))
                {
                    this.Data = new DataTable();
                    this.Data.Load(reader);
                    this.Context.Items["data"] = this.Data;
                    if (this.NeedCount)
                        this.Count = (int)cmd.Parameters["count"].Value;
                    this.DataReady.Set();
                }
            }
        }
    }

    protected override void OnUnload(EventArgs e)
    {
        base.OnUnload(e);
        this.DataReady.Close();
    }

    public int Count
    {
        get
        {
            return this._count;
        }
        set
        {
            this._count = value;
            this.Context.Items["count"] = value;
        }
    }

    public bool NeedCount { get; set; }
    public int NewPageIndex { get; set; }
    public ManualResetEvent DataReady { get; set; }
    public DataTable Data { get; set; }
}