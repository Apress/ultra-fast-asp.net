using System;
using System.Web;
using System.Web.UI;
using com.terraserver_usa;

public partial class terra1 : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.AsyncTimeout = TimeSpan.FromSeconds(30);
        PageAsyncTask pat = new PageAsyncTask(BeginAsync, EndAsync, TimeoutAsync, null, true);
        RegisterAsyncTask(pat);
    }

    private IAsyncResult BeginAsync(object sender, EventArgs e, AsyncCallback cb, object state)
    {
        TerraService terra = new TerraService();
        Place place = new Place();
        place.City = "Seattle";
        place.State = "WA";
        place.Country = "US";
        IAsyncResult ar = terra.BeginGetPlaceFacts(place, cb, terra);
        return ar;
    }

    private void EndAsync(IAsyncResult ar)
    {
        TerraService terra = (TerraService)ar.AsyncState;
        PlaceFacts facts = terra.EndGetPlaceFacts(ar);
        this.LA.Text = String.Format("Latitude: {0:0.##}", facts.Center.Lat);
        this.LO.Text = String.Format("Longitude: {0:0.##}", facts.Center.Lon);
    }

    private void TimeoutAsync(IAsyncResult ar)
    {
        this.LA.Text = "Web service call timed out.";
    }
}
