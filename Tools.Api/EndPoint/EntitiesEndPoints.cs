
namespace Tools.Api.EndPoint
{
    public static class EntitiesEndPoints
    {
        public static void MapEntitiesEndPoints(this WebApplication app)
        {
            // GET 
            app.MapGet("/api/Cliente", ClienteEndPoints.GetAllClientesAsync);
            app.MapGet("/api/Cliente/TotalDeuda", ClienteEndPoints.GetTotalDebt);
            app.MapGet("/api/Cliente/CuatroConMasDeuda", ClienteEndPoints.GetFourMoreDebt);

            //POST
            app.MapPost("/api/Cliente", ClienteEndPoints.Add);


            //PUT
            app.MapPut("/api/Cliente", ClienteEndPoints.Update);
        }
    }
}
